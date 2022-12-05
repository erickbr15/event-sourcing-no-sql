using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.Events;

public class FlightBookedEvent : IEvent<FlightBookedEventArgs>
{
    public string Topic { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Id { get; set; } = default!;
    public string EventTime { get; set; } = default!;
    public string DataVersion { get; set; } = default!;
    public FlightBookedEventArgs EventArgs { get; set; } = default!;

    public override string ToString()
    {
        return $"{Topic}.{Subject}.{EventTime}.{EventArgs}";
    }
}
