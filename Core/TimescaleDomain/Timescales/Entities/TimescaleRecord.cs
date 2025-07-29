using TimescaleDomain.Timescales.Abstractions.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace TimescaleDomain.Timescales.Entities;

public class TimescaleRecord : ITimescaleRecord
{
    public TimescaleRecord(DateTime date, Seconds executionTime, double value)
    {
        Id = Guid.NewGuid();
        Date = date;
        ExecutionTime = executionTime;
        Value = value;
    }

    private TimescaleRecord()
    {
    }

    public Guid Id { get; }
    public DateTime Date { get; }
    public Seconds ExecutionTime { get; }
    public double Value { get; }
};