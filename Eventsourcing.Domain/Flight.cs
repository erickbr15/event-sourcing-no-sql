namespace Eventsourcing.Domain;

public class Flight
{
    public Guid FlightId { get; set; }
    public string Code { get; set; } = default!;
    public Guid SourceAirportId { get; set; }
    public Guid DestinationAirportId { get; set; }
    public Guid CarrierId { get; set; }
    public int Duration { get; set; }
    public DateTime Departure { get; set; }
    public DateTime Arrival { get; set; }
    public Carrier Carrier { get; set; } = default!;
    public Airport SourceAirport { get; set; } = default!;
    public Airport DestinationAirport { get; set; } = default!;
    public IList<Booking> Bookings { get; set; } = new List<Booking>();
}
