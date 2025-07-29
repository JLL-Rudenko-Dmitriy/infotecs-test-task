namespace TimescaleExceptions.Exceptions.DataReaders;

public class OutOfSizeFileException : InvalidLineLengthException
{
    public override string MessageDetails { get; } = "File has more than 10000 raws.";
}