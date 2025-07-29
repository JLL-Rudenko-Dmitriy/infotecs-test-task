using ExceptionHandlers.Abstractions;
using TimescaleDomain.Exceptions.ValidationExceptions;

namespace ExceptionHandlers.ExceptionHandlers.ValidatorExceptionHandlers;

public class TimescaleValidatorExceptionHandler : TimescaleExceptionHandler<TimescaleValidatorException>
{
    protected override string SpecializedExceptionTitle { get; } = "Date is not valid";
    
    protected override int SpecializedExceptionStatusCode { get; } = 422;

    protected override string SpecializedExceptionMessageDetails { get; } = "Cant proceed file";
}