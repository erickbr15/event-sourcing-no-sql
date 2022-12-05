using Eventsourcing.DataLoader.Interfaces;

namespace Eventsourcing.DataLoader;

public class TextCatalogDataSource : ITextCatalogDataSource
{
    private readonly ITextCatalogDataSourceOptions _options;

    private string _countriesText = default!;
    private string _citiesText = default!;
    private string _carriersText = default!;
    private string _airportsText = default!;
    private string _bookingStatusesText = default!;

    public TextCatalogDataSource(ITextCatalogDataSourceOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        LoadCatalogData();
    }

    public string Countries => _countriesText;

    public string Cities => _citiesText;

    public string Carriers => _carriersText;

    public string Airports => _airportsText;

    public string BookingStatuses => _bookingStatusesText;    

    private void LoadCatalogData()
    {
        _countriesText = File.ReadAllText(_options.CountriesFullpathDataSource, System.Text.Encoding.UTF8);
        _citiesText = File.ReadAllText(_options.CitiesFullpatgDataSource, System.Text.Encoding.UTF8);
        _carriersText = File.ReadAllText(_options.CarriersFullpathDataSource, System.Text.Encoding.UTF8);
        _airportsText = File.ReadAllText(_options.AirportsFullpathDataSource, System.Text.Encoding.UTF8);
        _bookingStatusesText = File.ReadAllText(_options.BookingStatusesFullpathDataSource, System.Text.Encoding.UTF8);
    }
}
