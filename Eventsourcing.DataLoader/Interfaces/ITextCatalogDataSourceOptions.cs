namespace Eventsourcing.DataLoader.Interfaces;

public interface ITextCatalogDataSourceOptions
{
    string CountriesFullpathDataSource { get; }
    string CitiesFullpatgDataSource { get; }
    string CarriersFullpathDataSource { get; }
    string AirportsFullpathDataSource { get; }
    string BookingStatusesFullpathDataSource { get; }
}
