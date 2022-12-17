using System.Collections.Generic;

namespace Exam.DeliveriesManager
{
    public class Deliverer
    {
        private Deliverer() 
        { 
            this.Packages = new HashSet<Package>();
        }

        public Deliverer(string id, string name)
            : this()
        {
            this.Id = id;
            this.Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<Package> Packages { get; set; }
    }
}
