using Eventsourcing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventsourcing.DataAccess.Sql.Entities.TypesConfiguration;

internal class FlightTypeConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.ToTable("Flights", "dbo");
        builder.HasKey(t => t.FlightId);

        builder.Property(t => t.FlightId).HasColumnName("FlightId").IsRequired();
        builder.Property(t => t.Code).HasColumnName("Code").HasMaxLength(10).IsRequired();
        builder.Property(t => t.SourceAirportId).HasColumnName("SourceAirportId").IsRequired();
        builder.Property(t => t.DestinationAirportId).HasColumnName("DestinationAirportId").IsRequired();
        builder.Property(t => t.CarrierId).HasColumnName("CarrierId").IsRequired();
        builder.Property(t => t.Duration).HasColumnName("Duration").IsRequired();
        builder.Property(t => t.Departure).HasColumnName("Departure").IsRequired();
        builder.Property(t => t.Arrival).HasColumnName("Arrival").IsRequired();

        builder.HasMany(t => t.Bookings)
            .WithOne(t => t.Flight)
            .HasForeignKey(t => t.FlightId)
            .IsRequired();
    }
}
