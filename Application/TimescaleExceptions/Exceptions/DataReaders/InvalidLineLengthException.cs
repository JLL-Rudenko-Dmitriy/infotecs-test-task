using TimescaleExceptions.Abstractions;

namespace TimescaleExceptions.Exceptions.DataReaders;

public class InvalidLineLengthException : DataReaderException
{
    public override string MessageDetails { get; } = "Line has invalid length";
    
    public override int StatusCode { get; } = 422;
}