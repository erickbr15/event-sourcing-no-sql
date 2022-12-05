using Eventsourcing.BackOffice.Commands.Interfaces;
using Eventsourcing.DataAccess.Sql;
using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.BackOffice.Commands.Flights;

public class FlightCommandEventSimulator : IFlightCommandEventSimulator
{
    private readonly FlightDbContext _flightDbContext;
    private readonly IEventsFactory _flightEventsFactory;

    public FlightCommandEventSimulator(FlightDbContext flightDbContext, IEventsFactory flightEventsFactory)
    {
        _flightDbContext = flightDbContext ?? throw new ArgumentNullException(nameof(flightDbContext));
        _flightEventsFactory = flightEventsFactory ?? throw new ArgumentNullException(nameof(flightEventsFactory));
    }

    public IEnumerable<IEvent<FlightScheduledEventArgs>> ScheduleFlights()
    {
        var simulationRules = FlightSimulationRulesGenerator.GetFlightSimlationRules();        
        var scheduledFlightEvents = new List<IEvent<FlightScheduledEventArgs>>();

        foreach (var simulationRule in simulationRules)
        {
            foreach (var carrier in simulationRule.CarrierCodes)
            {
                var carrierData = _flightDbContext.Carriers.Single(c => string.Equals(c.Code, carrier));
                foreach (var sourceAirportAndTimes in simulationRule.SourceAirportCodesAndTimes)
                {
                    var possibleDestinations = simulationRule.DestinationAirportCodes
                        .Where(code => !string.Equals(code, sourceAirportAndTimes.Key))
                        .ToList();

                    IList<string> destinations = new List<string>();

                    while (destinations.Count < sourceAirportAndTimes.Value.Count)
                    {
                        int destinationIndex = Random.Shared.Next(0, possibleDestinations.Count() - 1);
                        if (!destinations.Contains(possibleDestinations[destinationIndex]))
                        {
                            destinations.Add(possibleDestinations[destinationIndex]);
                        }
                    }

                    int destIndex = 0;
                    foreach (var departureTime in sourceAirportAndTimes.Value)
                    {
                        var scheduledFlightEventArgs = new FlightScheduledEventArgs
                        {
                            CarrierCode = carrier,
                            CarrierName = carrierData.Name,
                            Code = $"{carrierData.Code}{Random.Shared.Next(100, 9999)}",
                            Duration = 110,
                            FlightId = Guid.NewGuid(),
                            FlightDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, departureTime.Hour, departureTime.Minute, 0),
                            SourceAirportCode = sourceAirportAndTimes.Key,
                            DestinationAirportCode = destinations[destIndex++]
                        };

                        var newScheduledFlightEvent = _flightEventsFactory.CreateFlightScheduledEvent(scheduledFlightEventArgs);
                        scheduledFlightEvents.Add(newScheduledFlightEvent);
                    }
                }
            }
        }        

        return scheduledFlightEvents;
    }
}
