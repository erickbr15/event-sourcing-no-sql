using Eventsourcing.DataLoader.Interfaces;

namespace Eventsourcing.DataLoader;

public class DataLoaderFacade : IDataLoaderFacade
{
    private readonly ITextCatalogMapper _catalogMapper;
    private readonly ISqlDataLoader _sqlDataLoader;
    private readonly IGraphDataLoader _graphDataLoader;

    public DataLoaderFacade(ITextCatalogMapper catalogMapper, ISqlDataLoader sqlDataLoader, IGraphDataLoader graphDataLoader)
    {
        _catalogMapper = catalogMapper ?? throw new ArgumentNullException(nameof(catalogMapper));
        _sqlDataLoader = sqlDataLoader ?? throw new ArgumentNullException(nameof(sqlDataLoader));
        _graphDataLoader = graphDataLoader ?? throw new ArgumentNullException(nameof(graphDataLoader));
    }

    public async Task CreateCatalogsInSqlAsync(ITextCatalogDataSource dataSource, CancellationToken cancellationToken)
    {
        var catalogs = _catalogMapper.MapToInputModel(dataSource);

        try
        {
            await _sqlDataLoader.CleanupCatalogsAsync(cancellationToken);

            await _sqlDataLoader.LoadCountriesAsync(catalogs.Countries, cancellationToken);
            await _sqlDataLoader.LoadCitiesAsync(catalogs.Cities, cancellationToken);
            await _sqlDataLoader.LoadCarriersAsync(catalogs.Carriers, cancellationToken);
            await _sqlDataLoader.LoadAirportsAsync(catalogs.Airports, cancellationToken);
            await _sqlDataLoader.LoadBookingStatusesAsync(catalogs.BookingStatuses, cancellationToken);
        }
        catch (Exception ex)
        {
            //TODO: Log the exception
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }        
    }

    public async Task CreateCatalogsInNeo4jAsync(ITextCatalogDataSource dataSource, CancellationToken cancellationToken)
    {
        var catalogs = _catalogMapper.MapToInputModel(dataSource);

        try
        {
            await _graphDataLoader.LoadCitiesAsync(catalogs.Cities, cancellationToken);
            await _graphDataLoader.LoadCarriersAsync(catalogs.Carriers, cancellationToken);
            await _graphDataLoader.LoadAirportsAsync(catalogs.Airports, cancellationToken);
        }
        catch (Exception ex)
        {
            //TODO: Log the exception
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }
    }
}
