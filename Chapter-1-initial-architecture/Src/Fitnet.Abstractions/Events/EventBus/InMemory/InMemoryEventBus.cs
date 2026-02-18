namespace EvolutionaryArchitecture.Fitnet.Abstractions.Events.EventBus.InMemory;

using MediatR;

internal sealed class InMemoryEventBus(IMediator mediator) : IEventBus
{
    public async Task PublishAsync<TEvent>(TEvent eventData, CancellationToken cancellationToken = default) where TEvent : IIntegrationEvent =>
        await mediator.Publish(eventData, cancellationToken);
}
