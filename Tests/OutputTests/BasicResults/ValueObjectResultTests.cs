using OutputTests.BasicResults.Helpers;
using ValueObjects.Results;
using ValueObjects.Types.Regular.Base;
using ValueObjects.Types.Regular.Numbers;
using Xunit;

namespace OutputTests.BasicResults;

public class ValueObjectResultTests : BasicResultTests<ValueObjectResult>
{
    private class TestValueObject(int value) : NumberValueObjectBase<int, TestValueObject>(value), IValueObject<int, TestValueObject>
    {
        public static ValueObjectResult<TestValueObject> Create(int value)
        {
            return ValueObjectResult.Pass(new TestValueObject(value));
        }
    }
    
    [Fact]
    public void IHaveASuccessfulResult_WithAValue_ThenIsSuccessful_ShouldReturnTrue()
    {
        var expectedValue = TestValueObject.Create(5).Value;
        const string expectedLog = "A Description";
        var result = ValueObjectResult.Pass(expectedValue, expectedLog);
        Assert.True(result.IsSuccessful);
        Assert.False(result.IsFailure);
        Assert.Equal(expectedValue, result.Value);
        Assert.Contains(expectedLog, result.SuccessLogs);
        Assert.Empty(result.ErrorMessages);
    }
    
    [Fact]
    public void IHaveAFailureResult_ThenIsFailure_ShouldReturnTrue_AndValueShouldThrowError()
    {
        const string expectedError = "this should fail";
        var result = ValueObjectResult.Fail<TestValueObject>(expectedError);
        Assert.False(result.IsSuccessful);
        Assert.True(result.IsFailure);
        Assert.Empty(result.SuccessLogs);
        Assert.ThrowsAny<Exception>(() => result.Value);
        Assert.Single(result.ErrorMessages);
        Assert.Contains(expectedError, result.ErrorMessages.FirstOrDefault());
    }
}