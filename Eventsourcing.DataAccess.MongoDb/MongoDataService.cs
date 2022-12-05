using Eventsourcing.Events.Args;
using MongoDB.Driver;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.DataAccess.MongoDb;

public class MongoDataService : IMongoDataService
{
    private static string _ConnectionString = "mongodb://localhost:27017";
    private static string _DatabaseName = "eventsource-poc";
    private static string _Collection = "flight-events";

    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    /// <summary>
    ///     Creates an instance of <see cref="MongoDataService"/>
    /// </summary>
    public MongoDataService()
    {
        _client = new MongoClient(_ConnectionString);
        _database = _client.GetDatabase(_DatabaseName);
    }

    public async Task<IEvent<FlightScheduledEventArgs>> InsertEventAsync(IEvent<FlightScheduledEventArgs> flightScheduledEvent, CancellationToken cancellationToken)
    {
        if (flightScheduledEvent is null)
        {
            return null;
        }

        var events = _database.GetCollection<IEvent<FlightScheduledEventArgs>>(_Collection);
        await events.InsertOneAsync(flightScheduledEvent);
        

        return flightScheduledEvent;
    }

    public async Task<IEvent<FlightBookedEventArgs>> InsertEventAsync(IEvent<FlightBookedEventArgs> bookingEvent, CancellationToken cancellationToken)
    {
        if (bookingEvent is null)
        {
            return null;
        }

        var events = _database.GetCollection<IEvent<FlightBookedEventArgs>>(_Collection);
        await events.InsertOneAsync(bookingEvent);


        return bookingEvent;
    }
}
