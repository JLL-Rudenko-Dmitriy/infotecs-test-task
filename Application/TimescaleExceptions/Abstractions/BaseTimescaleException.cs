namespace TimescaleExceptions.Abstractions;

public class BaseTimescaleException : Exception, ITimescaleException
{
    public BaseTimescaleException()
    {
    }
    
    public BaseTimescaleException(string? message)
        : base(message)
    {
    }

    public BaseTimescaleException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public virtual string Title { get; } = "Unhandled Exception";
    
    public virtual string MessageDetails { get; } =  "Can't handle exception";

    public virtual int StatusCode { get; } = 500;
}