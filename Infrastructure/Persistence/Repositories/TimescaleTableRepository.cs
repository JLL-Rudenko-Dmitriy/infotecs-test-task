using Microsoft.EntityFrameworkCore;
using TimescaleDomain.Timescales.Abstractions.Entities;
using TimescaleDomain.Timescales.Abstractions.Repositories;
using TimescaleDomain.Timescales.Entities;

namespace Persistence.Repositories;

public class TimescaleTableRepository : ITimescaleTableRepository
{
    private readonly TimescaleDbContext _dbContext;

    public TimescaleTableRepository(TimescaleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IList<TimescaleRecord>? GetLastRecords(string name,  int count)
    {
        var recordsByName = _dbContext.TimescaleTables
            .FirstOrDefault(t => t.Name == name);
        
        return recordsByName?
            .Records
            .OrderByDescending(r => r.Date)
            .Take(count).ToList();
    }
    

    public TimescaleTable CreateOrUpdateTimescaleTable(TimescaleTable timescaleTable)
    {
        var dbTransaction = _dbContext.Database.BeginTransaction();

        try
        {
            var table = _dbContext.TimescaleTables.FirstOrDefault(t => t.Name == timescaleTable.Name);

            if (table != null)
            {
                UpdateRecordsByName(table.Name, timescaleTable.Records);
                return  table;
            }
        
            _dbContext.TimescaleTables.Add(timescaleTable);
            _dbContext.SaveChanges();
            
            dbTransaction.Commit();
        }
        catch (Exception exception)
        {
            dbTransaction.Rollback();
            throw;
        }
        
        return  timescaleTable;
    }

    public async Task<IList<TimescaleRecord>?> GetLastRecordsAsync(string name, int count)
    {
        var recordsByName = await _dbContext.TimescaleTables
            .FirstOrDefaultAsync(t => t.Name == name);
        
        return recordsByName?
            .Records
            .OrderBy(r => r.Date)
            .Take(count).ToList();
    }

    public async Task<TimescaleTable> CreateOrUpdateTimescaleTableASync(TimescaleTable timescaleTable)
    {
        var dbTransaction = await _dbContext.Database.BeginTransactionAsync();
        
        try
        {
            var table = await _dbContext.TimescaleTables.FirstOrDefaultAsync(t => t.Name == timescaleTable.Name);

            if (table != null)
            {
                await UpdateRecordsByNameAsync(table.Name, timescaleTable.Records);
                await dbTransaction.CommitAsync();
                return table;
            }

            await _dbContext.TimescaleTables.AddAsync(timescaleTable);
            await _dbContext.SaveChangesAsync();

            await dbTransaction.CommitAsync();
        }
        catch (Exception exception)
        {
            await dbTransaction.RollbackAsync();
            throw;
        }

        
        return  timescaleTable;
    }

    public void UpdateRecordsByName(string name, IEnumerable<TimescaleRecord> timescaleRecords)
    {
        var table = _dbContext.TimescaleTables
            .Include(t => t.Records)
            .First(t => t.Name == name);
    
        _dbContext.RemoveRange(table.Records);
        
        table.Records = timescaleRecords;
        _dbContext.AddRange(table.Records);

        _dbContext.SaveChanges();
    }
    
    public async Task UpdateRecordsByNameAsync(string name, IEnumerable<TimescaleRecord> timescaleRecords)
    {
        var table = await _dbContext.TimescaleTables
            .Include(t => t.Records)
            .FirstAsync(t => t.Name == name);
    
        _dbContext.RemoveRange(table.Records);
        
        table.Records = timescaleRecords;
        await _dbContext.AddRangeAsync(table.Records);

        await _dbContext.SaveChangesAsync();
    }
}