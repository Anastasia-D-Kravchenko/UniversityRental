namespace UniversityRental.Models;

public class Rental
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public User RentedBy { get; }
    public Equipment RentedEquipment { get; }
    public DateTime RentDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    public decimal Penalty { get; private set; }

    private const decimal DailyPenaltyRate = 10.0m;

    public Rental(User user, Equipment equipment, int daysToRent, DateTime? overrideStartDate = null)
    {
        RentedBy = user;
        RentedEquipment = equipment;
        RentDate = overrideStartDate ?? DateTime.Now;
        DueDate = RentDate.AddDays(daysToRent);
    }

    public void CompleteReturn(DateTime? overrideReturnDate = null)
    {
        ReturnDate = overrideReturnDate ?? DateTime.Now;
        RentedEquipment.IsAvailable = true;

        if (ReturnDate > DueDate)
        {
            int lateDays = (ReturnDate.Value - DueDate).Days;
            Penalty = lateDays * DailyPenaltyRate;
        }
    }

    public bool IsActive => !ReturnDate.HasValue;
    public bool IsOverdue => IsActive && DateTime.Now > DueDate;
}