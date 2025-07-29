namespace TimescaleDomain.Exceptions.ValidationExceptions;

public class NotNegativeExecutionTime : TimescaleValidatorException
{
    public NotNegativeExecutionTime()
    {
    }

    public NotNegativeExecutionTime(string? message) : base(message)
    {
    }

    public NotNegativeExecutionTime(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}