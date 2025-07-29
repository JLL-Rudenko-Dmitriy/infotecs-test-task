using System.Collections;
using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace DomainLogicTests.Testcases;

public class SequenceOfValidRecords : IEnumerable<object[]>
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
                    new TimescaleRecord(new DateTime(2002, 01, 01, 01, 01, 01, 01, DateTimeKind.Utc),
                        new Seconds(124.0),
                        444444.0)
                }.AsEnumerable(),
                "Test_1",
        };
        
        yield return new object[]
        {
            new  List<TimescaleRecord> {
                new TimescaleRecord(
                    new DateTime(2022, 02, 01, 01, 01, 01, 01, DateTimeKind.Utc),
                    new Seconds(13.0),
                    456.0),
                new TimescaleRecord(new DateTime(2022, 01, 01, 012, 13, 33, 01, DateTimeKind.Utc),
                    new Seconds(123.0),
                    23.0),
                new TimescaleRecord(new DateTime(2022, 01, 01, 04, 03, 01, 01, DateTimeKind.Utc),
                    new Seconds(124.0),
                    12312.0)
            }.AsEnumerable(),
            "Test_2",
        };
        
        yield return new object[]
        {
            new  List<TimescaleRecord> {
                new TimescaleRecord(
                    new DateTime(2023, 02, 01, 01, 01, 01, 01, DateTimeKind.Utc),
                    new Seconds(13.123),
                    0.23),
                new TimescaleRecord(new DateTime(2023, 01, 01, 012, 13, 33, 01, DateTimeKind.Utc),
                    new Seconds(125.0),
                    -0.02),
                new TimescaleRecord(new DateTime(2023, 01, 01, 04, 03, 01, 01, DateTimeKind.Utc),
                    new Seconds(1278.1231),
                    -1.0)
            }.AsEnumerable(),
            "Test_3",
        };
        
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}