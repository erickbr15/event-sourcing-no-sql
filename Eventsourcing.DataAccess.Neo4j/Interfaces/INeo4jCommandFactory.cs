using Eventsourcing.Events.Args;

namespace Eventsourcing.DataAccess.Neo4j.Interfaces;

public interface INeo4jCommandFactory
{
    Neo4jCommand CreateInsertCityCommand(string cityName, string countryName);
    Neo4jCommand CreateInsertAirportCommand(string cityName, string airportCode, string airportName);
    Neo4jCommand CreateInsertCarrierCommand(string code, string name);
    Neo4jCommand CreateInsertScheduledFlightCommand(FlightScheduledEventArgs eventArgs);
    Neo4jCommand CreateInsertUserCommand(Guid userId, string name, string lastName, short age, string genre, string email);
    Neo4jCommand CreateInsertBookingCommand(FlightBookedEventArgs eventArgs);

}
