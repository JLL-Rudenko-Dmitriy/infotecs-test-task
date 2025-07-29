using System.Linq.Expressions;
using TimescaleApplication.Specifications.Abstractions;
using TimescaleDomain.Timescales.Entities;

namespace TimescaleApplication.Specifications.TimescalesScpecifications;

public class AverageValueBetween : Specification<TimescalesResultData>
{
    private readonly double _minValue;
    private readonly double _maxValue;

    public AverageValueBetween(double minValue, double maxValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;
    }

    public override Expression<Func<TimescalesResultData, bool>> ToExpression() =>
        value => value.AverageValue >= _minValue && value.AverageValue <= _maxValue;
}