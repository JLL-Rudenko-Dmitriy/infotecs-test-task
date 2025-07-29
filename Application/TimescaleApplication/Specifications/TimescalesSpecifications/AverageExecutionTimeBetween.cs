using System.Linq.Expressions;
using TimescaleApplication.Specifications.Abstractions;
using TimescaleDomain.Timescales.Entities;

namespace TimescaleApplication.Specifications.TimescalesSpecifications;

public class AverageExecutionTimeBetween : Specification<TimescalesResultData>
{
    private readonly double _minExecutionTime;
    private readonly double _maxExecutionTime;

    public AverageExecutionTimeBetween(double minValue, double maxValue)
    {
        _minExecutionTime = minValue;
        _maxExecutionTime = maxValue;
    }

    public override Expression<Func<TimescalesResultData, bool>> ToExpression() =>
        value => 
            value.AverageExecutionTime >= _minExecutionTime && value.AverageExecutionTime <= _maxExecutionTime;
}