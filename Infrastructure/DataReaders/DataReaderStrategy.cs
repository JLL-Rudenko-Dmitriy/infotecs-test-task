using DataReaders.Factories;
using TimescaleApplication.Abstractions.DataReaderStrategy;
using TimescaleDomain.DataReaders.Abstractions;

namespace DataReaders;

public class DataReaderStrategy : IDataReaderStrategy
{
    public ITimescaleDataReader GetDataReaderByFileExtensions(string fileExtension, string fileName, Stream stream)
    {
        if (fileExtension.Equals("csv"))
        {
            return DataReaderStrategyFactory.GetCsvDataReader(new StreamReader(stream), fileName);
        }

        throw new Exception("Cant process this file type");
    }
}