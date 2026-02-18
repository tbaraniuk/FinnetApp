namespace EvolutionaryArchitecture.Modules.Offers;

using Infrastructure.Database;
using Domain;
using EvolutionaryArchitecture.Fitnet.Abstractions.EventContracts;
using EvolutionaryArchitecture.Fitnet.Abstractions.Events;
using EvolutionaryArchitecture.Fitnet.Abstractions.Events.EventBus;

internal sealed class PassExpiredEventHandler(
    IEventBus eventBus,
    OffersPersistence persistence,
    TimeProvider timeProvider) : IIntegrationEventSubscriber<PassExpiredEvent>
{
    public async Task Handle(PassExpiredEvent @event, CancellationToken cancellationToken)
    {
        var nowDate = timeProvider.GetUtcNow();
        var offer = Offer.PrepareStandardPassExtension(@event.CustomerId, nowDate);
        persistence.Offers.Add(offer);
        await persistence.SaveChangesAsync(cancellationToken);

        var offerPreparedEvent = OfferPrepareEvent.Create(offer.Id, offer.CustomerId, timeProvider.GetUtcNow());
        await eventBus.PublishAsync(offerPreparedEvent, cancellationToken);
    }
}
