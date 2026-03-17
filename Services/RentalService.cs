using UniversityRental.Models;
using UniversityRental.Exceptions;

namespace UniversityRental.Services;

public class RentalService
{
    private readonly List<Rental> _rentals = new();
    private readonly List<Equipment> _equipments = new();
    private readonly List<User> _users = new();

    public void AddUser(User user) => _users.Add(user);
    public void AddEquipment(Equipment equipment) => _equipments.Add(equipment);
    public void MarkAsUnavailable(Equipment equipment) => equipment.IsAvailable = false;
    public IEnumerable<Equipment> DisplayEquipments() => _equipments;
    public IEnumerable<Equipment> GetAvailableEquipment() => _equipments.Where(e => e.IsAvailable);
    public User? GetUser(string firstName) => _users.FirstOrDefault(u => u.FirstName == firstName);
    public Equipment? GetEquipment(string name) => _equipments.FirstOrDefault(e => e.Name == name);

    public Rental RentEquipment(User user, Equipment equipment, int days, DateTime? startDate = null)
    {
        if (!equipment.IsAvailable)
            throw new EquipmentUnavailableException($"{equipment.Name} is currently unavailable.");

        int activeCount = _rentals.Count(r => r.RentedBy.Id == user.Id && r.ReturnDate == null);
        if (activeCount >= user.MaxActiveRentals)
            throw new RentalLimitExceededException($"{user.FirstName} has reached the limit of {user.MaxActiveRentals} rentals.");

        equipment.IsAvailable = false;
        var rental = new Rental(user, equipment, days, startDate ?? DateTime.Now);
        _rentals.Add(rental);
        return rental;
    }

    public void ReturnEquipment(Rental rental, DateTime? returnDate = null)
    {
        rental.CompleteReturn(returnDate ?? DateTime.Now);
    }

    public IEnumerable<Rental> GetActiveRentalsForUser(User user)
    {
        return _rentals.Where(r => r.RentedBy.Id == user.Id && r.ReturnDate == null);
    }

    public IEnumerable<Rental> GetOverdueRentals()
    {
        return _rentals.Where(r => r.ReturnDate == null && DateTime.Now > r.DueDate);
    }

    public void PrintSummaryReport()
    {
        Console.WriteLine("\n--- RENTAL SERVICE SUMMARY ---");
        Console.WriteLine($"Total Equipment: {_equipments.Count}");
        Console.WriteLine($"Available Now: {_equipments.Count(e => e.IsAvailable)}");
        Console.WriteLine($"Active Rentals: {_rentals.Count(r => r.ReturnDate == null)}");
        Console.WriteLine($"Total Penalties Collected: ${_rentals.Sum(r => r.Penalty)}");
        Console.WriteLine("------------------------------\n");
    }
}