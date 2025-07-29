namespace TimescaleDomain.Exceptions.TimescaleTable;

public class InvalidTimescaleTableSize : TimescaleTableException
{
    public InvalidTimescaleTableSize()
    {
    }

    public InvalidTimescaleTableSize(string? message) : base(message)
    {
    }

    public InvalidTimescaleTableSize(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}