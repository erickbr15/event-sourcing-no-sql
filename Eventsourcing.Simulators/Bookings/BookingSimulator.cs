using Eventsourcing.DataAccess.Sql;
using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.Simulators.Bookings;

public class BookingSimulator : IBookingSimulator
{
    private readonly FlightDbContext _flightDbContext;
    private readonly IEventsFactory _eventsFactory;

    public BookingSimulator(FlightDbContext flightDbContext, IEventsFactory eventsFactory)
    {
        _flightDbContext = flightDbContext ?? throw new ArgumentNullException(nameof(flightDbContext));
        _eventsFactory = eventsFactory ?? throw new ArgumentNullException(nameof(eventsFactory));
    }

    public IEnumerable<IEvent<FlightBookedEventArgs>> BookingFlights()
    {
        var users = _flightDbContext.Users.ToList();
        var flights = _flightDbContext.Flights.ToList();

        var events = new List<IEvent<FlightBookedEventArgs>>();
        
        int i = 0;
        foreach (var flight in flights)
        {
            int usersInFlight = 0;
            for (; i < users.Count; i++)
            {
                var flightBookedEventArgs = new FlightBookedEventArgs
                {
                    UserId = users[i].UserId.ToString(),
                    UserAge = users[i].Age.ToString(),
                    UserEmail = users[i].Email,
                    UserFullname = $"{users[i].Name} {users[i].LastName}",
                    FlightCode = flight.Code,
                    BookingId = Guid.NewGuid().ToString(),
                    JourneyId = Guid.NewGuid().ToString(),
                    BookingDate = flight.Departure.AddDays(-15).ToString("s"),
                    JourneyDate = flight.Departure.ToString("s")
                };

                var newEvent = _eventsFactory.CreateFlightBookedEvent(flightBookedEventArgs);

                events.Add(newEvent);

                usersInFlight++;

                if(usersInFlight == 3)
                {
                    break;
                }
            }
        }

        return events;
    }
}
