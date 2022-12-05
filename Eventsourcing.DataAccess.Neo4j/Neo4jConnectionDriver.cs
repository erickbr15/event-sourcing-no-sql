using Eventsourcing.DataAccess.Neo4j.Interfaces;
using Neo4j.Driver;

namespace Eventsourcing.DataAccess.Neo4j;

public class Neo4jConnectionDriver : INeo4jConnectionDriver, IDisposable
{
    private static object _syncRoot = new object();
    private static readonly Lazy<Neo4jConnectionDriver> _lazy =  new Lazy<Neo4jConnectionDriver>(()=> new Neo4jConnectionDriver());
    private bool _disposed;
    
    private IDriver _driver;
    private string _database;

    public static INeo4jConnectionDriver Neo4JDriver { get { return _lazy.Value; } }

    private Neo4jConnectionDriver()
    {
    }

    ~Neo4jConnectionDriver() => Dispose(false);    

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _driver?.Dispose();
        }

        _disposed = true;
    }

    public async Task InitDriverAsync(Neo4jConnectionOptions connectionOptions)
    {
        if (connectionOptions == null)
        {
            throw new ArgumentNullException(nameof(connectionOptions));
        }
        lock (_syncRoot)
        {
            _driver = GraphDatabase.Driver(connectionOptions.Uri, AuthTokens.Basic(connectionOptions.User, connectionOptions.Password));
        }
        _database = connectionOptions.Database;

        await _driver.VerifyConnectivityAsync();
    }

    public async Task WriteTransactionAsync(Neo4jCommand command)
    {
        using var session = _driver.AsyncSession(configBuilder =>
                configBuilder.WithDefaultAccessMode(AccessMode.Write).WithDatabase(_database));

        await session.WriteTransactionAsync(async tx =>
        {
            var cursor = await tx.RunAsync(command.Query, command.Parameters);
            await cursor.ConsumeAsync();
        });
    }

    public async Task<IList<TResult>> ExecuteReadAsync<TResult>(Neo4jCommand command, Func<IRecord, TResult> mapper)
    {
        using var session = _driver.AsyncSession(configBuilder =>
                configBuilder.WithDefaultAccessMode(AccessMode.Read).WithDatabase(_database));

        IList<TResult> result = default!;

        await session.ExecuteReadAsync(async tx => {
            var cursor = await tx.RunAsync(command.Query, command.Parameters);
            var records = await cursor.ToListAsync();            
            result = records.Select(mapper).ToList();
        });

        return result;
    }
}
