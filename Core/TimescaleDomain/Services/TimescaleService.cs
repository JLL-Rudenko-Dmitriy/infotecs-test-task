using TimescaleDomain.Contracts;
using TimescaleDomain.DataReaders.Abstractions;
using TimescaleDomain.Exceptions.TimescaleTable;
using TimescaleDomain.Timescales.Abstractions.Entities;
using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.TimescaleValidators.Abstractions;
using TimescaleDomain.TimescaleValidators.Entities.ValidatorChains;

namespace TimescaleDomain.Services;

public class TimescaleService : ITimescaleService
{
    private readonly IValidatorChain _validatorChain;

    public TimescaleService(IValidatorChain validatorChain)
    {
        _validatorChain = validatorChain;
    }

    public ITimescaleTable ProcessDataFromReader(ITimescaleDataReader timescalesData)
    {
        var dataset = new List<TimescaleRecord>();
        
        while (timescalesData.ReadNext(out var timescaleRecord))
        {
            
            if (!_validatorChain.IsValid(timescaleRecord))
            {
                throw _validatorChain.ValidatorNodeException!;
            }
            
            dataset.Add(timescaleRecord);
        }
        
        var timescaleTable = new TimescaleTable(
            timescalesData.Name,
            dataset);

        if (!timescaleTable.IsValidSize)
        {
            throw new InvalidTimescaleTableSize("Timescale table size isn't valid.");
        }

        return timescaleTable;
    }

    public async Task<ITimescaleTable> ProcessDataFromReaderAsync(ITimescaleDataReader timescaleDataReader)
    {
        var dataset = new List<TimescaleRecord>();

        var record = await timescaleDataReader.ReadNextAsync();
        while (record != null)
        {
            dataset.Add(record);
            record = await timescaleDataReader.ReadNextAsync();
        }
        
        var timescaleTable = new TimescaleTable(
            timescaleDataReader.Name,
            dataset);

        if (!timescaleTable.IsValidSize)
        {
            throw new InvalidTimescaleTableSize("Timescale table size isn't valid.");
        }
        
        return timescaleTable;
    }
}