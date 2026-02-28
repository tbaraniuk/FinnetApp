namespace EvolutionaryArchitecture.Modules.Offers.Infrastructure.Saga;

using System.Text.Json;
using EvolutionaryArchitecture.Fitnet.Abstractions.EventContracts;
using EvolutionaryArchitecture.Modules.Offers.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

public sealed class PassRenewalHandler(
    OffersPersistence persistence,
    TimeProvider timeProvider
    )
{
    public async Task Handle(PassExpiredEvent @event, CancellationToken cancellationToken)
    {
        if (await persistence.OfferSagas.AnyAsync(s => s.sagaId == @event.PassId, cancellationToken))
        {
            return;
        }

        var saga = new OfferSaga
        {
            sagaId = Guid.NewGuid(),
            status = SagaStatus.STARTED,
            createdAt = timeProvider.GetUtcNow()
        };

        persistence.OfferSagas.Add(saga);

        persistence.OutboxMessages.Add(new OutboxMessage
        {
            Type = "CreateOffer",
            Payload = JsonSerializer.Serialize(new
            {
                @event.PassId
            }),
            CreatedAt = DateTime.UtcNow
        });

        await persistence.SaveChangesAsync(cancellationToken);
    }

}
