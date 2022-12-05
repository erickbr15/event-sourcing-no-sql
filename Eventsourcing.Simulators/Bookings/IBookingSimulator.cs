using Eventsourcing.Events.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventsourcing.Events.Interfaces;

namespace Eventsourcing.Simulators.Bookings
{
    public interface IBookingSimulator
    {
        IEnumerable<IEvent<FlightBookedEventArgs>> BookingFlights();
    }
}
