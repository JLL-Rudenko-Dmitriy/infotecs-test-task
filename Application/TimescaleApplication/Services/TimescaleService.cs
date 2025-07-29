using TimescaleApplication.Abstractions.DataReaderStrategy;
using TimescaleApplication.Contracts;
using TimescaleApplication.Dto.Results;
using TimescaleApplication.Dto.Results.ResultsMappers;
using TimescaleApplication.Dto.ResultsFilters;
using TimescaleApplication.Dto.ResultsFilters.SpecificationMappers;
using TimescaleApplication.Dto.ValueRecords;
using TimescaleApplication.Dto.ValueRecords.ValueRecordsMappers;
using TimescaleApplication.Specifications.Abstractions;
using TimescaleApplication.Specifications.NullSpecification;
using TimescaleDomain.Timescales.Abstractions.Repositories;
using TimescaleDomain.Timescales.Entities;
using IDomainTimescaleService = TimescaleDomain.Contracts.ITimescaleService;

namespace TimescaleApplication.Services;

public class TimescaleService : ITimescaleService
{
    private readonly IDomainTimescaleService _domainTimescaleService;
    private readonly ITimescaleResultDataRepository  _timescaleResultDataRepository;
    private readonly ITimescaleTableRepository _timescaleTableRepository;
    private readonly IDataReaderStrategy  _dataReaderStrategy;

    public TimescaleService(
        IDomainTimescaleService domainTimescaleService,
        ITimescaleResultDataRepository timescaleResultDataRepository,
        ITimescaleTableRepository  timescaleTableRepository,
        IDataReaderStrategy  dataReaderStrategy)
    {
        _domainTimescaleService = domainTimescaleService;
        _timescaleResultDataRepository = timescaleResultDataRepository;
        _timescaleTableRepository = timescaleTableRepository;
        _dataReaderStrategy = dataReaderStrategy;
    }

    public void ProcessingFile(string fileName, Stream stream)
    {
        var extension = Path.GetExtension(fileName).ToLower().Replace(".", "");
        fileName = Path.GetFileNameWithoutExtension(fileName).ToLower();
        
        var processingStrategy = _dataReaderStrategy.GetDataReaderByFileExtensions(
            extension,
            fileName,
            stream);

        var timescaleTable = _domainTimescaleService.ProcessDataFromReader(processingStrategy);

        var actualTable = _timescaleTableRepository.CreateOrUpdateTimescaleTable((TimescaleTable)timescaleTable);

        _timescaleResultDataRepository.CreateOrUpdate(actualTable.Id, timescaleTable.Results());
    }

    public IList<TimescaleResultDto> GetTimescaleResults(
        string? fileName,
        IEnumerable<ResultsAbstractFilter> filters)
    {
        List<Specification<TimescalesResultData>> specifications = [];
        specifications.AddRange(filters.Select(filter => filter.ToSpecification()));

        Specification<TimescalesResultData> specification = new NullSpecification<TimescalesResultData>();
        
        foreach (var spec in specifications)
        {
            specification = specification.And(specification, spec);
        }

        return _timescaleResultDataRepository.FindWithSpecification(fileName, specification)
            .Select(r => r.ToDto())
            .ToList();
    }
    
    public async Task<IList<TimescaleResultDto>?> GetTimescaleResultsAsync(
        string? fileName,
        IEnumerable<ResultsAbstractFilter> filters)
    {
        List<Specification<TimescalesResultData>> specifications = [];
        specifications.AddRange(filters.Select(filter => filter.ToSpecification()));

        Specification<TimescalesResultData> specification = new NullSpecification<TimescalesResultData>();
        
        foreach (var spec in specifications)
        {
            specification = specification.And(specification, spec);
        }

        var timescaleResults = await _timescaleResultDataRepository
            .FindWithSpecificationAsync(fileName, specification);

        return timescaleResults
            .Select(r => r.ToDto())
            .ToList();
    }

    public async Task<IList<ValueRecordDto>> TakeLastRecordsAsync(string? fileName)
    {
        const int count = 10;

        var lastRecords = await _timescaleTableRepository.GetLastRecordsAsync(fileName, count);
        
        return lastRecords
            .Select(rec => rec.ToDto()).ToList();
    }

    public IList<ValueRecordDto> TakeLastRecords(string? fileName)
    {
        const int count = 10;

        var lastRecords = _timescaleTableRepository.GetLastRecords(fileName, count);

        if (lastRecords != null) 
            return lastRecords.Select(rec => rec.ToDto()).ToList();
        
        return new List<ValueRecordDto>();
    }

    public async Task ProcessingFileAsync(string fileName, Stream stream)
    {
        var extension = Path.GetExtension(fileName).ToLower().Replace(".", "");
        fileName = Path.GetFileNameWithoutExtension(fileName).ToLower();
        
        var processingStrategy = _dataReaderStrategy.GetDataReaderByFileExtensions(
            extension,
            fileName,
            stream);

        try
        {
            var timescaleTable = await _domainTimescaleService.ProcessDataFromReaderAsync(processingStrategy);

            var actualTable = await _timescaleTableRepository.CreateOrUpdateTimescaleTableASync((TimescaleTable)timescaleTable);

            await _timescaleResultDataRepository.CreateOrUpdateAsync(actualTable.Id, timescaleTable.Results());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}