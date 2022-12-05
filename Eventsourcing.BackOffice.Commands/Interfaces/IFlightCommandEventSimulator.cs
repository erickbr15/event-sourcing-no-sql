using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.BackOffice.Commands.Interfaces;

public interface IFlightCommandEventSimulator
{
    IEnumerable<IEvent<FlightScheduledEventArgs>> ScheduleFlights();
}
