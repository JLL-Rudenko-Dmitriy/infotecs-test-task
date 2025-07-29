namespace TimescaleExceptions.Abstractions;

public interface ITimescaleException
{
    public string Title { get; }
    
    public string MessageDetails { get; }
    
    public int StatusCode { get; }
}