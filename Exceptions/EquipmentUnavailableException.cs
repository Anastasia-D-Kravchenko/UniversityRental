namespace UniversityRental.Exceptions;

public class EquipmentUnavailableException : Exception
{
    public EquipmentUnavailableException(string message) : base(message){}
}