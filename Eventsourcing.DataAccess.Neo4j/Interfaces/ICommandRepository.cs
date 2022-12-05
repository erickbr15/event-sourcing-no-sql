using Eventsourcing.Events.Args;

namespace Eventsourcing.DataAccess.Neo4j.Interfaces;

public interface ICommandRepository
{
    Task AddCityAsync(string cityName, string countryName);
    Task AddAirportAsync(string cityName, string airportCode, string airportName);
    Task AddCarrierAsync(string code, string name);
    Task AddScheduledFlightAsync(FlightScheduledEventArgs flightScheduledEventArgs);
    Task AddUserAsync(Guid userId, string name, string lastName, short age, string genre, string email);
    Task AddBookingAsync(FlightBookedEventArgs args);
}
