using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;
using TimescaleDomain.TimescaleValidators.Abstractions;
using TimescaleDomain.TimescaleValidators.Entities.NodeValidators;

namespace TimescaleDomain.TimescaleValidators.Entities.ValidatorChains;

public class ValidatorChain : IValidatorChain
{
    private readonly INodeValidator _next;

    public ValidatorChain()
    {
        _next = new DateAreaNode();
        _next.SetNext(new NotNegativeExecutionTimeNode());
    }

    public ValidatorChain(INodeValidator next)
    {
        _next = next;
    }

    public Exception? ValidatorNodeException { get; private set; }

    public bool IsValid(TimescaleRecord timescaleRecord)
    {
        try
        {
            _next.Validate(timescaleRecord);
        }
        catch (Exception exception)
        {
            ValidatorNodeException = exception;
            return false;
        }
        
        return true;
    }
}