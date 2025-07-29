using ExceptionHandlers.Abstractions;
using TimescaleExceptions.Exceptions.DataReaders;

namespace ExceptionHandlers.ExceptionHandlers;

public class DataReaderExceptionHandler : TimescaleExceptionHandler<DataReaderException>
{
}