using Eventsourcing.DataAccess.Neo4j.Interfaces;
using Eventsourcing.DataLoader.Interfaces;
using Eventsourcing.DataLoader.Model;

namespace Eventsourcing.DataLoader;

public class GraphDataLoader : IGraphDataLoader
{
    private readonly ICommandRepository _commandRepository;

    public GraphDataLoader(ICommandRepository commandRepository)
    {
        _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
    }

    public async Task LoadAirportsAsync(IEnumerable<AirportInputModel> airports, CancellationToken cancellationToken)
    {
        if (airports is null || airports.Any() == false)
        {
            return;
        }

        foreach (var airport in airports)
        {
            await _commandRepository.AddAirportAsync(airport.CityName, airport.Code, airport.Name);
        }
    }

    public async Task LoadCarriersAsync(IEnumerable<CarrierInputModel> carriers, CancellationToken cancellationToken)
    {
        if (carriers is null || carriers.Any() == false)
        {
            return;
        }

        foreach (var carrier in carriers)
        {
            await _commandRepository.AddCarrierAsync(carrier.Code, carrier.Name);
        }
    }

    public async Task LoadCitiesAsync(IEnumerable<CityInputModel> cities, CancellationToken cancellationToken)
    {
        if (cities is null || cities.Any() == false)
        {
            return;
        }
        foreach (var city in cities)
        {
            await _commandRepository.AddCityAsync(city.Name, city.CountryName);
        }
    }    
}
