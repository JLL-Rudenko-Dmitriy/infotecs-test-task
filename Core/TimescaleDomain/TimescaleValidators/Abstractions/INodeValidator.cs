using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace TimescaleDomain.TimescaleValidators.Abstractions;

public interface INodeValidator
{
    public void Validate(TimescaleRecord timescaleRecord);

    public INodeValidator SetNext(INodeValidator next);
}