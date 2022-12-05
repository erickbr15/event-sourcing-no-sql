using Eventsourcing.DataLoader.Model;

namespace Eventsourcing.DataLoader.Interfaces;

public interface ITextCatalogMapper
{
    CatalogsInputModel MapToInputModel(ITextCatalogDataSource dataSource);
}
