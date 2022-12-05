using Eventsourcing.DataLoader.Interfaces;
using Eventsourcing.DataLoader.Model;

namespace Eventsourcing.DataLoader.Mappers;

public class CarrierTextToInputModelMapper : ITextToInputModelMapper<CarrierInputModel>
{
    public IEnumerable<CarrierInputModel> Map(IEnumerable<string> textLines)
    {
        if (textLines is null || textLines.Any() == false)
        {
            return Enumerable.Empty<CarrierInputModel>();
        }

        var carrierInputModel = new List<CarrierInputModel>();

        var indexes = new CarrierTextIndexes();
        foreach (var line in textLines)
        {
            var fields = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var carrier = new CarrierInputModel
            {
                Code = fields[indexes.Code],
                Name = fields[indexes.Name]
            };

            carrierInputModel.Add(carrier);
        }

        return carrierInputModel;
    }

    private struct CarrierTextIndexes
    {
        public int Code => 0;
        public int Name => 1;
    }
}
