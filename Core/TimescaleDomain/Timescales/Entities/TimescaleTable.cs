using TimescaleDomain.Timescales.Abstractions.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace TimescaleDomain.Timescales.Entities;

public class TimescaleTable : ITimescaleTable
{

    private readonly List<TimescaleRecord> _records;
    
    public TimescaleTable(string name, IEnumerable<TimescaleRecord> records)
    {
        Id = Guid.NewGuid();
        Name = name;
        _records = records.OrderBy(data => data.Date).ToList();
    }

    private TimescaleTable()
    {
    }

    public Guid Id { get; private set; }
    
    public string Name { get; private set; }
    
    public IEnumerable<TimescaleRecord> Records
    {
        get
        {
            return _records;
        }
        set
        {
            _records.Clear();
            _records.AddRange(value);
        }
    }

    public Seconds TimeDelta =>
        new Seconds(Records.Last().Date.Subtract(Records.First().Date)); 
    
    public DateTime FirstOperationDateTime =>
        Records.First().Date;
    
    public double AverageExecutionTime 
    {
        get
        {
            var count = Records.Count();
            return Records.Select(data => (data.ExecutionTime.Value / count)).Sum();
        }
    }
    
    public double AverageValue =>
        Records.Select(data => data.Value).Average();
    
    public double MedianValue 
        => Records.ElementAt(Records.Count() / 2).Value;
    
    public double MaxValue =>
        Records.Max(data => data.Value);
    
    public double MinValue =>
        Records.Min(data => data.Value);

    public bool IsValidSize => Records.Count() is > 0 and <= 10_000;
    
    public TimescalesResultData Results()
    {
        return new TimescalesResultData(
            TimeDelta,
            FirstOperationDateTime,
            AverageExecutionTime,
            AverageValue,
            MedianValue,
            MaxValue,
            MinValue);
    }

    public void AddRecord(TimescaleRecord record)
    {
        _records.Add(record);
    }

    public void RemoveRecord(TimescaleRecord record)
    {
        _records.Remove(record);
    }
}