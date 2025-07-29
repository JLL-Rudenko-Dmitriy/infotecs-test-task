using TimescaleDomain.DataReaders.Abstractions;
using TimescaleDomain.Timescales.Abstractions.Entities;
using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;
using TimescaleDomain.TimescaleValidators.Abstractions;

namespace TimescaleDomain.Contracts;

public interface ITimescaleService
{
    public ITimescaleTable ProcessDataFromReader(ITimescaleDataReader timescaleDataReader);
    
    public Task<ITimescaleTable> ProcessDataFromReaderAsync(ITimescaleDataReader timescaleDataReader);
}