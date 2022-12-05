using Eventsourcing.Events.Args;

namespace Eventsourcing.Events.Interfaces;

public interface IEventProcessor<TEvent>
{
    event EventHandler<ProcessFinalizedEventArgs> ProcessFinalized;
    Task ProcessAsync(TEvent eventToProcess);
}
