using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.BackOffice.Commands.Interfaces;

public interface IBackOfficeCommands
{
    IEnumerable<IEvent<FlightScheduledEventArgs>> ScheduleFlights();
}
