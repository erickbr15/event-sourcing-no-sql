using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.DataAccess.MongoDb
{
    public interface IMongoDataService
    {
        Task<IEvent<FlightScheduledEventArgs>> InsertEventAsync(IEvent<FlightScheduledEventArgs> flightScheduledEvent, CancellationToken cancellationToken);

        Task<IEvent<FlightBookedEventArgs>> InsertEventAsync(IEvent<FlightBookedEventArgs> bookingEvent, CancellationToken cancellationToken);

    }
}
