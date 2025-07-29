using TimescaleDomain.Exceptions.ValidationExceptions;
using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;
using TimescaleDomain.TimescaleValidators.Abstractions;

namespace TimescaleDomain.TimescaleValidators.Entities.NodeValidators;

public class DateAreaNode : INodeValidator
{
    private INodeValidator? _next = null;
    
    public void Validate(TimescaleRecord timescaleRecord)
    {
        if (timescaleRecord.Date < MinDate || timescaleRecord.Date > MaxDate)
        {
            throw new InvalidDatePeriodException();
        }
        
        _next?.Validate(timescaleRecord);
    }

    public INodeValidator SetNext(INodeValidator next)
    {
        _next = next;
        return _next;
    }
    
    private DateTime MinDate => new DateTime(2000,1,1, 0,0,0,DateTimeKind.Utc);
    private DateTime MaxDate => DateTime.UtcNow;
}