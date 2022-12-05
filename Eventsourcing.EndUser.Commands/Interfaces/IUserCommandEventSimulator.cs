namespace Eventsourcing.EndUser.Commands.Interfaces;

internal interface IUserCommandEventSimulator
{
    Task RegisterUsersAsync(CancellationToken cancellationToken);
}
