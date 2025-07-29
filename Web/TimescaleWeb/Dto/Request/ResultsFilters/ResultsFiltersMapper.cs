using TimescaleApplication.Dto.ResultsFilters;

namespace TimescaleWeb.Dto.Request.ResultsFilters;

public static class ResultsFiltersMapper
{
    public static AverageExecutionTimeRange ToApplicationDto(this WebAverageExecutionTimeRange averageExecutionTimeRange) => 
            new AverageExecutionTimeRange(
                averageExecutionTimeRange.minExecutionTime ??  throw new  ArgumentNullException(nameof(averageExecutionTimeRange)),
                averageExecutionTimeRange.maxExecutionTime ??   throw new  ArgumentNullException(nameof(averageExecutionTimeRange)));
    
    public static AverageValueRange ToApplicationDto(this WebAverageValueRange averageValueRange) =>
        new AverageValueRange(
            averageValueRange.MinValue ?? throw new  ArgumentNullException(nameof(averageValueRange)),
            averageValueRange.MaxValue ?? throw new  ArgumentNullException(nameof(averageValueRange)));

    public static FirstOperationTime ToApplicationDto(this WebFirstOperationTimeRange firstOperationTimeRange) =>
        new FirstOperationTime(
            firstOperationTimeRange.MinFirstOperationTime 
                ?? throw new ArgumentNullException(nameof(firstOperationTimeRange)),
            firstOperationTimeRange.MaxFirstOperationTime
                ?? throw new ArgumentNullException(nameof(firstOperationTimeRange)));
}