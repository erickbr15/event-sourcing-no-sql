using Eventsourcing.DataLoader.Interfaces;
using Eventsourcing.DataLoader.Model;

namespace Eventsourcing.DataLoader.Mappers;

public class CityTextInputModelMapper : ITextToInputModelMapper<CityInputModel>
{
    public IEnumerable<CityInputModel> Map(IEnumerable<string> textLines)
    {
        if (textLines is null || textLines.Any() == false)
        {
            return Enumerable.Empty<CityInputModel>();
        }

        var citiesInputModel = new List<CityInputModel>();

        var indexes = new TextCityIndexes();
        foreach (var line in textLines)
        {
            var fields = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var city = new CityInputModel
            {
                CountryName = fields[indexes.Country],
                Name = fields[indexes.City]
            };

            citiesInputModel.Add(city);
        }

        return citiesInputModel;
    }

    private struct TextCityIndexes
    {
        public int Country => 0;
        public int City => 1;
    }
}
