namespace Eventsourcing.DataLoader.Interfaces;

public interface ITextCatalogDataSource
{
    string Countries { get; }
    string Cities { get; }
    string Carriers { get; }
    string Airports { get; }
    string BookingStatuses { get; }    
}
