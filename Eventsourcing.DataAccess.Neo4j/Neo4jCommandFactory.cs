using Eventsourcing.DataAccess.Neo4j.Interfaces;
using Eventsourcing.Events.Args;
using System.Text;

namespace Eventsourcing.DataAccess.Neo4j
{
    public class Neo4jCommandFactory : INeo4jCommandFactory
    {
        public Neo4jCommand CreateInsertAirportCommand(string cityName, string airportCode, string airportName)
        {
            var query = new StringBuilder();
            query.AppendLine($"CREATE (airport:Airport {{code:$airportCode, city:$cityName, name:$airportName}}) ");
            query.AppendLine($"WITH airport");
            query.AppendLine($"MATCH (hostCity:City {{name:$cityName}})"); ;
            query.AppendLine($"CREATE (hostCity)-[:HAS_AIRPORT]->(airport);");


            var command = new Neo4jCommand
            {
                Query = query.ToString(),
                Parameters = new { 
                    airportCode = airportCode,
                    cityName = cityName,
                    airportName = airportName
                }
            };

            return command;
        }

        public Neo4jCommand CreateInsertCarrierCommand(string code, string name)
        {
            var command = new Neo4jCommand
            {
                Query = $"CREATE (n:Carrier{{code:$code, name:$name}});",
                Parameters = new { code = code, name = name }
            };

            return command;
        }

        public Neo4jCommand CreateInsertCityCommand(string cityName, string countryName)
        {
            var command = new Neo4jCommand
            {
                Query = $"CREATE (n:City{{name:$cityName, country:$countryName}});",
                Parameters = new { cityName = cityName, countryName = countryName }
            };

            return command;
        }

        public Neo4jCommand CreateInsertScheduledFlightCommand(FlightScheduledEventArgs eventArgs)
        {
            var query = new StringBuilder();
            query.AppendLine($"CREATE (flight:Flight {{code: $flightCode, carrier:$carrierCode, duration:$duration, " +
                $"source_airport_code:$sourceAirportCode, destination_airport_code:$destinationAirportCode, departure:$departure}})");
            query.AppendLine($"WITH flight");
            query.Append($"MATCH (sourceAirport:Airport {{code:$sourceAirportCode}}), ");
            query.Append($"(destinationAirport:Airport {{code:$destinationAirportCode}}), ");
            query.Append($"(carrier:Carrier {{code:$carrierCode}}) ");
            query.AppendLine($"CREATE (sourceAirport)-[:HAS_FLIGHT]->(flight)-[:FLIGHT_TO]->(destinationAirport) ");
            query.AppendLine($"CREATE (flight)-[:OPERATED_BY]->(carrier);");

            var command = new Neo4jCommand
            {
                Query = query.ToString(),
                Parameters = new
                { 
                    flightCode=eventArgs.Code,
                    carrierCode=eventArgs.CarrierCode,
                    duration=eventArgs.Duration,
                    sourceAirportCode=eventArgs.SourceAirportCode,
                    destinationAirportCode=eventArgs.DestinationAirportCode,
                    departure=eventArgs.FlightDate.ToString("s")
                }
            };

            return command;
        }

        public Neo4jCommand CreateInsertUserCommand(Guid userId, string name, string lastName, short age, string genre, string email)
        {
            var query = new StringBuilder();
            query.AppendLine($"CREATE (person:Person {{id:$userId, name:$name, lastName:$lastName, age:$age, genre:$genre, email:$email}});");

            return new Neo4jCommand
            {
                Query = query.ToString(),
                Parameters = new
                {
                    userId = userId.ToString(),
                    name = name,
                    lastName = lastName,
                    age = age,
                    genre = genre,
                    email = email
                }
            };
        }

        public Neo4jCommand CreateInsertBookingCommand(FlightBookedEventArgs eventArgs)
        {
            var query = new StringBuilder();
            query.AppendLine($"MATCH (persona:Persona{{id:$userId}})");
            query.AppendLine($"MATCH (f1:Flight{{code:$flightCode}})");
            query.AppendLine($"MERGE (persona)-[m:MADE_BOOKING]->(booking:Booking{{_id:$bookingId, booking_date:$bookingDate}})");
            query.AppendLine($"MERGE (p1:Passenger{{id:$userId, email:$userEmail}}) ON CREATE SET p1.name = $userFullname, p1.age = $userAge");
            query.AppendLine($"MERGE (j1:Journey{{_id:$journeyId, date_of_journey:$journeyDate}})-[:BY_FLIGHT]-> (f1)");
            query.AppendLine($"WITH persona, booking, j1, f1, p1");
            query.AppendLine($"MERGE (booking)-[:HAS_PASSENGER]->(p1)");
            query.AppendLine($"MERGE (booking)-[:HAS_JOURNEY]->(j1);");

            return new Neo4jCommand
            {
                Query = query.ToString(),
                Parameters = new
                {
                    userId=eventArgs.UserId,
                    flightCode = eventArgs.FlightCode,
                    bookingId = eventArgs.BookingId,
                    bookingDate = eventArgs.BookingDate,
                    userEmail = eventArgs.UserEmail,
                    userFullname = eventArgs.UserFullname,
                    userAge = eventArgs.UserAge,
                    journeyId = eventArgs.JourneyId,
                    journeyDate = eventArgs.JourneyDate
                }
            };
        }
    }
}
