using Eventsourcing.DataLoader.Events;
using Eventsourcing.DataLoader.Model;

namespace Eventsourcing.DataLoader.Interfaces;

public interface ISqlDataLoader
{
    event EventHandler<EntityAddedToContextEventArgs> EntityAddedToContext;

    Task CleanupCatalogsAsync(CancellationToken cancellationToken);
    Task LoadCountriesAsync(IEnumerable<CountryInputModel> countries, CancellationToken cancellationToken);
    Task LoadCitiesAsync(IEnumerable<CityInputModel> cities, CancellationToken cancellationToken);    
    Task LoadAirportsAsync(IEnumerable<AirportInputModel> airports, CancellationToken cancellationToken);
    Task LoadCarriersAsync(IEnumerable<CarrierInputModel> carriers, CancellationToken cancellationToken);
    Task LoadBookingStatusesAsync(IEnumerable<BookingStatusInputModel> bookingStatuses, CancellationToken cancellationToken);
}
