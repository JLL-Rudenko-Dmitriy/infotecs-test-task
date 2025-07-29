using TimescaleDomain.Timescales.Abstractions.Entities;
using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace TimescaleDomain.Timescales.Abstractions.Repositories;

public interface ITimescaleTableRepository
{
    public IList<TimescaleRecord>?  GetLastRecords(string name, int count);
    
    public TimescaleTable CreateOrUpdateTimescaleTable(TimescaleTable timescaleTable);
    
    public Task<IList<TimescaleRecord>?>  GetLastRecordsAsync(string name, int count);
    
    public Task<TimescaleTable> CreateOrUpdateTimescaleTableASync(TimescaleTable timescaleTable);
}