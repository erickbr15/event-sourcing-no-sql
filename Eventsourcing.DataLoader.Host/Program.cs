// See https://aka.ms/new-console-template for more information
using Eventsourcing.DataAccess.Neo4j;
using Eventsourcing.DataAccess.Neo4j.Interfaces;
using Eventsourcing.DataAccess.Sql;
using Eventsourcing.DataLoader;
using Eventsourcing.DataLoader.Host;
using Eventsourcing.DataLoader.Interfaces;
using Eventsourcing.DataLoader.Mappers;
using Eventsourcing.DataLoader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfiguration configuration = configBuilder.Build();

using IHost host = Host.CreateDefaultBuilder(args)    
    .ConfigureServices((_, services) => {
        services.AddDbContext<FlightDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlFlights")));
        services.AddTransient<ITextToInputModelMapper<CountryInputModel>, CountryTextInputModelMapper>();
        services.AddTransient<ITextToInputModelMapper<CityInputModel>, CityTextInputModelMapper>();
        services.AddTransient<ITextToInputModelMapper<AirportInputModel>, AirportTextToInputModelMapper>();
        services.AddTransient<ITextToInputModelMapper<BookingStatusInputModel>, BookingStatusTextToInputModelMapper>();
        services.AddTransient<ITextToInputModelMapper<CarrierInputModel>, CarrierTextToInputModelMapper>();
        services.AddTransient<ITextCatalogMapper, TextCatalogMapper>();
        services.AddTransient<INeo4jCommandFactory, Neo4jCommandFactory>();
        services.AddTransient<ICommandRepository, CommandRepository>();
        services.AddTransient<ISqlDataLoader, SqlDataLoader>();
        services.AddTransient<IGraphDataLoader, GraphDataLoader>();
        services.AddTransient<IDataLoaderFacade, DataLoaderFacade>();
    })
    .Build();

using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;

var dataLoader = provider.GetRequiredService<IDataLoaderFacade>();

var catalogDataSourceOptions = new TextCatalogDataSourceOptions(configuration);
var catalogDataSource = new TextCatalogDataSource(catalogDataSourceOptions);

var neo4jConnectionOptionsSection = configuration.GetRequiredSection("Neo4jConnectionOptions");
var neo4jConnectionOptions = new Neo4jConnectionOptions
{
    Uri = neo4jConnectionOptionsSection.GetValue<string>("Uri") ?? string.Empty,
    User = neo4jConnectionOptionsSection.GetValue<string>("User") ?? string.Empty,
    Password = neo4jConnectionOptionsSection.GetValue<string>("Password") ?? string.Empty,
    Database = neo4jConnectionOptionsSection.GetValue<string>("Database") ?? string.Empty,
};

await Neo4jConnectionDriver.Neo4JDriver.InitDriverAsync(neo4jConnectionOptions);

await dataLoader.CreateCatalogsInSqlAsync(catalogDataSource, cancellationToken: default);
await dataLoader.CreateCatalogsInNeo4jAsync(catalogDataSource, cancellationToken: default);

await host.RunAsync();
