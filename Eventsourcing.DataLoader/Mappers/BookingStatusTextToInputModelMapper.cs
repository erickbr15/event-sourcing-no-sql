using Eventsourcing.DataLoader.Interfaces;
using Eventsourcing.DataLoader.Model;

namespace Eventsourcing.DataLoader.Mappers;

public class BookingStatusTextToInputModelMapper : ITextToInputModelMapper<BookingStatusInputModel>
{
    public IEnumerable<BookingStatusInputModel> Map(IEnumerable<string> textLines)
    {
        if (textLines is null || textLines.Any() == false)
        {
            return Enumerable.Empty<BookingStatusInputModel>();
        }

        var bookingStatusesInputModel = new List<BookingStatusInputModel>();

        var indexes = new TextBookingStatusIndexes();
        foreach (var line in textLines)
        {
            var fields = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var status = new BookingStatusInputModel
            {
                Id = short.Parse(fields[indexes.Id]),
                Name = fields[indexes.Name]
            };

            bookingStatusesInputModel.Add(status);
        }

        return bookingStatusesInputModel;
    }

    private struct TextBookingStatusIndexes
    {
        public int Id => 0;
        public int Name => 1;
    }
}
