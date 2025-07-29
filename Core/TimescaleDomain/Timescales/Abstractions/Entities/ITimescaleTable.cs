using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace TimescaleDomain.Timescales.Abstractions.Entities;

public interface ITimescaleTable
{
    public Guid Id { get; }
    
    public string Name { get;}
    
    public IEnumerable<TimescaleRecord> Records { get; }
    
    Seconds TimeDelta { get; }
    
    DateTime FirstOperationDateTime { get; }
    
    double AverageExecutionTime { get; }
    
    double AverageValue { get; }
    
    double MedianValue { get; }
    
    double MaxValue { get; }
    
    double MinValue { get; }
    
    public bool IsValidSize { get; }
    
    public TimescalesResultData Results();

    public void AddRecord(TimescaleRecord record);
    
    public void RemoveRecord(TimescaleRecord record);
}