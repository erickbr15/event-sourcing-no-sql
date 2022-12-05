using Eventsourcing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventsourcing.DataAccess.Sql.Entities.TypesConfiguration;

internal class BookingTypeConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings", "dbo");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("BookingId").IsRequired();
        builder.Property(t => t.FlightId).HasColumnName("FlightId").IsRequired();
        builder.Property(t => t.Code).HasColumnName("Code").HasMaxLength(15).IsRequired();
        builder.Property(t => t.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(t => t.StatusId).HasColumnName("BookingStatusId").IsRequired();
        builder.Property(t => t.Price).HasColumnName("Price").IsRequired();

        builder.Property(t => t.SeatRow).HasColumnName("SeatRow");
        builder.Property(t => t.SeatLetter).HasColumnName("SeatLetter").HasMaxLength(2);
        builder.Property(t => t.LuggageWeight).HasColumnName("LuggageWeight");
        builder.Property(t => t.BookingDate).HasColumnName("BookingDate");
        builder.Property(t => t.CheckinDate).HasColumnName("CheckinDate");
        builder.Property(t => t.IsWebCheckin).HasColumnName("IsWebCheckin");
    }
}
