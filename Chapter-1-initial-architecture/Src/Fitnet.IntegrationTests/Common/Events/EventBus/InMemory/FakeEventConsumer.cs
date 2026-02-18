namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Abstractions.Events.EventBus.InMemory;

using EvolutionaryArchitecture.Fitnet.Abstractions.Events;

internal sealed class TestEventConsumer : IIntegrationEventHandler<FakeEvent>
{
    public Task Handle(FakeEvent @event, CancellationToken cancellationToken)
    {
        @event.MarkAsConsumed();
        return Task.CompletedTask;
    }
}
