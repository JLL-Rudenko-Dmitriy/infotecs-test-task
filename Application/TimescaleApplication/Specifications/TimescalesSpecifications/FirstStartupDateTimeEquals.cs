using System.Linq.Expressions;
using TimescaleApplication.Specifications.Abstractions;
using TimescaleDomain.Timescales.Entities;

namespace TimescaleApplication.Specifications.TimescalesSpecifications;

public class FirstStartupDateTimeEquals : Specification<TimescalesResultData>
{
    private readonly TimeSpan _minFirstOperationTime;
    private readonly TimeSpan _maxFirstOperationTime;

    public FirstStartupDateTimeEquals(TimeSpan minOperationTime, TimeSpan maxOperationTime)
    {
        _minFirstOperationTime = minOperationTime;
        _maxFirstOperationTime = maxOperationTime;
    }
    
    public override Expression<Func<TimescalesResultData, bool>> ToExpression() =>
        result => result.FirstOperationDateTime.TimeOfDay <= _maxFirstOperationTime &&
                   result.FirstOperationDateTime.TimeOfDay >= _minFirstOperationTime;
}