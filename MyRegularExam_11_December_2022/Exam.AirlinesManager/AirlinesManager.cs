using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class AirlinesManager : IAirlinesManager
    {
        private readonly IDictionary<string, Airline> airlines;
        private readonly IDictionary<string, Flight> flights;

        public AirlinesManager()
        {
            this.airlines= new Dictionary<string, Airline>();
            this.flights = new Dictionary<string, Flight>();
        }   

        public void AddAirline(Airline airline)
        {
            this.airlines.Add(airline.Id, airline);
        }

        public void AddFlight(Airline airline, Flight flight)
        {
            if (!this.Contains(airline))
            {
                throw new ArgumentException();
            }

            this.flights.Add(flight.Id, flight);
            airline.Flights.Add(flight);
            flight.Airline = airline;
        }

        public bool Contains(Airline airline)
        {
            return this.airlines.ContainsKey(airline.Id);
        }

        public bool Contains(Flight flight)
        {
            return this.flights.ContainsKey(flight.Id);
        }

        public void DeleteAirline(Airline airline)
        {
            if (!this.Contains(airline))
            {
                throw new ArgumentException();
            }

            foreach (Flight flight in airline.Flights)
            {
                this.flights.Remove(flight.Id);
            }

            this.airlines.Remove(airline.Id);
        }

        public IEnumerable<Airline> GetAirlinesOrderedByRatingThenByCountOfFlightsThenByName()
        {
            return this.airlines.Values
                .OrderByDescending(a => a.Rating)
                .ThenByDescending(a => a.Flights.Count)
                .ThenBy(a => a.Name);
        }

        public IEnumerable<Airline> GetAirlinesWithFlightsFromOriginToDestination(string origin, string destination)
        {
            return this.airlines.Values
                .Where(a => a.Flights
                .Any(f => f.IsCompleted == false && f.Origin == origin && f.Destination == destination));
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return this.flights.Values;
        }

        public IEnumerable<Flight> GetCompletedFlights()
        {
            return this.GetAllFlights()
                .Where(f => f.IsCompleted == true);
        }

        public IEnumerable<Flight> GetFlightsOrderedByCompletionThenByNumber()
        {
            //var uncompletedFlight = this.GetAllFlights()
            //    .Where(f => f.IsCompleted == false)
            //    .OrderBy(f => f.Number)
            //    .ToList();

            //var completedFlights = this.GetAllFlights()
            //    .Where(f => f.IsCompleted == true)
            //    .OrderBy(f => f.Number)
            //    .ToList();

            //var result = new List<Flight>();

            //result.AddRange(uncompletedFlight);
            //result.AddRange(completedFlights);

            return this.GetAllFlights()
                .OrderBy(f => f.IsCompleted)
                .ThenBy(f => f.Number);
        }

        public Flight PerformFlight(Airline airline, Flight flight)
        {
            if (!this.Contains(airline))
            {
                throw new ArgumentException();
            }

            if (!this.Contains(flight))
            {
                throw new ArgumentException();
            }

            flight.IsCompleted = true;

            return flight;
        }
    }
}
