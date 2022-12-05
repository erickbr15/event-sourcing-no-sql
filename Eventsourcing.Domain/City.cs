namespace Eventsourcing.Domain;

public class City
{
    public Guid Code { get; set; }
    public Guid CountryCode { get; set; }
    public string Name { get; set; } = default!;
    public Country Country { get; set; } = default!;
    public IList<Airport> Airports { get; set; } = new List<Airport>();

    public override string ToString()
    {
        return $"City:{{CountryCode:{CountryCode}, Code:{Code}, Name:{Name}}}";
    }
}
