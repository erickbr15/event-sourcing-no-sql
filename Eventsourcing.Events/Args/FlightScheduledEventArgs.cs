namespace Eventsourcing.Events.Args;

public class FlightScheduledEventArgs
{
    public Guid FlightId { get; set; }
    public string Code { get; set; } = default!;
    public string SourceAirportCode { get; set; } = default!;
    public string DestinationAirportCode { get; set; } = default!;
    public string CarrierCode { get; set; } = default!;
    public string CarrierName { get; set; } = default!;
    public int Duration { get; set; }
    public DateTime FlightDate { get; set; }

    public override string ToString()
    {
        return $"{CarrierName}.{Code}.{SourceAirportCode}.{DestinationAirportCode}.{FlightId}";
    }
}
