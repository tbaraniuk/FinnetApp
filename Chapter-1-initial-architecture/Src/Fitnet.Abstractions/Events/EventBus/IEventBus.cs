namespace EvolutionaryArchitecture.Fitnet.Abstractions.Events.EventBus;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent eventData, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent;
}