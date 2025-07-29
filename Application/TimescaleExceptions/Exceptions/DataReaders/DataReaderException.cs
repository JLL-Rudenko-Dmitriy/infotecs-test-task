using TimescaleExceptions.Abstractions;

namespace TimescaleExceptions.Exceptions.DataReaders;

public class DataReaderException : Exception, ITimescaleException
{
    public DataReaderException()
    {
    }

    public DataReaderException(string? message)
        : base(message)
    {
    }

    public DataReaderException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public string Title { get; } = "Data processing error";

    public virtual string MessageDetails { get; } = "Error while reading data";

    public virtual int StatusCode { get; } = 500;
}