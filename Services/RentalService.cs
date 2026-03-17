using UniversityRental.Models;

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

    public IEnumerable<Equipment> GetAvailableEquipment() 
        => _equipments.Where(e => e.IsAvailable);

    public User? GetUser(string firstName) 
        => _users.FirstOrDefault(u => u.FirstName == firstName);

    public Equipment? GetEquipment(string name) 
        => _equipments.FirstOrDefault(e => e.Name == name);
}