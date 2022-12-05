namespace Eventsourcing.Domain;

public class Booking
{
    public Guid Id { get; set; }
    public Guid FlightId { get; set; }
    public string Code { get; set; } = default!;
    public Guid UserId { get; set; }
    public short StatusId { get; set; }
    public short? SeatRow { get; set; }
    public string? SeatLetter { get; set; }
    public decimal Price { get; set; }
    public decimal? LuggageWeight { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime CheckinDate { get; set; }
    public bool? IsWebCheckin { get; set; }
    public Flight Flight { get; set; } = default!;
    public User User { get; set; } = default!;
}
