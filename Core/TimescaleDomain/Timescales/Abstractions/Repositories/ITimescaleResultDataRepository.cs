using TimescaleDomain.Specifications.Abstractions;
using TimescaleDomain.Timescales.Entities;

namespace TimescaleDomain.Timescales.Abstractions.Repositories;

public interface ITimescaleResultDataRepository
{
    public IList<TimescalesResultData>? FindWithSpecification(
        string? fileName, ISpecification<TimescalesResultData>? specification);
    
    public void CreateOrUpdate(Guid timescaleTableId, TimescalesResultData results);
    
    public Task<IList<TimescalesResultData>?> FindWithSpecificationAsync(
        string name, ISpecification<TimescalesResultData>? spec);
    
    public Task CreateOrUpdateAsync(Guid timescaleTableId, TimescalesResultData results);
}