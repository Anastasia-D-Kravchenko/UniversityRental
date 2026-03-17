namespace UniversityRental.Exceptions;

public class RentalLimitExceededException : Exception
{
    public RentalLimitExceededException(string message) : base(message){}
}