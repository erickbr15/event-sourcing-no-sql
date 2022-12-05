using Eventsourcing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventsourcing.DataAccess.Sql.Entities.TypesConfiguration;

internal class AirportTypeConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.ToTable("Airports", "dbo");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
        builder.Property(t => t.Code).HasColumnName("Code").HasMaxLength(10).IsRequired();
        builder.Property(t => t.CityCode).HasColumnName("CityCode").IsRequired();
        builder.Property(t => t.Name).HasColumnName("Name").HasMaxLength(500);

        builder.HasMany(t => t.OutgoingFlights)
            .WithOne(t => t.SourceAirport)
            .HasForeignKey(t => t.SourceAirportId)
            .IsRequired();

        builder.HasMany(t => t.ArrivingFlights)
            .WithOne(t => t.DestinationAirport)
            .HasForeignKey(t => t.DestinationAirportId)
            .IsRequired();
    }
}
