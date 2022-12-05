namespace Eventsourcing.Domain;

public class Carrier
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public IList<Flight> Flights { get; set; } = new List<Flight>();

    public override string ToString()
    {
        return $"Carrier:{{Id:{Id}, Code:{Code}, Name:{Name}}}";
    }
}
