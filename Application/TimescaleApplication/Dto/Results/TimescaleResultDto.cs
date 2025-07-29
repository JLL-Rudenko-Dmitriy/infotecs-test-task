namespace TimescaleApplication.Dto.Results;

public record TimescaleResultDto( 
    Guid Id,
    double TimeDelta,
    DateTime FirstOperationDateTime,
    double AverageExecutionTime, 
    double AverageValue,
    double MedianValue,
    double MaxValue,
    double MinValue);