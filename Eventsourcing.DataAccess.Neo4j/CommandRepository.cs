using Eventsourcing.DataAccess.Neo4j.Interfaces;
using Eventsourcing.Events.Args;

namespace Eventsourcing.DataAccess.Neo4j;

public class CommandRepository : ICommandRepository
{
    private readonly INeo4jCommandFactory _commandFactory;

    public CommandRepository(INeo4jCommandFactory commandFactory)
    {
        _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
    }

    public async Task AddCityAsync(string cityName, string countryName)
    {
        var command = _commandFactory.CreateInsertCityCommand(cityName, countryName);
        await Neo4jConnectionDriver.Neo4JDriver.WriteTransactionAsync(command);
    }

    public async Task AddAirportAsync(string cityName, string airportCode, string airportName)
    {
        var command = _commandFactory.CreateInsertAirportCommand(cityName, airportCode, airportName);
        await Neo4jConnectionDriver.Neo4JDriver.WriteTransactionAsync(command);
    }

    public async Task AddCarrierAsync(string code, string name)
    {
        var command = _commandFactory.CreateInsertCarrierCommand(code, name);
        await Neo4jConnectionDriver.Neo4JDriver.WriteTransactionAsync(command);
    }

    public async Task AddScheduledFlightAsync(FlightScheduledEventArgs flightScheduledEventArgs)
    {
        var command = _commandFactory.CreateInsertScheduledFlightCommand(flightScheduledEventArgs);
        await Neo4jConnectionDriver.Neo4JDriver.WriteTransactionAsync(command);
    }

    public async Task AddUserAsync(Guid userId, string name, string lastName, short age, string genre, string email)
    {
        var command = _commandFactory.CreateInsertUserCommand(userId, name, lastName, age, genre, email);
        await Neo4jConnectionDriver.Neo4JDriver.WriteTransactionAsync(command);
    }

    public async Task AddBookingAsync(FlightBookedEventArgs args)
    {
        var command = _commandFactory.CreateInsertBookingCommand(args);
        await Neo4jConnectionDriver.Neo4JDriver.WriteTransactionAsync(command);
    }
}
