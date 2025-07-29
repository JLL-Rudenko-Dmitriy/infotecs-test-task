using TimescaleDomain.Exceptions.Abstractions;

namespace TimescaleDomain.Exceptions.ValidationExceptions;

public class TimescaleValidatorException : TimescaleException
{
    public TimescaleValidatorException()
    {
    }

    public TimescaleValidatorException(string? message) : base(message)
    {
    }

    public TimescaleValidatorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
}