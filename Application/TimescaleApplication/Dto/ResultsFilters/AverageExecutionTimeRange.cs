namespace TimescaleApplication.Dto.ResultsFilters;

public record AverageExecutionTimeRange(double AverageExecutionTimeMin, double AverageExecutionTimeMax) 
    : ResultsAbstractFilter;