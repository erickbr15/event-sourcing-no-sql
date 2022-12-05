namespace Eventsourcing.DataLoader.Interfaces;

public interface ITextToInputModelMapper<TInputModel>
{
    IEnumerable<TInputModel> Map(IEnumerable<string> textLines);
}
