namespace TimescaleDomain.Exceptions.Abstractions;

public abstract class TimescaleException : Exception
{
    protected TimescaleException()
    {
    }

    protected TimescaleException(string? message) : base(message)
    {
    }

    protected TimescaleException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}