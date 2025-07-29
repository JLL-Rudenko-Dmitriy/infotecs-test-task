using Microsoft.EntityFrameworkCore;
using TimescaleDomain.Specifications.Abstractions;
using TimescaleDomain.Timescales.Abstractions.Repositories;
using TimescaleDomain.Timescales.Entities;

namespace Persistence.Repositories;

public class TimescaleResultDataRepository : ITimescaleResultDataRepository
{
    private readonly TimescaleDbContext _dbContext;

    public TimescaleResultDataRepository(TimescaleDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void CreateOrUpdate(Guid timescaleTableId, TimescalesResultData results)
    {
        var resultsFromDb = _dbContext.TimescalesResultData.FirstOrDefault(r 
            => EF.Property<Guid>(r, "timescale_id") == timescaleTableId);
        
        if (resultsFromDb != null)
        {
            resultsFromDb.Update(results);
            _dbContext.Update(resultsFromDb);
            _dbContext.SaveChanges();
            return;
        }
        
        var resultsExistingEntry = _dbContext.Entry(results);
        resultsExistingEntry.Property<Guid>("timescale_id").CurrentValue = timescaleTableId;
        resultsExistingEntry.Property<Guid>("timescale_id").IsModified = true;
        
        _dbContext.TimescalesResultData.Add(results);
        _dbContext.SaveChanges();
    }
    
    public IList<TimescalesResultData>? FindWithSpecification(
        string? fileName,
        ISpecification<TimescalesResultData>? specification)
    {
        var query = _dbContext.TimescalesResultData.AsQueryable();
        if (fileName != null)
        {
            query = query.Join(_dbContext.TimescaleTables,
                    result => EF.Property<Guid>(result, "timescale_id"),
                    table => table.Id,
                    (result, table) => new { result, table })
                .Where(joined => joined.table.Name == fileName)
                .Select(joined => joined.result);
        }
        
        return  query.Where(specification.ToExpression()).ToList();
    }

    public async Task<IList<TimescalesResultData>?> FindWithSpecificationAsync(
        string? fileName,
        ISpecification<TimescalesResultData>? specification)
    {
        var query = _dbContext.TimescalesResultData.AsQueryable();
        if (fileName != null)
        {
            query = query.Join(_dbContext.TimescaleTables,
                    result => EF.Property<Guid>(result, "timescale_id"),
                    table => table.Id,
                    (result, table) => new { result, table })
                .Where(joined => joined.table.Name == fileName)
                .Select(joined => joined.result);
        }
        
        return await query.Where(specification.ToExpression()).ToListAsync();
    }

    public async Task CreateOrUpdateAsync(Guid timescaleTableId, TimescalesResultData results)
    {
        var resultsFromDb = await _dbContext.TimescalesResultData.FirstOrDefaultAsync(r 
            => EF.Property<Guid>(r, "timescale_id") == timescaleTableId);
        
        if (resultsFromDb != null)
        {
            resultsFromDb.Update(results);
            _dbContext.Update(resultsFromDb);
            await _dbContext.SaveChangesAsync();
            return;
        }
        
        var resultsExistingEntry = _dbContext.Entry(results);
        resultsExistingEntry.Property<Guid>("timescale_id").CurrentValue = timescaleTableId;
        resultsExistingEntry.Property<Guid>("timescale_id").IsModified = true;
        
        await _dbContext.TimescalesResultData.AddAsync(results);
        await _dbContext.SaveChangesAsync();
    }
}