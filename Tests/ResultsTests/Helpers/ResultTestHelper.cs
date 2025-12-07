using DDD.Core.Results.Base;
using DDD.Core.Results.ValueObjects;
using Xunit;

namespace OutputTests.Helpers;

public static class ResultTestHelper
{
    
    public static void Equivalent<T>(TypedResult<T> expectedResult, TypedResult<T> result)
    {
        Equivalent((ResultStatus) expectedResult, result);
        if (result.IsSuccessful)
        {
            Assert.Equal(expectedResult.Output, result.Output);
        }
    }
    
    public static void CheckSuccess<T>(TypedResult<T> result, T expectedValue, ResultLayer expectedLayer = ResultLayer.Unknown)
    {
        CheckSuccess(result, expectedLayer);
        Assert.Equal(expectedValue, result.Output);
    }
    
    public static void CheckFailure<T>(TypedResult<T> result, FailureType expectedFailureType, ResultLayer failedLayer, string expectedErrorMessage)
    {
        CheckFailure((ResultStatus) result, expectedFailureType, failedLayer, expectedErrorMessage);
        Assert.ThrowsAny<Exception>(() => result.Output);
    }
    
    public static void CheckFailure<T>(TypedResult<T> result, FailureType expectedFailureType, string expectedErrorMessage)
    {
        CheckFailure((ResultStatus) result, expectedFailureType, ResultLayer.Unknown, expectedErrorMessage);
        Assert.ThrowsAny<Exception>(() => result.Output);
    }
    
    public static void Equivalent(ResultStatus expectedResult, ResultStatus result)
    {
        Assert.Equal(expectedResult.IsSuccessful, result.IsSuccessful);
        Assert.Equal(expectedResult.IsFailure, result.IsFailure);
        Assert.Equal(expectedResult.CurrentFailureType, result.CurrentFailureType);
        Assert.Equal(expectedResult.CurrentLayer, result.CurrentLayer);
        Assert.Equal(expectedResult.Errors, result.Errors);
        Assert.Equal(expectedResult.ErrorMessages, result.ErrorMessages);
    }
    
    public static void CheckSuccess(ResultStatus result, ResultLayer expectedLayer = ResultLayer.Unknown)
    {
        Assert.True(result.IsSuccessful);
        Assert.False(result.IsFailure);
        Assert.Equal(FailureType.None, result.CurrentFailureType);
        Assert.Equal(expectedLayer, result.CurrentLayer);
        Assert.Empty(result.Errors);
        Assert.Empty(result.ErrorMessages);
        CheckFailureTypes(result, FailureType.None);
    }
    
    public static void CheckFailure(ResultStatus result, FailureType expectedFailureType, string expectedErrorMessage)
    {
        CheckFailure(result, expectedFailureType, ResultLayer.Unknown, expectedErrorMessage);
    }
    
    public static void CheckFailure(ResultStatus result, FailureType expectedFailureType, ResultLayer failedLayer, string expectedErrorMessage)
    {
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccessful);
        Assert.Equal(expectedFailureType, result.CurrentFailureType);
        Assert.Equal(failedLayer, result.CurrentLayer);
        Assert.Single(result.ErrorMessages);
        Assert.Equal(expectedErrorMessage, result.ErrorMessagesToString());
        CheckFailureTypes(result, expectedFailureType);
    }

    private static void CheckFailureTypes(ResultStatus result, FailureType expectedFailureType)
    {
        Assert.Equal(FailureType.OperationTimeout == expectedFailureType, result.OperationTimedOut);
        Assert.Equal(FailureType.InvalidRequest == expectedFailureType, result.IsAnInvalidRequest);
        Assert.Equal(FailureType.DomainViolation == expectedFailureType, result.IsADomainViolation);
        Assert.Equal(FailureType.NotAllowed == expectedFailureType, result.IsNotAllowed);
        Assert.Equal(FailureType.InvalidInput == expectedFailureType, result.IsInvalidInput);
        Assert.Equal(FailureType.NotFound == expectedFailureType, result.IsNotFound);
        Assert.Equal(FailureType.AlreadyExists == expectedFailureType, result.DoesAlreadyExists);
        Assert.Equal(FailureType.InvariantViolation == expectedFailureType, result.IsInvariantViolation);
        Assert.Equal(FailureType.ConcurrencyViolation == expectedFailureType, result.IsConcurrencyViolation);
        Assert.True(result.ContainsFailureType(expectedFailureType));
    }
}