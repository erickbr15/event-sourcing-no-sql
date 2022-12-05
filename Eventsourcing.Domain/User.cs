namespace Eventsourcing.Domain;

public class User
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Genre { get; set; } = default!;
    public short Age { get; set; }
    public IList<Booking> Bookings { get; set; } = new List<Booking>();

    public override string ToString()
    {
        return $"{Name}.{LastName}.{Age}.{Genre}.{Email}.{UserId}";
    }
}
