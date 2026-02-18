namespace EvolutionaryArchitecture.Modules.Offers.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EvolutionaryArchitecture.Modules.Offers.Domain;

internal sealed class OfferEntityConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offers");
        builder.HasKey(offers => offers.Id);
        builder.Property(offers => offers.PreparedAt).IsRequired();
        builder.Property(offers => offers.OfferedFromTo).IsRequired();
        builder.Property(offers => offers.OfferedFromDate).IsRequired();
        builder.Property(offers => offers.Discount).IsRequired();
        builder.Property(offers => offers.CustomerId).IsRequired();
    }
}