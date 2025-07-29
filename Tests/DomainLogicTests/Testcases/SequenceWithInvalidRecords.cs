using System.Collections;
using TimescaleDomain.Exceptions.ValidationExceptions;
using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace DomainLogicTests.Testcases;

public class SequenceWithInvalidRecords : IEnumerable<object[]>
{

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<TimescaleRecord>
            {
                new TimescaleRecord(new DateTime(2001, 01, 01, 01, 01, 01, 01, DateTimeKind.Utc),
                    new Seconds(1.0),
                    0),
                new TimescaleRecord(
                    new DateTime(2000, 01, 01, 01, 01, 01, 01, DateTimeKind.Utc),
                    new Seconds(-0.1),
                    -0.1)
            }.AsEnumerable(),
            "Test_1",
            typeof(NotNegativeExecutionTime)
        };

        yield return new object[]
        {
            new List<TimescaleRecord>
            {
                new TimescaleRecord(
                    new DateTime(2000, 01, 01, 01, 01, 01, 01, DateTimeKind.Utc),
                    new Seconds(13.0),
                    456.0),
                
                new TimescaleRecord(
                    new DateTime(2000, 01, 01, 01, 01, 01, 01, DateTimeKind.Utc).AddDays(-1),
                    new Seconds(13.0),
                    456.0),
            }.AsEnumerable(),
            "Test_2",
            typeof(InvalidDatePeriodException)
        };

        yield return new object[]
        {
            new List<TimescaleRecord>
            {
                new TimescaleRecord(
                    DateTime.UtcNow,
                    new Seconds(13.0),
                    456.0),
                
                new TimescaleRecord(
                    DateTime.UtcNow.AddSeconds(1),
                    new Seconds(13.0),
                    456.0),
            }.AsEnumerable(),
            "Test_3",
            typeof(InvalidDatePeriodException)
        };
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}