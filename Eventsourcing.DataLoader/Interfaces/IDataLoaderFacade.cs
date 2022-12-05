namespace Eventsourcing.DataLoader.Interfaces;

public interface IDataLoaderFacade
{
    Task CreateCatalogsInSqlAsync(ITextCatalogDataSource dataSource, CancellationToken cancellationToken);
    Task CreateCatalogsInNeo4jAsync(ITextCatalogDataSource dataSource, CancellationToken cancellationToken);
}
