using Eventsourcing.DataAccess.Neo4j.Interfaces;
using Eventsourcing.DataAccess.Sql;
using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.Application.Neo4j.EventProcessors;

public class ScheduledFlightEventProcessor : IEventProcessor<IEvent<FlightScheduledEventArgs>>
{
    private static readonly object _syncRoot = new object();
    private readonly ICommandRepository _commandRepository;
    private readonly FlightDbContext _flightDbContext;

    public ScheduledFlightEventProcessor(ICommandRepository commandRepository, FlightDbContext flightDbContext)
    {
        _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
        _flightDbContext = flightDbContext ?? throw new ArgumentNullException(nameof(flightDbContext));
    }

    public event EventHandler<ProcessFinalizedEventArgs> OnProcessFinalized;

    event EventHandler<ProcessFinalizedEventArgs> IEventProcessor<IEvent<FlightScheduledEventArgs>>.ProcessFinalized
    {
        add
        {
            lock (_syncRoot)
            {
                OnProcessFinalized += value;
            }
        }

        remove
        {
            lock (_syncRoot)
            {
                OnProcessFinalized -= value;
            }
        }
    }

    public async Task ProcessAsync(IEvent<FlightScheduledEventArgs> eventToProcess)
    {
        await _commandRepository.AddScheduledFlightAsync(eventToProcess.EventArgs);

        var flightDomain = new Domain.Flight
        {
            FlightId = eventToProcess.EventArgs.FlightId,
            Code = eventToProcess.EventArgs.Code,
            Duration = eventToProcess.EventArgs.Duration,
            Departure = eventToProcess.EventArgs.FlightDate,
            Arrival = new DateTime(
                    eventToProcess.EventArgs.FlightDate.Year,
                    eventToProcess.EventArgs.FlightDate.Month,
                    eventToProcess.EventArgs.FlightDate.Day).AddMinutes(eventToProcess.EventArgs.Duration)
        };

        var sourceAirport = _flightDbContext.Airports.SingleOrDefault(a => a.Code == eventToProcess.EventArgs.SourceAirportCode);
        var destinationAirport = _flightDbContext.Airports.SingleOrDefault(a => a.Code == eventToProcess.EventArgs.DestinationAirportCode);
        var carrier = _flightDbContext.Carriers.SingleOrDefault(a => a.Code == eventToProcess.EventArgs.CarrierCode);

        flightDomain.SourceAirportId = sourceAirport.Id;
        flightDomain.DestinationAirportId = destinationAirport.Id;
        flightDomain.CarrierId = carrier.Id;

        _flightDbContext.Flights.Add(flightDomain);

        await _flightDbContext.SaveChangesAsync();
    }
}
