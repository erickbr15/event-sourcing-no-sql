namespace Eventsourcing.Domain;

public class Country
{
    public Guid Code { get; set; }
    public string Name { get; set; } = default!;
    public IList<City> Cities { get; set; } = new List<City>();

    public override string ToString()
    {
        return $"Country{{Code:{Code}, Name:{Name}}}";
    }
}
