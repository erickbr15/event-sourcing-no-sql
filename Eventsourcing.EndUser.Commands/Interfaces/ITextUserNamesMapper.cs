using Eventsourcing.EndUser.Commands.Users;

namespace Eventsourcing.EndUser.Commands.Interfaces;

public interface ITextUserNamesMapper
{
    IEnumerable<UserInputModel> MapToInputModel(ITextUserNamesDataSource dataSource);
}
