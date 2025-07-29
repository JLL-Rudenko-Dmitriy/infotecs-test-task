using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace TimescaleDomain.DataReaders.Abstractions;

public interface ITimescaleDataReader
{
    public string Name { get; }
    
    public bool ReadNext(out TimescaleRecord timescaleRecord);
    
    public Task<TimescaleRecord?> ReadNextAsync();
}