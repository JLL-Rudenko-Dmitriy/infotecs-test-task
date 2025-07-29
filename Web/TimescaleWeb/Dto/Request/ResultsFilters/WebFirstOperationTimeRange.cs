using Microsoft.AspNetCore.Mvc;

namespace TimescaleWeb.Dto.Request.ResultsFilters;

public record WebFirstOperationTimeRange(
    [FromQuery(Name = "gt-f-op-time")] TimeSpan? MinFirstOperationTime,
    [FromQuery(Name = "lt-f-op-time")] TimeSpan? MaxFirstOperationTime);