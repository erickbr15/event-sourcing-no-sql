namespace Eventsourcing.DataLoader.Events;

public class EntityAddedToContextEventArgs : EventArgs
{
    public string EntityStateDescription { get; set; }

    public EntityAddedToContextEventArgs(string entityStateDescription)
    {
        EntityStateDescription = entityStateDescription;
    }
}
