using Eventsourcing.DataAccess.Sql;
using Eventsourcing.DataLoader.Events;
using Eventsourcing.DataLoader.Interfaces;
using Eventsourcing.DataLoader.Model;
using Eventsourcing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Eventsourcing.DataLoader;

public class SqlDataLoader : ISqlDataLoader
{
    private static object _syncRoot = new object();        
    private readonly FlightDbContext _flightDbContext;
    
    public SqlDataLoader(FlightDbContext flightDbContext)
    {
        _flightDbContext = flightDbContext ?? throw new ArgumentNullException(nameof(flightDbContext));
    }

    public event EventHandler<EntityAddedToContextEventArgs> OnEntityAddedToContext;
    
    event EventHandler<EntityAddedToContextEventArgs> ISqlDataLoader.EntityAddedToContext
    {
        add
        {
            lock (_syncRoot)
            {
                OnEntityAddedToContext += value;
            }
        }

        remove
        {
            lock (_syncRoot)
            {
                OnEntityAddedToContext -= value;
            }
        }
    }

    public async Task CleanupCatalogsAsync(CancellationToken cancellationToken)
    {
        var sqlInsertCommand = $"EXEC [dbo].usp_DeleteCatalogs;";
        await _flightDbContext.Database.ExecuteSqlRawAsync(sqlInsertCommand, cancellationToken);
    }

    public async Task LoadAirportsAsync(IEnumerable<AirportInputModel> airports, CancellationToken cancellationToken)
    {
        var cities = await _flightDbContext.Cities.ToListAsync(cancellationToken);

        foreach (var input in airports)
        {
            var city = cities.SingleOrDefault(c => string.Equals(c.Name, input.CityName, StringComparison.OrdinalIgnoreCase));

            if (city is null)
            {
                continue;
            }

            var airport = new Airport
            {
                Id = Guid.NewGuid(),
                Name = input.Name,
                Code = input.Code,
                CityCode = city.Code
            };
            await _flightDbContext.Airports.AddAsync(airport, cancellationToken);

            PublishEvent(new EntityAddedToContextEventArgs(airport.ToString()));
        }

        await _flightDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task LoadBookingStatusesAsync(IEnumerable<BookingStatusInputModel> bookingStatuses, CancellationToken cancellationToken)
    {            
        foreach (var item in bookingStatuses)
        {
            var sqlInsertCommand = $"INSERT INTO [dbo].[BookingStatuses](Id, Name) VALUES({item.Id}, '{item.Name}');";
            await _flightDbContext.Database.ExecuteSqlRawAsync(sqlInsertCommand, cancellationToken);
        }
    }

    public async Task LoadCarriersAsync(IEnumerable<CarrierInputModel> carriers, CancellationToken cancellationToken)
    {
        foreach (var item in carriers)
        {
            var carrier = new Carrier
            {
                Id = Guid.NewGuid(),
                Code = item.Code,
                Name = item.Name
            };

            await _flightDbContext.Carriers.AddAsync(carrier, cancellationToken);
            PublishEvent(new EntityAddedToContextEventArgs(carrier.ToString()));
        }

        await _flightDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task LoadCitiesAsync(IEnumerable<CityInputModel> cities, CancellationToken cancellationToken)
    {            
        var countries = _flightDbContext.Countries.ToList();

        foreach (var item in cities)
        {
            var country = countries.SingleOrDefault(c => string.Equals(c.Name, item.CountryName, StringComparison.OrdinalIgnoreCase));
            if (country is null)
            {
                continue;
            }

            var newCity = new City
            {
                Code = Guid.NewGuid(),
                CountryCode = country.Code,
                Name = item.Name
            };

            await _flightDbContext.Cities.AddAsync(newCity, cancellationToken);
            PublishEvent(new EntityAddedToContextEventArgs(newCity.ToString()));
        }

        await _flightDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task LoadCountriesAsync(IEnumerable<CountryInputModel> countries, CancellationToken cancellationToken)
    {
        foreach (var item in countries)
        {
            var newCountry = new Country
            {
                Code = Guid.NewGuid(),
                Name = item.Name
            };

            await _flightDbContext.Countries.AddAsync(newCountry, cancellationToken);
            PublishEvent(new EntityAddedToContextEventArgs(newCountry.ToString()));
        }

        await _flightDbContext.SaveChangesAsync(cancellationToken);
    }    

    private void PublishEvent(EntityAddedToContextEventArgs eventArgs)
    {
        OnEntityAddedToContext?.Invoke(this, eventArgs);
    }
}
