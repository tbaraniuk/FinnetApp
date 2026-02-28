namespace EvolutionaryArchitecture.Modules.Offers.Infrastructure.Database;

using Domain;
using Microsoft.EntityFrameworkCore;

public sealed class OffersPersistence(DbContextOptions<OffersPersistence> options) : DbContext(options)
{
    private const string Schema = "Offers";

    public DbSet<Offer> Offers => Set<Offer>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<OfferSaga> OfferSagas => Set<OfferSaga>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new OfferEntityConfiguration());
        modelBuilder.Entity<OfferSaga>()
            .HasIndex(x => x.sagaId)
            .IsUnique();
    }
}
