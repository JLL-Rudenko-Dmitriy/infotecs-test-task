using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace TimescaleDomain.TimescaleValidators.Abstractions;

public interface IValidatorChain
{
    public bool IsValid(TimescaleRecord timescaleRecord);
    
    public Exception? ValidatorNodeException { get; }
}