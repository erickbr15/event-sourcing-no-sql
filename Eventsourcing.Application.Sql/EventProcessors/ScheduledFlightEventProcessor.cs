using Eventsourcing.DataAccess.Sql;
using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.Application.Sql.EventProcessors
{
    public class ScheduledFlightEventProcessor : IEventProcessor<IEvent<FlightScheduledEventArgs>>
    {
        private static readonly object _syncRoot = new object();
        private readonly FlightDbContext _flightDbContext;

        public ScheduledFlightEventProcessor(FlightDbContext flightDbContext)
        {
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
            
            var sourceAirport = _flightDbContext.Airports.SingleOrDefault(a=>string.Equals(a.Code, eventToProcess.EventArgs.SourceAirportCode, StringComparison.OrdinalIgnoreCase));
            var destinationAirport = _flightDbContext.Airports.SingleOrDefault(a => string.Equals(a.Code, eventToProcess.EventArgs.DestinationAirportCode, StringComparison.OrdinalIgnoreCase));
            var carrier = _flightDbContext.Carriers.SingleOrDefault(a => string.Equals(a.Code, eventToProcess.EventArgs.CarrierCode, StringComparison.OrdinalIgnoreCase));

            flightDomain.SourceAirportId = sourceAirport.Id;
            flightDomain.DestinationAirportId = destinationAirport.Id;
            flightDomain.CarrierId = carrier.Id;

            _flightDbContext.Flights.Add(flightDomain);
            await _flightDbContext.SaveChangesAsync();
        }
    }
}