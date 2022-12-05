namespace Eventsourcing.DataAccess.Neo4j;

public class Neo4jCommand
{
    public string Query { get; set; } = default!;
    public object Parameters { get; set; } = default!;
}
