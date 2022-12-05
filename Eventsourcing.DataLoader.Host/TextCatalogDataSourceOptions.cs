using Eventsourcing.DataLoader.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Eventsourcing.DataLoader.Host;

public class TextCatalogDataSourceOptions : ITextCatalogDataSourceOptions
{
    private readonly IConfiguration _configuration;

    private string _countriesFullpath = default!;
    private string _citiesFullpath = default!;
    private string _carriersFullpath = default!;
    private string _airportsFullpath = default!;
    private string _bookingStatusesFullpath = default!;
    private string _menNamesFullpath = default!;
    private string _womenFullpath = default!;
    private string _lastNamesFullpath = default!;

    public TextCatalogDataSourceOptions(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        var catalogsDataSourceSection = _configuration.GetRequiredSection("CatalogsDataSource");
        if(catalogsDataSourceSection.Exists() == false)
        {
            throw new InvalidOperationException("The CatalogsDataSource configuration is missing.");
        }

        _countriesFullpath = catalogsDataSourceSection.GetValue<string>("CountriesFullpath") ?? string.Empty;
        _citiesFullpath = catalogsDataSourceSection.GetValue<string>("CitiesFullpath") ?? string.Empty;
        _carriersFullpath = catalogsDataSourceSection.GetValue<string>("CarriersFullpath") ?? string.Empty;
        _airportsFullpath = catalogsDataSourceSection.GetValue<string>("AirportsFullpath") ?? string.Empty;
        _bookingStatusesFullpath = catalogsDataSourceSection.GetValue<string>("BookingStatusesFullpath") ?? string.Empty;
        _menNamesFullpath = catalogsDataSourceSection.GetValue<string>("MenNamesFullpath") ?? string.Empty;
        _womenFullpath = catalogsDataSourceSection.GetValue<string>("WomenNamesFullpath") ?? string.Empty;
        _lastNamesFullpath = catalogsDataSourceSection.GetValue<string>("LastNamesFullpath") ?? string.Empty;
    }

    public string CountriesFullpathDataSource => _countriesFullpath;

    public string CitiesFullpatgDataSource => _citiesFullpath;

    public string CarriersFullpathDataSource => _carriersFullpath;

    public string AirportsFullpathDataSource => _airportsFullpath;

    public string BookingStatusesFullpathDataSource => _bookingStatusesFullpath;

    public string MenNamesFullpathDataSource => _menNamesFullpath;

    public string WomenNamesFullpathDataSource => _womenFullpath;

    public string LastNamesFullpathDataSource => _lastNamesFullpath;
}
