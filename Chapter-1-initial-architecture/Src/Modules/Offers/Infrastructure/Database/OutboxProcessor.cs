namespace EvolutionaryArchitecture.Modules.Offers.Infrastructure.Database;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using EvolutionaryArchitecture.Fitnet.Abstractions.Events.EventBus;

public partial class OutboxProcessor
    (IServiceProvider serviceProvider, IEventBus bus, ILogger<OutboxProcessor> logger)
    : BackgroundService
{
    [LoggerMessage(
        EventId = 100,
        Level = LogLevel.Warning,
        Message = "{message}"
    )]
    static partial void LogOutboxFailure(ILogger logger, string message, Exception exception);

    [LoggerMessage(
        EventId = 200,
        Level = LogLevel.Warning,
        Message = "{message}{type}"
    )]
    static partial void LogOutboxTypeFailure(ILogger logger, string message, string type);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var persistence = scope.ServiceProvider.GetRequiredService<OffersPersistence>();
                var outboxMessages = await persistence.OutboxMessages
                    .Where(m => m.ProcessedAt == null)
                    .ToListAsync(stoppingToken);

                foreach (var outboxMessage in outboxMessages)
                {
                    if (outboxMessage.Type == "OfferCreated")
                    {
                        var payload = JsonSerializer.Deserialize<Dictionary<string, Guid>>(outboxMessage.Payload);

                        var sagaId = payload!["sagaId"];

                        var saga = await persistence.OfferSagas
                            .FirstOrDefaultAsync(x => x.sagaId == sagaId, stoppingToken);

                        if (saga != null && saga.status != SagaStatus.COMPLETED)
                        {
                            saga.status = SagaStatus.COMPLETED;
                            saga.updatedAt = DateTime.UtcNow;
                        }
                    }
                    else
                    {
                        var eventType = Type.GetType(outboxMessage.Type);

                        if (eventType is null)
                        {
                            LogOutboxTypeFailure(logger, "Could not resolve type: {Type}: ", outboxMessage.Type);
                            continue;
                        }

                        var eventPayload = JsonSerializer.Deserialize(outboxMessage.Payload, eventType);

                        if (eventPayload is null)
                        {
                            continue;
                        }

                        await bus.PublishAsync((dynamic)eventPayload, stoppingToken);
                    }

                    outboxMessage.ProcessedAt = DateTimeOffset.Now;
                }
                await persistence.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                LogOutboxFailure(logger, "Error processing outbox messages", ex);
            }
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
