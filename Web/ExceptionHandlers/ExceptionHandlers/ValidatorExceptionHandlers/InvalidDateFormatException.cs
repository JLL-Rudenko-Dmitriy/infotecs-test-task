using ExceptionHandlers.Abstractions;
using TimescaleDomain.Exceptions.ValidationExceptions;

namespace ExceptionHandlers.ExceptionHandlers.ValidatorExceptionHandlers;

public class InvalidDateFormatExceptionHandler : TimescaleExceptionHandler<InvalidDatePeriodException>
{
    protected override string SpecializedExceptionTitle { get; } = "Invalid date format";

    protected override int SpecializedExceptionStatusCode { get; } = 422;

    protected override string SpecializedExceptionMessageDetails { get; } = "Can't handle this format";

}