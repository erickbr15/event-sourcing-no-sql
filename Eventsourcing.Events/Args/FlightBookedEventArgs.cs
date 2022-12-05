namespace Eventsourcing.Events.Args;

public class FlightBookedEventArgs
{
    public string UserId { get; set; }
    public string FlightCode { get; set; }
    public string BookingId { get; set; }
    public string BookingDate { get; set; }
    public string UserEmail { get; set; }
    public string UserFullname { get; set; }
    public string UserAge { get; set; }
    public string JourneyId { get; set; }
    public string JourneyDate { get; set; }
}
