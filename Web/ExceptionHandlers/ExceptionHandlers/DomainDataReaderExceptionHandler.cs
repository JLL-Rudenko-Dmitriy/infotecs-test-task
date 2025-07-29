using ExceptionHandlers.Abstractions;
using TimescaleDomain.Exceptions.DataReaderExceptions;


namespace ExceptionHandlers.ExceptionHandlers;

public class DomainDataReaderExceptionHandler : TimescaleExceptionHandler<DataReaderException>
{
    protected override string SpecializedExceptionTitle { get; } = "Error while reading data";

    protected override int SpecializedExceptionStatusCode { get; } = 422;

    protected override string SpecializedExceptionMessageDetails { get; } = "Can't proceed this data";
}