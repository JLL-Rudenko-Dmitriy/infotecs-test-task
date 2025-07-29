namespace TimescaleApplication.Dto.ResultsFilters;

public record AverageValueRange(double MinAverageValue,  double MaxAverageValue)
    : ResultsAbstractFilter;
