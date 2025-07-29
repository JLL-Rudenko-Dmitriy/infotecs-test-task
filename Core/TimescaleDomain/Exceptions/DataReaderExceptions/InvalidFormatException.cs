namespace TimescaleDomain.Exceptions.DataReaderExceptions;

public class InvalidFormatException : DataReaderException
{
    public InvalidFormatException()
    {
    }

    public InvalidFormatException(string? message) : base(message)
    {
    }

    public InvalidFormatException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}