using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.EndUser.Commands.Interfaces;

public interface IEndUserCommands
{
    Task<IEnumerable<IEvent<UserRegisteredEventArgs>>> RegisterUsersAsync(CancellationToken CancellationToken);
}
