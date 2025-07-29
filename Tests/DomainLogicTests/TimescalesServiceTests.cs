using DomainLogicTests.Testcases;
using Moq;
using TimescaleDomain.DataReaders.Abstractions;
using TimescaleDomain.Services;
using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.TimescaleValidators.Abstractions;
using TimescaleDomain.TimescaleValidators.Entities.ValidatorChains;

namespace DomainLogicTests;

public class TimescalesServiceTests
{
    [Theory]
    [ClassData(typeof(SequenceOfValidRecords))]
    public async Task ProcessingData_SequenceOfValidRecords_AllRecordsIsValid(
        IEnumerable<TimescaleRecord> inputData,
        string name)
    {
        var iterator = inputData.GetEnumerator();
        var validatorChain = new ValidatorChain();
        var dataReader = GetTimescaleDataReaderMock(validatorChain, iterator, name);
        var timescaleService = new TimescaleService(validatorChain);

        var timescaleTable = await timescaleService.ProcessDataFromReaderAsync(dataReader.Object);
        
        Assert.NotNull(timescaleTable.Records);
    }
    
    [Theory]
    [ClassData(typeof(SequenceWithInvalidRecords))]
    public async Task ProcessingData_SequenceOfInvalidRecords_AssertException(
        IEnumerable<TimescaleRecord> inputData,
        string name,
        Type expectedExceptionType)
    {
        var iterator = inputData.GetEnumerator();
        var validatorChain = new ValidatorChain();
        var dataReader = GetTimescaleDataReaderMock(validatorChain, iterator, name);
        var timescaleService = new TimescaleService(validatorChain);
        
        await Assert.ThrowsAsync(expectedExceptionType, () => timescaleService.ProcessDataFromReaderAsync(dataReader.Object));
    }
    
    [Theory]
    [ClassData(typeof(SequenceOfValidRecordsAndExpectedResults))]
    public async Task ComputeResultsData_SequenceOfValidRecords_AssertCorrectComputations(
        IEnumerable<TimescaleRecord> inputData,
        string name,
        TimescalesResultData expectedResult)
    {
        var iterator = inputData.GetEnumerator();
        var validatorChain = new ValidatorChain();
        var dataReader = GetTimescaleDataReaderMock(validatorChain, iterator, name);
        var timescaleService = new TimescaleService(validatorChain);

        var timescaleTable = await timescaleService.ProcessDataFromReaderAsync(dataReader.Object);

        var expectedWithoutId = new
        {
            timeDelta = expectedResult.TimeDelta,
            firstOperationDateTime = expectedResult.FirstOperationDateTime,
            averageExecutionTime = expectedResult.AverageExecutionTime,
            averageValue = expectedResult.AverageValue,
            medianValue = expectedResult.MedianValue,
            maxValue = expectedResult.MaxValue,
            minValue = expectedResult.MinValue
        };

        var result = timescaleTable.Results();
        var resultWithoutId = new
        {
            timeDelta = result.TimeDelta,
            firstOperationDateTime = result.FirstOperationDateTime,
            averageExecutionTime = result.AverageExecutionTime,
            averageValue = result.AverageValue,
            medianValue = result.MedianValue,
            maxValue = result.MaxValue,
            minValue = result.MinValue
        };
        
        Assert.Equal(expectedWithoutId, resultWithoutId);
    }

    private static Mock<ITimescaleDataReader> GetTimescaleDataReaderMock(
        IValidatorChain validatorChain,
        IEnumerator<TimescaleRecord> iterator,
        string name)
    {
        var timescaleDataReader = new Mock<ITimescaleDataReader>();
        
        var dataReader = new Mock<ITimescaleDataReader>();
        
        dataReader.Setup(sp => sp.ReadNextAsync()).Returns(() =>
        {
            if (!iterator.MoveNext())
                return Task.FromResult<TimescaleRecord>(null);
            
            var currentValue = iterator.Current;
            
            return (validatorChain.IsValid(currentValue) 
                ? Task.FromResult(currentValue)
                : throw validatorChain.ValidatorNodeException!)!;
        });
        
        dataReader.Setup(sp => sp.Name).Returns(name);
        
        return dataReader;
    }
}