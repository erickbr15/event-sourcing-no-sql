namespace Eventsourcing.Domain;

public class Airport
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public Guid CityCode { get; set; }
    public string Name { get; set; } = default!;
    public City City { get; set; } = default!;
    public IList<Flight> OutgoingFlights { get; set; } = new List<Flight>();
    public IList<Flight> ArrivingFlights { get; set; } = new List<Flight>();

    public override string ToString()
    {
        return $"Airport:{{CityCode:{CityCode}, Id:{Id}, Code:{Code}, Name:{Name}}}";
    }
}
