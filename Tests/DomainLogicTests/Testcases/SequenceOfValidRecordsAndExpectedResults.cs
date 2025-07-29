using System.Collections;
using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace DomainLogicTests.Testcases;

public class SequenceOfValidRecordsAndExpectedResults : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new  List<TimescaleRecord> {
                new TimescaleRecord(
                    new DateTime(2000, 01, 01, 01, 01, 01, 01, DateTimeKind.Utc),
                    new Seconds(13.0),
                    12.0),
                new TimescaleRecord(new DateTime(2001, 01, 01, 01, 01, 01, 01, DateTimeKind.Utc),
                    new Seconds(123.0),
                    12.0),
                new TimescaleRecord(
                    new DateTime(2002, 01, 01, 01, 01, 01, 01, DateTimeKind.Utc),
                    new Seconds(124.0),
                    444444.0)
            }.AsEnumerable(),
            
            "Test_1",
            
            new TimescalesResultData(
                new Seconds(
                    new DateTime(2002, 01, 01, 01, 01, 01, 01, DateTimeKind.Utc) 
                            - new DateTime(2000, 01, 01, 01, 01, 01, 01, DateTimeKind.Utc)),
                new DateTime(2000, 01, 01, 01, 01, 01, 01, DateTimeKind.Utc),
                (13.0+123.0+124.0)/3,
                (12.0+12.0+444444.0)/3,
                12.0,
                444444.0,
                12.0),
        };
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}