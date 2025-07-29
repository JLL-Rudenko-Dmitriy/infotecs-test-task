using TimescaleDomain.Exceptions.ValidationExceptions;
using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.TimescaleValidators.Abstractions;

namespace TimescaleDomain.TimescaleValidators.Entities.NodeValidators;

public class NotNegativeExecutionTimeNode : INodeValidator
{
    private INodeValidator? _next = null;
    
    public void Validate(TimescaleRecord timescaleRecord)
    {
        
        if (timescaleRecord.ExecutionTime.Value < 0)
        {
            throw new NotNegativeExecutionTime();
        }
        
        _next?.Validate(timescaleRecord);
    }

    public INodeValidator SetNext(INodeValidator next)
    {
        _next = next;
        return _next;
    }
}