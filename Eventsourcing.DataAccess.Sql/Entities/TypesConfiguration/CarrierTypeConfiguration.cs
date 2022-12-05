using Eventsourcing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventsourcing.DataAccess.Sql.Entities.TypesConfiguration;

internal class CarrierTypeConfiguration : IEntityTypeConfiguration<Carrier>
{
    public void Configure(EntityTypeBuilder<Carrier> builder)
    {
        builder.ToTable("Carriers", "dbo");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
        builder.Property(t => t.Code).HasColumnName("Code").HasMaxLength(10).IsRequired();
        builder.Property(t => t.Name).HasColumnName("Name").HasMaxLength(150);

        builder.HasMany(t => t.Flights)
            .WithOne(t => t.Carrier)
            .HasForeignKey(t => t.CarrierId)
            .IsRequired();
    }
}
