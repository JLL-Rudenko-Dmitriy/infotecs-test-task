using DataReaders.DataReaders;
using TimescaleApplication.Abstractions.DataReaderStrategy;

namespace DataReaders.Factories;

public static class DataReaderStrategyFactory
{
    public static CSVDataReader GetCsvDataReader(StreamReader streamReader, string name)
    {
        return new CSVDataReader(streamReader, name);
    }
}