using Eventsourcing.DataLoader.Interfaces;
using Eventsourcing.DataLoader.Model;

namespace Eventsourcing.DataLoader.Mappers;

public class AirportTextToInputModelMapper : ITextToInputModelMapper<AirportInputModel>
{
    public IEnumerable<AirportInputModel> Map(IEnumerable<string> textLines)
    {
        if (textLines is null || textLines.Any() == false)
        {
            return Enumerable.Empty<AirportInputModel>();
        }

        var inputmodel = new List<AirportInputModel>();
        var indexes = new TextAirportIndexes();

        foreach (var line in textLines)
        {
            var fields = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var newModel = new AirportInputModel
            {
                Code = fields[indexes.Code],
                Name = fields[indexes.Name],
                CityName = fields[indexes.City]
            };

            inputmodel.Add(newModel);
        }

        return inputmodel;
    }

    private struct TextAirportIndexes
    {
        public int Code => 0;
        public int Name => 1;
        public int City => 2;
    }
}
