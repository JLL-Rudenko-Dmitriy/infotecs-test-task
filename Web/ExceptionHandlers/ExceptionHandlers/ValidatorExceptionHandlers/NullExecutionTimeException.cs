using ExceptionHandlers.Abstractions;
using TimescaleDomain.Exceptions.ValidationExceptions;

namespace ExceptionHandlers.ExceptionHandlers.ValidatorExceptionHandlers;

public class NullExecutionTimeExceptionHandler : TimescaleExceptionHandler<NotNegativeExecutionTime>
{
    protected override string SpecializedExceptionTitle { get; } = "Invalid execution time value";

    protected override int SpecializedExceptionStatusCode { get; } = 422;

    protected override string SpecializedExceptionMessageDetails { get; } = "Execution time can't be 0";
}