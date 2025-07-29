using TimescaleDomain.Timescales.Abstractions.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace TimescaleDomain.Timescales.Entities;

public class TimescalesResultData : IResultData
{
    public IResultData Update(IResultData results)
    {
        TimeDelta = results.TimeDelta;
        FirstOperationDateTime = results.FirstOperationDateTime;
        AverageExecutionTime = results.AverageExecutionTime;
        AverageValue = results.AverageValue;
        MedianValue = results.MedianValue;
        MaxValue = results.MaxValue;
        MinValue = results.MinValue;
        return this;
    }

    public TimescalesResultData(
        Seconds timeDelta,
        DateTime firstOperationDateTime,
        double averageExecutionTime,
        double averageValue,
        double medianValue,
        double maxValue,
        double minValue)
    {
        Id = Guid.NewGuid();
        TimeDelta = timeDelta;
        FirstOperationDateTime = firstOperationDateTime;
        AverageExecutionTime = averageExecutionTime;
        AverageValue = averageValue;
        MedianValue = medianValue;
        MaxValue = maxValue;
        MinValue = minValue;
    }
    
    private TimescalesResultData()
    {
    }
    
    public Guid Id { get; private set; }
    
    public Seconds TimeDelta { get; private set; }
    
    public DateTime FirstOperationDateTime { get; private set; }
    
    public double AverageExecutionTime { get; private set; }
    
    public double AverageValue { get; private set; }
    
    public double MedianValue { get; private set; }
    
    public double MaxValue { get; private set; }
    
    public double MinValue { get; private set; }
};