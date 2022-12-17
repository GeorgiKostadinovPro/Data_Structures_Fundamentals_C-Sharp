using System;
using System.Collections.Generic;

namespace Exam.DeliveriesManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IDeliveriesManager deliveriesManager = new DeliveriesManager();

            Package package1 = new Package("1", "George", "Lozenets Sofia, Bulgaria", "0879454529", 1.5);
            Package package2 = new Package("2", "Lyubo", "Mladost 1A Sofia, Bulgaria", "0878465789", 2.5);
            Package package3 = new Package("3", "Kristyan", "Vitoshka Smolyan, Bulgaria", "0878463456", 3.5);

            Deliverer deliverer = new Deliverer("1", "Amazon");

            deliveriesManager.AddPackage(package1);
            deliveriesManager.AddPackage(package2);
            deliveriesManager.AddPackage(package3);

            deliveriesManager.AddDeliverer(deliverer);

            deliveriesManager.AssignPackage(deliverer, package1);
            deliveriesManager.AssignPackage(deliverer, package2);
            deliveriesManager.AssignPackage(deliverer, package3);

            IEnumerable<Package> packages = deliveriesManager.GetPackagesOrderedByWeightThenByReceiver();

            foreach (Package package in packages)
            {
                Console.WriteLine(package.Id);
            }
        }
    }
}
