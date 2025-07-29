namespace TimescaleApplication.Dto.ValueRecords;

public record ValueRecordDto(
    Guid Id,
    DateTime Date,
    double ExecutionTime,
    double Value);