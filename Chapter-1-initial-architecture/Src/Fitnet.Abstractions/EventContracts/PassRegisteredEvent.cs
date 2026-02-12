namespace EvolutionaryArchitecture.Fitnet.Abstractions.EventContracts;

using EvolutionaryArchitecture.Fitnet.Abstractions.Events;

public record PassRegisteredEvent(Guid Id, Guid PassId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    public static PassRegisteredEvent Create(Guid passId) =>
        new(Guid.NewGuid(), passId, DateTimeOffset.UtcNow);
}
