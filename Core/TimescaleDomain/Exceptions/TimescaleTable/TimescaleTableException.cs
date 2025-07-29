using TimescaleDomain.Exceptions.Abstractions;

namespace TimescaleDomain.Exceptions.TimescaleTable;

public class TimescaleTableException : TimescaleException
{
    public TimescaleTableException()
    {
    }

    public TimescaleTableException(string? message) : base(message)
    {
    }

    public TimescaleTableException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}