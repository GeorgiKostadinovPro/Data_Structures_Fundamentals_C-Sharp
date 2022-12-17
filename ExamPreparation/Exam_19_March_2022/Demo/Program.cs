using System;
using System.Linq;
using VaccOps;
using VaccOps.Models;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            VaccDb db = new VaccDb();

            Doctor d1 = new Doctor("d1", 3);

            Patient p1 = new Patient("p1", 1, 13, "Smolyan");

            db.AddDoctor(d1);
            db.AddPatient(d1, p1);

            var patiens = db.GetPatients();

            Console.WriteLine(patiens.Count());
        }
    }
}
