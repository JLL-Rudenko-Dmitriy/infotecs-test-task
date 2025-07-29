using TimescaleDomain.Timescales.Entities;

namespace TimescaleApplication.Dto.ValueRecords.ValueRecordsMappers;

public static class ValueRecordMapper
{
    public static ValueRecordDto ToDto(this TimescaleRecord timescaleRecord)
    {
        return new ValueRecordDto(
            timescaleRecord.Id,
            timescaleRecord.Date,
            timescaleRecord.ExecutionTime.Value,
            timescaleRecord.Value);
    }
}