namespace TimescaleDomain.Timescales.ValueObjects;

public record struct Seconds
{
    public Seconds(TimeSpan seconds)
    {
        Value = seconds.TotalSeconds;
    }

    public Seconds(double seconds)
    {
        Value = seconds;
    }
    
    public double Value { get; }
};