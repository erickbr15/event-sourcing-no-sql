using Eventsourcing.BackOffice.Commands.Interfaces;
using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.BackOffice.Commands;

public class BackOfficeCommands : IBackOfficeCommands
{
    private readonly IFlightCommandEventSimulator _flightSchedulerEventSimulator;

    public BackOfficeCommands(IFlightCommandEventSimulator flightSchedulerEventSimulator)
    {
        _flightSchedulerEventSimulator = flightSchedulerEventSimulator ?? throw new ArgumentNullException(nameof(flightSchedulerEventSimulator));
    }

    public IEnumerable<IEvent<FlightScheduledEventArgs>> ScheduleFlights()
    {
        var events = _flightSchedulerEventSimulator.ScheduleFlights();
        return events;
    }
}