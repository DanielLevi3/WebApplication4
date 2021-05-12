using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class FlightController : ApiController
    {
        public long Id { get; set; }
        public string OriginCountry { get; set; }
        public string DestCountry { get; set; }
        public long Remaining { get; set; }

        public static List<Flights> flights =new List<Flights>();

        static FlightController()
        {
           /*
             flights.Add(new Flights
             {
                 Id = 1,
                 OriginCountry = "Israel",
                 DestCountry = "Istanbul",
                 Remaining = 270
             });
             flights.Add(new Flights
             {
                 Id = 2,
                 OriginCountry = "USA",
                 DestCountry = "Japan",
                 Remaining = 3
             });
             flights.Add(new Flights
             {
                 Id = 3,
                OriginCountry = "Denemark",
                 DestCountry = "Sweden",
                 Remaining = 482
             });
             flights.Add(new Flights
             {
                 Id = 4,
                 OriginCountry = "Korea",
                 DestCountry = "Taiwan",
                 Remaining = 12
             });
          
            */
        }

        // GET api/flights all flights
        public IEnumerable<Flights> Get()
        {
            
            List<Flights> real_flights = new List<Flights>();
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source = C:\\Users\\levid\\09052021SQLite.db; Version = 3;"))
            {
                conn.Open();
                using (SQLiteCommand select_query = new SQLiteCommand("SELECT * from Flights", conn))
                {
                    using (SQLiteDataReader reader = select_query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flights f = new Flights()
                            {
                                Id = (long)reader["ID"],
                                DestCountry = reader["DestinationCountry"].ToString(),
                                OriginCountry = reader["OriginCountry"].ToString(),
                                Remaining = (long)reader["Remaining"]
                            };
                            real_flights.Add(f);
                        }
                    }
                }
            }
            return real_flights;
           
        }

        // GET api/flights/5 flight by id
        public Flights Get(int id)
        {
            Flights f = new Flights();
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source = C:\\Users\\levid\\09052021SQLite.db; Version = 3;"))
            {
                conn.Open();
                using (SQLiteCommand select_query = new SQLiteCommand($"SELECT * from Flights where ID={id}", conn))
                {
                    using (SQLiteDataReader reader = select_query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                                f.Id = (long)reader["ID"];
                                f.DestCountry = reader["DestinationCountry"].ToString();
                                f.OriginCountry = reader["OriginCountry"].ToString();
                                f.Remaining = (long)reader["Remaining"];
                         }
                    }
                }
            }
            return f;
        }

        // POST api/flights -- add flight
        public void Post([FromBody] Flights flight)
        {
            
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source = C:\\Users\\levid\\09052021SQLite.db; Version = 3;"))
            {
                conn.Open();
                using (SQLiteCommand select_query = new SQLiteCommand($"INSERT INTO Flights(ID,OriginCountry,DestinationCountry,Remaining) VALUES({flight.Id},'{flight.OriginCountry}','{flight.DestCountry}',{flight.Remaining})", conn))
                {
                    select_query.ExecuteNonQuery();
                }
            }
        }

    // PUT api/flights/5 -- update flight
    public void Put(int id, [FromBody] Flights flight)
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source = C:\\Users\\levid\\09052021SQLite.db; Version = 3;"))
            {
                conn.Open();
                using (SQLiteCommand select_query = new SQLiteCommand($" UPDATE Flights SET OriginCountry = '{flight.OriginCountry}', DestinationCountry = '{flight.DestCountry}', Remaining = {flight.Remaining} WHERE ID = {flight.Id}", conn))
                {
                    select_query.ExecuteNonQuery();
                }
            }
           
        }

        // DELETE api/flights/5 -- delete flight
        public void Delete(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source = C:\\Users\\levid\\09052021SQLite.db; Version = 3;"))
            {
                conn.Open();
                using (SQLiteCommand select_query = new SQLiteCommand($" DELETE FROM Flights WHERE ID={id}", conn))
                {
                    select_query.ExecuteNonQuery();
                }
            }
        }
    }
}


