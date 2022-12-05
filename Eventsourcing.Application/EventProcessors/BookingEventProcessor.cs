using Eventsourcing.DataAccess.Neo4j.Interfaces;
using Eventsourcing.DataAccess.Sql;
using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.Application.Neo4j.EventProcessors;

public class BookingEventProcessor : IEventProcessor<IEvent<FlightBookedEventArgs>>
{
    private static readonly object _syncRoot = new object();
    private readonly ICommandRepository _commandRepository;    

    public BookingEventProcessor(ICommandRepository commandRepository)
    {
        _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
    }

    public event EventHandler<ProcessFinalizedEventArgs> OnProcessFinalized;

    event EventHandler<ProcessFinalizedEventArgs> IEventProcessor<IEvent<FlightBookedEventArgs>>.ProcessFinalized
    {
        add
        {
            lock (_syncRoot)
            {
                OnProcessFinalized += value;
            }
        }

        remove
        {
            lock (_syncRoot)
            {
                OnProcessFinalized -= value;
            }
        }
    }

    public async Task ProcessAsync(IEvent<FlightBookedEventArgs> eventToProcess)
    {
        await _commandRepository.AddBookingAsync(eventToProcess.EventArgs);
    }
}
