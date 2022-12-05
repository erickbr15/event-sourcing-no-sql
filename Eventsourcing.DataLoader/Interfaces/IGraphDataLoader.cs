using Eventsourcing.DataLoader.Model;

namespace Eventsourcing.DataLoader.Interfaces;

public interface IGraphDataLoader
{
    Task LoadCitiesAsync(IEnumerable<CityInputModel> cities, CancellationToken cancellationToken);
    Task LoadCarriersAsync(IEnumerable<CarrierInputModel> carriers, CancellationToken cancellationToken);
    Task LoadAirportsAsync(IEnumerable<AirportInputModel> airports, CancellationToken cancellationToken);
}
