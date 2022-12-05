using Eventsourcing.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventsourcing.DataAccess.Sql;

public interface IFlightDbContext
{    
    DbSet<Country> Countries { get; set; }
    DbSet<City> Cities { get; set; }
    DbSet<Airport> Airports { get; set; }
    DbSet<Carrier> Carriers { get; set; }
    DbSet<Flight> Flights { get; set; }
    DbSet<Booking> Bookings { get; set; }
    DbSet<User> Users { get; set; }
}
