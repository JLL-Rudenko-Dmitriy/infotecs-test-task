namespace TimescaleExceptions.Exceptions.DataReaders;

public sealed class EmptyFileException : InvalidLineLengthException
{
    public override string MessageDetails { get; } = "Empty file";
}