using Core.Results.Base.Abstract;
using Core.Results.Base.Enums;
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
    
    public static void CheckSuccess<T>(TypedResult<T> result, T expectedValue)
    {
        CheckSuccess(result);
        Assert.Equal(expectedValue, result.Output);
    }
    
    public static void CheckFailure<T>(TypedResult<T> result, FailureType expectedFailureType, FailedLayer failedLayer, string expectedErrorMessage)
    {
        CheckFailure((ResultStatus) result, expectedFailureType, failedLayer, expectedErrorMessage);
        Assert.ThrowsAny<Exception>(() => result.Output);
    }
    
    public static void CheckFailure<T>(TypedResult<T> result, FailureType expectedFailureType, string expectedErrorMessage)
    {
        CheckFailure((ResultStatus) result, expectedFailureType, FailedLayer.Unknown, expectedErrorMessage);
        Assert.ThrowsAny<Exception>(() => result.Output);
    }
    
    public static void Equivalent(ResultStatus expectedResult, ResultStatus result)
    {
        Assert.Equal(expectedResult.IsSuccessful, result.IsSuccessful);
        Assert.Equal(expectedResult.IsFailure, result.IsFailure);
        Assert.Equal(expectedResult.FailureType, result.FailureType);
        Assert.Equal(expectedResult.FailedLayer, result.FailedLayer);
        Assert.Equal(expectedResult.ErrorMessages, result.ErrorMessages);
    }
    
    public static void CheckSuccess(ResultStatus result)
    {
        Assert.True(result.IsSuccessful);
        Assert.False(result.IsFailure);
        Assert.Equal(FailureType.None, result.FailureType);
        Assert.Equal(FailedLayer.None, result.FailedLayer);
        Assert.Empty(result.ErrorMessages);
        CheckFailureTypes(result, FailureType.None);
    }
    
    public static void CheckFailure(ResultStatus result, FailureType expectedFailureType, string expectedErrorMessage)
    {
        CheckFailure(result, expectedFailureType, FailedLayer.Unknown, expectedErrorMessage);
    }
    
    public static void CheckFailure(ResultStatus result, FailureType expectedFailureType, FailedLayer failedLayer, string expectedErrorMessage)
    {
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccessful);
        Assert.Equal(expectedFailureType, result.FailureType);
        Assert.Equal(failedLayer, result.FailedLayer);
        Assert.Single(result.ErrorMessages);
        Assert.Equal(expectedErrorMessage, result.GetErrorMessages());
        CheckFailureTypes(result, expectedFailureType);
    }

    private static void CheckFailureTypes(ResultStatus result, FailureType expectedFailureType)
    {
        Assert.Equal(FailureType.OperationTimeout == expectedFailureType, result.OperationTimedOut);
        Assert.Equal(FailureType.InvalidRequest == expectedFailureType, result.IsAnInvalidRequest);
        Assert.Equal(FailureType.DomainViolation == expectedFailureType, result.IsADomainViolation);
        Assert.Equal(FailureType.NotAllowed == expectedFailureType, result.IsNotAllowed);
        Assert.Equal(FailureType.ValueObject == expectedFailureType, result.IsValueObjectFailure);
        Assert.Equal(FailureType.Mapper == expectedFailureType, result.IsMapperFailure);
        Assert.Equal(FailureType.Entity == expectedFailureType, result.IsEntityFailure);
        Assert.Equal(FailureType.NotFound == expectedFailureType, result.IsNotFound);
        Assert.Equal(FailureType.AlreadyExists == expectedFailureType, result.DoesAlreadyExists);
        Assert.True(result.IsFailureType(expectedFailureType));
    }
}