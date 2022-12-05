namespace Eventsourcing.DataAccess.Neo4j;

public class Neo4jConnectionOptions
{
    public string Uri { get; set; } = default!;
    public string User { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Database { get; set; } = default!;
}
