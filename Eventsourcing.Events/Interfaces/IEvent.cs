namespace Eventsourcing.Events.Interfaces;

public interface IEvent<TEventArgs>
{
    string Topic { get; set; }
    string Subject { get; set; }
    string Id { get; set; }
    string EventTime { get; set; }
    TEventArgs EventArgs { get; set; }
    string DataVersion { get; set; }
}
