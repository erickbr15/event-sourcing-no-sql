namespace Eventsourcing.EndUser.Commands.Interfaces;

public interface ITextUserNamesDataSource
{
    string MenNames { get; }
    string WomenNames { get; }
    string LastNames { get; }
}
