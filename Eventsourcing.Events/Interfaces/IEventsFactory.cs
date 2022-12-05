using Eventsourcing.Events.Args;

namespace Eventsourcing.Events.Interfaces;

public interface IEventsFactory
{
    IEvent<UserRegisteredEventArgs> CreateUserRegisteredEvent(UserRegisteredEventArgs userRegisteredArgs);
    IEvent<FlightScheduledEventArgs> CreateFlightScheduledEvent(FlightScheduledEventArgs scheduledFlightArgs);
    IEvent<FlightBookedEventArgs> CreateFlightBookedEvent(FlightBookedEventArgs flightBookedArgs);
    
}
