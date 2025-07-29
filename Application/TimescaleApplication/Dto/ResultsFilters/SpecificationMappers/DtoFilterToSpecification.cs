using TimescaleApplication.Specifications.Abstractions;
using TimescaleApplication.Specifications.TimescalesScpecifications;
using TimescaleApplication.Specifications.TimescalesSpecifications;
using TimescaleDomain.Timescales.Entities;

namespace TimescaleApplication.Dto.ResultsFilters.SpecificationMappers;

public static class DtoFilterToSpecification
{
    public static Specification<TimescalesResultData> ToSpecification(this ResultsAbstractFilter filter)
    {
        return filter switch
        {
            AverageExecutionTimeRange avgExecutionTimeRange => avgExecutionTimeRange.ToSpecification(),
            AverageValueRange range => range.ToSpecification(),
            FirstOperationTime firstOpTime => firstOpTime.ToSpecification(),
            _ => throw new NotSupportedException($"Filter type {filter.GetType()} not supported")
        };
    }
    
    private static Specification<TimescalesResultData> ToSpecification(this AverageExecutionTimeRange range)
        => new AverageExecutionTimeBetween(range.AverageExecutionTimeMin, range.AverageExecutionTimeMax);
    
    private static Specification<TimescalesResultData> ToSpecification(this AverageValueRange range) 
        => new AverageValueBetween(range.MinAverageValue,  range.MaxAverageValue);
    
    private static Specification<TimescalesResultData> ToSpecification(this FirstOperationTime opTime)
        => new FirstStartupDateTimeEquals(opTime.MinTime, opTime.MaxTime);
}