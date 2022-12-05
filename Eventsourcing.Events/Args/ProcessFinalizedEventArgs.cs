namespace Eventsourcing.Events.Args;

public class ProcessFinalizedEventArgs : EventArgs
{
    public bool Success  => string.IsNullOrEmpty(this.Error);
    public string Error { get; set; } = default!;
    public string EventDescription { get; set; } = default!;

    public ProcessFinalizedEventArgs(string eventDescription)
    {
        this.EventDescription = eventDescription;
    }

    public ProcessFinalizedEventArgs(string eventDescription, string error)
    {
        this.EventDescription= eventDescription;
        this.Error = error;
    }

    public ProcessFinalizedEventArgs(string eventDescription, Exception ex)
    {
        this.EventDescription = eventDescription;
        this.Error = ex.ToString();
    }
}
