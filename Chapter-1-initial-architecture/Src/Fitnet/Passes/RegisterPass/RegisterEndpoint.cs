namespace EvolutionaryArchitecture.Fitnet.Passes.RegisterPass;

using Data;
using Data.Database;
using EvolutionaryArchitecture.Fitnet.Abstractions.EventContracts;
using EvolutionaryArchitecture.Fitnet.Abstractions.Events;
using EvolutionaryArchitecture.Fitnet.Abstractions.Events.EventBus;

internal sealed class ContractSignedEventHandler(
    PassesPersistence persistence,
    IEventBus eventBus) : IIntegrationEventSubscriber<ContractSignedEvent>
{
    public async Task Handle(ContractSignedEvent @event, CancellationToken cancellationToken)
    {
        var pass = Pass.Register(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt);
        await persistence.Passes.AddAsync(pass, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);

        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id);
        await eventBus.PublishAsync(passRegisteredEvent, cancellationToken);
    }
}
