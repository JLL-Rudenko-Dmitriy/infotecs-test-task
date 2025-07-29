using Microsoft.AspNetCore.Mvc;
using TimescaleApplication.Contracts;
using TimescaleApplication.Dto.ResultsFilters;
using TimescaleApplication.Dto.ResultsFilters.SpecificationMappers;
using TimescaleApplication.Specifications.Abstractions;
using TimescaleDomain.Timescales.Entities;
using TimescaleWeb.Dto.Request.ResultsFilters;

namespace TimescaleWeb.Controllers;

[ApiController]
[Route("/api/timescale")]
public class TimescaleController : ControllerBase
{
    private readonly ITimescaleService _timescaleService;

    public TimescaleController(ITimescaleService timescaleService)
    {
        _timescaleService = timescaleService;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file.Length == 0)
        {
            return BadRequest("File is empty");    
        }
        
        var fileName = Path.GetFileName(file.FileName);
        var fileStream = file.OpenReadStream();
        
        await _timescaleService.ProcessingFileAsync(fileName, fileStream);
        
        return Created();
    }

    [HttpGet("results")]
    public async Task<IActionResult> Results(
        [FromQuery] WebAverageExecutionTimeRange timeRange,
        [FromQuery] WebAverageValueRange valueRange,
        [FromQuery] WebFirstOperationTimeRange operationTimeRange,
        [FromQuery(Name = "filename")] string? filename = null)
    {
        List<ResultsAbstractFilter> filters = [];
        
        if (timeRange is { minExecutionTime: not null, maxExecutionTime: not null })
        {
            filters.Add(timeRange.ToApplicationDto());
        }

        if (valueRange is { MinValue: not null, MaxValue: not null })
        {
            filters.Add(valueRange.ToApplicationDto());
        }

        if (operationTimeRange is { MinFirstOperationTime: not null, MaxFirstOperationTime: not null })
        {
            filters.Add(operationTimeRange.ToApplicationDto());
        }

        var timescaleResults = 
            await _timescaleService.GetTimescaleResultsAsync(filename, filters.AsEnumerable());
        
        return Ok(timescaleResults);
    }

    [HttpGet("last-values")]
    public async Task<IActionResult> Results([FromQuery(Name = "filename")] string? filename = null)
    {
        var lastRecords = 
            await _timescaleService.TakeLastRecordsAsync(filename ?? throw new ArgumentNullException());
        
        return Ok(lastRecords);
    }
}