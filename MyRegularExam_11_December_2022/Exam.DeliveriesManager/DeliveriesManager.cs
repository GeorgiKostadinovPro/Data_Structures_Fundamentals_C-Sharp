using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class DeliveriesManager : IDeliveriesManager
    {
        private readonly IDictionary<string, Deliverer> deliverers;
        private readonly IDictionary<string, Package> packages;

        public DeliveriesManager()
        {
            this.deliverers = new Dictionary<string, Deliverer>();
            this.packages = new Dictionary<string, Package>();
        }

        public void AddDeliverer(Deliverer deliverer)
        {
            this.deliverers.Add(deliverer.Id, deliverer);
        }

        public void AddPackage(Package package)
        {
            this.packages.Add(package.Id, package);
        }

        public void AssignPackage(Deliverer deliverer, Package package)
        {
            if (!this.Contains(deliverer))
            {
                throw new ArgumentException();
            }

            if (!this.Contains(package))
            {
                throw new ArgumentException();
            }

            deliverer.Packages.Add(package);
            package.Deliverer = deliverer;
        }

        public bool Contains(Deliverer deliverer)
        {
            return this.deliverers.ContainsKey(deliverer.Id);
        }

        public bool Contains(Package package)
        {
            return this.packages.ContainsKey(package.Id);
        }

        public IEnumerable<Deliverer> GetDeliverers()
        {
            return this.deliverers.Values;
        }

        public IEnumerable<Deliverer> GetDeliverersOrderedByCountOfPackagesThenByName()
        {
            return this.GetDeliverers()
                .OrderByDescending(d => d.Packages.Count)
                .ThenBy(d => d.Name);
        }

        public IEnumerable<Package> GetPackages()
        {
            return this.packages.Values;
        }

        public IEnumerable<Package> GetPackagesOrderedByWeightThenByReceiver()
        {
            return this.GetPackages()
                .OrderByDescending(p => p.Weight)
                .ThenBy(p => p.Receiver);
        }

        public IEnumerable<Package> GetUnassignedPackages()
        {
            return this.GetPackages()
                .Where(p => p.Deliverer == null);
        }
    }
}
