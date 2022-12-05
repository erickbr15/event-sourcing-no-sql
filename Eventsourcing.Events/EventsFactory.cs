using Eventsourcing.Events.Args;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.Events;

public class EventsFactory : IEventsFactory
{
    public IEvent<FlightBookedEventArgs> CreateFlightBookedEvent(FlightBookedEventArgs flightBookedArgs)
    {
        if (flightBookedArgs is null)
        {
            return null;
        }

        return new FlightBookedEvent
        {
            Topic = "FLIGHTS.NEW.BOOKING",
            Subject = "ADD.NEW.FLIGHT.BOOKING",
            Id = flightBookedArgs.BookingId,
            EventTime = DateTime.Now.ToString("s"),
            EventArgs= flightBookedArgs,
            DataVersion = $"{DateTime.Now.ToUniversalTime().ToString("s")}.{flightBookedArgs}"
        };
    }

    public IEvent<FlightScheduledEventArgs> CreateFlightScheduledEvent(FlightScheduledEventArgs scheduledFlightArgs)
    {
        if (scheduledFlightArgs is null)
        {
            return null;
        }

        return new FlightScheduledEvent
        {
            Topic = "FLIGHTS.NEW.SCHEDULED.FLIGHT",
            Subject = "ADD.NEW.SCHEDULED.FLIGHT",
            Id = scheduledFlightArgs.FlightId.ToString(),
            EventTime = DateTime.Now.ToUniversalTime().ToString("u"),
            EventArgs = scheduledFlightArgs,
            DataVersion = $"{DateTime.Now.ToUniversalTime().ToString("s")}.{scheduledFlightArgs}"
        };
    }

    public IEvent<UserRegisteredEventArgs> CreateUserRegisteredEvent(UserRegisteredEventArgs userRegisteredArgs)
    {
        if (userRegisteredArgs is null)
        {
            return null;
        }

        return new UserRegisteredEvent
        {
            Topic = "USER.NEW.REGISTRATION",
            Subject = "ADD.NEW.USER",
            Id = userRegisteredArgs.UserId.ToString(),
            EventTime = DateTime.Now.ToUniversalTime().ToString("u"),
            EventArgs = userRegisteredArgs,
            DataVersion = $"{DateTime.Now.ToUniversalTime().ToString("s")}.{userRegisteredArgs}"
        };
    }
}
