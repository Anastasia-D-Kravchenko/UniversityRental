using System;
using System.Linq;
using UniversityRental.Models;
using UniversityRental.Services;
using UniversityRental.Exceptions;

namespace UniversityRental
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new RentalService();

            Console.WriteLine("--- DEMONSTRATION SCENARIO ---\n");

            var laptop = new Laptop("Dell XPS 15", 16, "Windows 11");
            var projector = new Projector("Epson HD", "1080p", 3000);
            var camera = new Camera("Sony A7 III", 24, "50mm");
            var brokenLaptop = new Laptop("Old ThinkPad", 8, "Windows 10");
            
            service.AddEquipment(laptop);
            service.AddEquipment(projector);
            service.AddEquipment(camera);
            service.AddEquipment(brokenLaptop);

            service.MarkAsUnavailable(brokenLaptop);

            var student = new Student("Alice", "Smith");
            var employee = new Employee("Dr. Bob", "Jones");
            
            service.AddUser(student);
            service.AddUser(employee);

            Console.WriteLine("-> Performing valid rentals...");
            var rental1 = service.RentEquipment(student, laptop, 7);
            var rental2 = service.RentEquipment(student, camera, 7);
            Console.WriteLine($"Rented {laptop.Name} and {camera.Name} to {student.FirstName}.");

            Console.WriteLine("\n-> Attempting invalid operations...");
            try
            {
                service.RentEquipment(student, projector, 3);
            }
            catch (RentalLimitExceededException ex)
            {
                Console.WriteLine($"EXPECTED ERROR: {ex.Message}");
            }

            try
            {
                service.RentEquipment(employee, brokenLaptop, 3);
            }
            catch (EquipmentUnavailableException ex)
            {
                Console.WriteLine($"EXPECTED ERROR: {ex.Message}");
            }

            Console.WriteLine("\n-> Returning equipment on time...");
            service.ReturnEquipment(rental1); 
            Console.WriteLine($"{rental1.RentedEquipment.Name} returned on time. Penalty: ${rental1.Penalty}");

            Console.WriteLine("\n-> Simulating a late return...");
            var lateRental = service.RentEquipment(employee, projector, 5, DateTime.Now.AddDays(-10));
            service.ReturnEquipment(lateRental, DateTime.Now);
            Console.WriteLine($"{lateRental.RentedEquipment.Name} returned late! Penalty: ${lateRental.Penalty}");

            service.PrintSummaryReport();
        }
    }
}