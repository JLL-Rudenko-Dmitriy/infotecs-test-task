using TimescaleDomain.Timescales.ValueObjects;

namespace TimescaleDomain.Timescales.Abstractions.Entities;

public interface IResultData
{
    public Guid Id { get; }
    
    public Seconds TimeDelta { get; }
    
    public DateTime FirstOperationDateTime { get; }
    
    public double AverageExecutionTime { get; }
    
    public double AverageValue { get; }
    
    public double MedianValue { get; }
    
    public double MaxValue { get; }
    
    public double MinValue { get; }

    public IResultData Update(IResultData results);
}