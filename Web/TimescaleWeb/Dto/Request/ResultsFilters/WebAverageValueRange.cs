using Microsoft.AspNetCore.Mvc;

namespace TimescaleWeb.Dto.Request.ResultsFilters;

public record WebAverageValueRange(
    [FromQuery(Name = "gt-avg-value")] double? MinValue,
    [FromQuery(Name = "lt-avg-value")] double? MaxValue);