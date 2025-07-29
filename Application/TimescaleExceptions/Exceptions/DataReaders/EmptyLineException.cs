namespace TimescaleExceptions.Exceptions.DataReaders;

public class EmptyLineException : DataReaderException
{
    public override string MessageDetails { get; } = "Can't handle file with empty line";
    public override int StatusCode { get; } = 422;
}