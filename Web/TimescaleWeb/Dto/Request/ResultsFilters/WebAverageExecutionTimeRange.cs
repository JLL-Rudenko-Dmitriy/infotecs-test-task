using Microsoft.AspNetCore.Mvc;

namespace TimescaleWeb.Dto.Request.ResultsFilters;

public record WebAverageExecutionTimeRange(
    [FromQuery(Name = "gt-execution-time")] double? minExecutionTime,
    [FromQuery(Name = "lt-execution-time")] double? maxExecutionTime);