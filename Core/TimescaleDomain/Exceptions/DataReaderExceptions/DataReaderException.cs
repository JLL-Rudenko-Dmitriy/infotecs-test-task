using TimescaleDomain.Exceptions.Abstractions;

namespace TimescaleDomain.Exceptions.DataReaderExceptions;

public class DataReaderException : TimescaleException
{
    public DataReaderException()
    {
    }

    public DataReaderException(string? message) : base(message)
    {
    }

    public DataReaderException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}