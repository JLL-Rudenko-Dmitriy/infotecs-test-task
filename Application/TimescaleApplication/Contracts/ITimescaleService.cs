using TimescaleApplication.Dto.Results;
using TimescaleApplication.Dto.ResultsFilters;
using TimescaleApplication.Dto.ValueRecords;

namespace TimescaleApplication.Contracts;

public interface ITimescaleService
{
    public void ProcessingFile(string fileName, Stream stream);
    
    public IList<TimescaleResultDto> GetTimescaleResults(string? fileName, 
        IEnumerable<ResultsAbstractFilter> filters);

    public IList<ValueRecordDto> TakeLastRecords(string? fileName);
    
    public Task<IList<TimescaleResultDto>?> GetTimescaleResultsAsync(string? fileName, 
        IEnumerable<ResultsAbstractFilter> filters);

    public Task<IList<ValueRecordDto>?> TakeLastRecordsAsync(string? fileName);
    
    public Task ProcessingFileAsync(string fileName, Stream stream);
}