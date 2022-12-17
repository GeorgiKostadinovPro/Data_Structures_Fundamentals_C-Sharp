using System.Collections.Generic;

namespace Exam.DeliveriesManager
{
    public class Airline
    {
        private Airline()
        {
            this.Flights = new HashSet<Flight>();
        }

        public Airline(string id, string name, double rating)
            : this()
        {
            this.Id = id;
            this.Name = name;
            this.Rating = rating;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }

        public ICollection<Flight> Flights { get; set; }
    }
}
