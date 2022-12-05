using Eventsourcing.DataLoader.Interfaces;
using Eventsourcing.DataLoader.Model;

namespace Eventsourcing.DataLoader.Mappers;

public class CountryTextInputModelMapper : ITextToInputModelMapper<CountryInputModel>
{
    public IEnumerable<CountryInputModel> Map(IEnumerable<string> textLines)
    {
        if (textLines is null || textLines.Any() == false)
        {
            return Enumerable.Empty<CountryInputModel>();
        }

        var countryInputModels = new List<CountryInputModel>();
        foreach (var line in textLines)
        {
            var country = new CountryInputModel
            {
                Name = line.Trim()
            };
            countryInputModels.Add(country);
        }

        return countryInputModels;
    }
}
