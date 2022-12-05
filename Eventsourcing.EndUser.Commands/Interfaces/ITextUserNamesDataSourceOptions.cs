namespace Eventsourcing.EndUser.Commands.Interfaces;

public interface ITextUserNamesDataSourceOptions
{
    string MenNamesFullpathDataSource { get; }
    string WomenNamesFullpathDataSource { get; }
    string LastNamesFullpathDataSource { get; }
}
