using Outputs.Results.Abstract;
using Xunit;

namespace OutputTests.Helpers;

public static class ResultTestHelper
{
    
    public static void CheckSuccess<T>(ValueResult<T> result, T expectedValue)
    {
        CheckSuccess(result);
        Assert.Equal(expectedValue, result.Value);
    }
    public static void CheckFailure<T>(ValueResult<T> result, FailureType expectedFailureType, string expectedErrorMessage)
    {
        CheckFailure((ResultStatus) result, expectedFailureType, expectedErrorMessage);
        Assert.ThrowsAny<Exception>(() => result.Value);
    }
    
    public static void CheckSuccess(ResultStatus result)
    {
        Assert.True(result.IsSuccessful);
        Assert.False(result.IsFailure);
        Assert.Equal(FailureType.None, result.FailureType);
        Assert.Empty(result.ErrorMessages);
    }
    public static void CheckFailure(ResultStatus result, FailureType expectedFailureType, string expectedErrorMessage)
    {
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccessful);
        Assert.Equal(expectedFailureType, result.FailureType);
        Assert.Single(result.ErrorMessages);
        Assert.Equal(expectedErrorMessage, result.GetErrorMessages());
    }
}