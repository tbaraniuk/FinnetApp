namespace EvolutionaryArchitecture.Fitnet.Abstractions.EventContracts;

using EvolutionaryArchitecture.Fitnet.Abstractions.Events;

public sealed record OfferPrepareEvent(Guid Id, Guid OfferId, Guid CustomerId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    public static OfferPrepareEvent Create(Guid offerId, Guid customerId, DateTimeOffset occurredAt) =>
        new(Guid.NewGuid(), offerId, customerId, occurredAt);
}
