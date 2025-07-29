using TimescaleDomain.Timescales.Entities;

namespace TimescaleApplication.Dto.Results.ResultsMappers;

public static class ResultTypeMapper
{
    public static TimescaleResultDto ToDto(this TimescalesResultData results)
    {
        return new TimescaleResultDto(
            results.Id,
            results.TimeDelta.Value,
            results.FirstOperationDateTime,
            results.AverageExecutionTime,
            results.AverageValue,
            results.MedianValue,
            results.MaxValue,
            results.MinValue);
    }
}