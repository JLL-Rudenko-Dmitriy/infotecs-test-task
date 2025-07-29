using System.ComponentModel.DataAnnotations;

namespace TimescaleDomain.Exceptions.ValidationExceptions;

public class InvalidDatePeriodException : TimescaleValidatorException
{
    public InvalidDatePeriodException()
    {
    }

    public InvalidDatePeriodException(string? message) : base(message)
    {
    }

    public InvalidDatePeriodException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}