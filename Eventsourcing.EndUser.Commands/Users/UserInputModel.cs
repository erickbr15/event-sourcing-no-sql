namespace Eventsourcing.EndUser.Commands.Users;

public class UserInputModel
{
    public Guid UserId { get; set; }    
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public short? Age { get; set; } 
    public string Genre { get; set; } = default!;   
}
