namespace EvolutionaryArchitecture.Fitnet.Abstractions.EventContracts;

using EvolutionaryArchitecture.Fitnet.Abstractions.Events;

public record PassExpiredEvent(Guid Id, Guid PassId, Guid CustomerId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    public static PassExpiredEvent Create(Guid passId, Guid customerId, DateTimeOffset occurredAt) =>
        new(Guid.NewGuid(), passId, customerId, occurredAt);
}
