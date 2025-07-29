using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace TimescaleDomain.DataReaders.Contracts;

public interface IDataReaderService
{
    public IEnumerable<TimescaleRecord>? GetTimescalesData();
}