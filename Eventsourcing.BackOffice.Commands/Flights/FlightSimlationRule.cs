namespace Eventsourcing.BackOffice.Commands.Flights;

public class FlightSimlationRule
{
    public IList<string> CarrierCodes { get; set; } = new List<string>();
    public IDictionary<string, IList<TimeOnly>> SourceAirportCodesAndTimes { get; set; } =
        new Dictionary<string, IList<TimeOnly>>();
    public IList<string> DestinationAirportCodes { get; set; } = new List<string>();        
}
