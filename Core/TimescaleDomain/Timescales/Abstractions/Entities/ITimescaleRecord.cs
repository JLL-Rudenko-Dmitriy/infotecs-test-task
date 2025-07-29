using TimescaleDomain.Timescales.ValueObjects;

namespace TimescaleDomain.Timescales.Abstractions.Entities;

public interface ITimescaleRecord
{
    public Guid Id { get; }
    
    public DateTime Date { get; }
    
    public Seconds ExecutionTime { get; }
    
    public double Value { get; }
}