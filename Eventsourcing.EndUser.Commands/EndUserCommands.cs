using Eventsourcing.EndUser.Commands.Interfaces;
using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.EndUser.Commands
{
    public class EndUserCommands : IEndUserCommands
    {        
        public Task<IEnumerable<IEvent<UserRegisteredEventArgs>>> RegisterUsersAsync(CancellationToken CancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}