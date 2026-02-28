namespace EvolutionaryArchitecture.Modules.Offers.Application;

using Infrastructure.Database;
using Domain;
using System.Text.Json;

internal sealed class CreateOfferCommandHandler(
    OffersPersistence persistence,
    TimeProvider timeProvider)
{
    public async Task Handle(
        Guid customerId,
        CancellationToken cancellationToken
    )
    {
        var nowDate = timeProvider.GetUtcNow();
        var offer = Offer.PrepareStandardPassExtension(customerId, nowDate);
        persistence.Offers.Add(offer);
        await persistence.SaveChangesAsync(cancellationToken);

        var offerPreparedEvent = OfferPrepareEvent.Create(offer.Id, offer.CustomerId, timeProvider.GetUtcNow());

        var outboxMessage = new OutboxMessage
        {
            Id = offer.Id,
            Type = "OfferCreated",
            Payload = JsonSerializer.Serialize(offerPreparedEvent),
            CreatedAt = nowDate
        };

        persistence.OutboxMessages.Add(outboxMessage);

        await persistence.SaveChangesAsync(cancellationToken);
    }
}
