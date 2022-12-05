// See https://aka.ms/new-console-template for more information

using Eventsourcing.DataAccess.Neo4j;
using Eventsourcing.DataAccess.Neo4j.Interfaces;
using Eventsourcing.DataAccess.Sql;
using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Eventsourcing.DataAccess.MongoDb;
using Eventsourcing.Simulators.Bookings;
using Eventsourcing.Events;
using Eventsourcing.BackOffice.Commands;
using Eventsourcing.BackOffice.Commands.Interfaces;
using Eventsourcing.BackOffice.Commands.Flights;

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfiguration configuration = configBuilder.Build();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => {
        services.AddDbContext<FlightDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlFlights")));        
        services.AddTransient<INeo4jCommandFactory, Neo4jCommandFactory>();
        services.AddTransient<ICommandRepository, CommandRepository>(); 
        services.AddSingleton<IEventsFactory, EventsFactory>();
        services.AddTransient<IFlightCommandEventSimulator, FlightCommandEventSimulator>();
        services.AddTransient<IBackOfficeCommands, BackOfficeCommands>();
        services.AddTransient<IBookingSimulator, BookingSimulator>();
        services.AddScoped<IMongoDataService, MongoDataService>();
        services.AddTransient<IEventProcessor<IEvent<FlightScheduledEventArgs>>, Eventsourcing.Application.Neo4j.EventProcessors.ScheduledFlightEventProcessor>();
        services.AddTransient<IEventProcessor<IEvent<FlightBookedEventArgs>>, Eventsourcing.Application.Neo4j.EventProcessors.BookingEventProcessor>();
    })
    .Build();


using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;

var neo4jConnectionOptionsSection = configuration.GetRequiredSection("Neo4jConnectionOptions");
var neo4jConnectionOptions = new Neo4jConnectionOptions
{
    Uri = neo4jConnectionOptionsSection.GetValue<string>("Uri") ?? string.Empty,
    User = neo4jConnectionOptionsSection.GetValue<string>("User") ?? string.Empty,
    Password = neo4jConnectionOptionsSection.GetValue<string>("Password") ?? string.Empty,
    Database = neo4jConnectionOptionsSection.GetValue<string>("Database") ?? string.Empty,
};

await Neo4jConnectionDriver.Neo4JDriver.InitDriverAsync(neo4jConnectionOptions);

var flightSimulator = provider.GetRequiredService<IBackOfficeCommands>();
var bookingSimulator = provider.GetRequiredService<IBookingSimulator>();

var eventContainer = provider.GetRequiredService<IMongoDataService>();
var flightTopicProcessor = provider.GetRequiredService<IEventProcessor<IEvent<FlightScheduledEventArgs>>>();

var bookingTopicProcessor = provider.GetRequiredService<IEventProcessor<IEvent<FlightBookedEventArgs>>>();

flightTopicProcessor.ProcessFinalized += Processor_ProcessFinalized;

var flightEvents = flightSimulator.ScheduleFlights();

foreach (var simEvent in flightEvents)
{
    await eventContainer.InsertEventAsync(simEvent, cancellationToken: default);
    await flightTopicProcessor.ProcessAsync(simEvent);
}



await host.RunAsync();

void Processor_ProcessFinalized(object? sender, ProcessFinalizedEventArgs e)
{
    Console.WriteLine(e.ToString());
}