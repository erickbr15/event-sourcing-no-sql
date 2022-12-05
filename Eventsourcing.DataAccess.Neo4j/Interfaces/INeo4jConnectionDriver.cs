using Neo4j.Driver;

namespace Eventsourcing.DataAccess.Neo4j.Interfaces;

public interface INeo4jConnectionDriver
{
    Task InitDriverAsync(Neo4jConnectionOptions connectionOptions);
    Task WriteTransactionAsync(Neo4jCommand commnand);
    Task<IList<TResult>> ExecuteReadAsync<TResult>(Neo4jCommand command, Func<IRecord, TResult> mapper);
}
