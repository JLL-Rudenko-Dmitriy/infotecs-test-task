using System.Globalization;
using TimescaleDomain.DataReaders.Abstractions;
using TimescaleDomain.Exceptions.DataReaderExceptions;
using TimescaleDomain.Exceptions.ValidationExceptions;
using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;
using TimescaleExceptions.Exceptions.DataReaders;

namespace DataReaders.DataReaders;

public class CSVDataReader : ITimescaleDataReader
{
    private readonly StreamReader _streamReader;
    private readonly char _delimiter = ';';

    public CSVDataReader(StreamReader streamReader, string name)
    {
        _streamReader = streamReader;
        
        _streamReader.ReadLine();
        
        Name = name;
    }
    
    public CSVDataReader(StreamReader streamReader, string name, char delimiter)
    {
        _streamReader = streamReader;
        Name = name;
        _delimiter = delimiter;
    }

    public string Name { get; }
    
    public bool ReadNext(out TimescaleRecord timescaleRecord)
    {
        timescaleRecord = default;
        
        var line =  _streamReader.ReadLine();
        if (line == null)
        {
            return false;
        }
        
        timescaleRecord = ToRawData(line.Split(_delimiter));
        return true;
    }

    public async Task<TimescaleRecord?> ReadNextAsync()
    {
        var line =  await _streamReader.ReadLineAsync();
        
        return line == null ? null : ToRawData(line.Split(_delimiter));
    }

    public async Task<TimescaleRecord?> ReadNextAsync(CancellationToken cancellationToken)
    {
        var line = await _streamReader.ReadLineAsync(cancellationToken);
        
        if (line == null)
        {
            return null;
        }
        
        return ToRawData(line.Split(_delimiter));
    }

    private static TimescaleRecord ToRawData(ReadOnlySpan<string> values)
    {
        if (string.IsNullOrEmpty(values[0]))
        {
            throw new EmptyLineException();
        }

        if (values.Length != 3)
        {
            throw new InvalidLineLengthException();
        }

        try
        {
            DateTime.TryParseExact(
                values[0],
                "yyyy-MM-ddTHH:mm:ss.ffffZ",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dateTime);

            var seconds = new Seconds(double.Parse(values[1], CultureInfo.InvariantCulture));
            var value = double.Parse(values[2], CultureInfo.InvariantCulture);
            
            return new TimescaleRecord(dateTime, seconds, value);
        }
        catch (FormatException)
        {
            throw new InvalidFormatException();
        }
    }
}