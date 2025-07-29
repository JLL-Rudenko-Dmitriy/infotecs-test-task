namespace TimescaleApplication.Dto.ResultsFilters;

public record FirstOperationTime(TimeSpan MinTime, TimeSpan MaxTime) : ResultsAbstractFilter;