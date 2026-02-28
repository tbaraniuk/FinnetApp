namespace EvolutionaryArchitecture.Modules.Offers.Infrastructure.Database;

public sealed class OfferSaga
{
    public Guid sagaId { get; set; }
    public Guid offerId { get; set; }
    public SagaStatus status { get; set; }
    public DateTimeOffset createdAt { get; set; }
    public DateTimeOffset updatedAt { get; set; }
}
