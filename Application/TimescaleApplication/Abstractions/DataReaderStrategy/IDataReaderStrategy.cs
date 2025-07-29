using TimescaleDomain.DataReaders.Abstractions;

namespace TimescaleApplication.Abstractions.DataReaderStrategy;

public interface IDataReaderStrategy
{
    public ITimescaleDataReader GetDataReaderByFileExtensions(string fileExtension, string fileName, Stream stream);
}