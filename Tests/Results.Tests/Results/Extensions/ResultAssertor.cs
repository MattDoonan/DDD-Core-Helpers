using DDD.Core.Results.Abstract;
using DDD.Core.Results.Exceptions;
using DDD.Core.Results.ValueObjects;
using Xunit;

namespace Results.Tests.Results.Extensions;

public static class ResultAssertor
{
    extension<T>(TypedResult<T> typedResult)
    {
        public void AssertEquivalent(TypedResult<T> secondTypedResult)
        {
            AssertEquivalent((ResultStatus) typedResult, secondTypedResult);
            if (typedResult.IsSuccessful)
            {
                Assert.Equal(typedResult.Output, secondTypedResult.Output);
            }
        }
        
        public void AssertFailure(FailureType expectedFailureType, ResultLayer expectedLayer, int expectedErrors)
        {
            AssertFailure((ResultStatus)typedResult, expectedFailureType, expectedLayer, expectedErrors);
            Assert.False(typedResult.HasOutput);
            Assert.Throws<ResultOutputAccessException>(() => typedResult.Output);
        }

        public void AssertSuccessful(T expectedValue, ResultLayer expectedLayer = ResultLayer.Unknown)
        {
            AssertSuccessful(typedResult, expectedLayer);
            Assert.True(typedResult.HasOutput);
            Assert.Equal(expectedValue, typedResult.Output);
        }
    }


    extension(ResultStatus resultStatus)
    {
        public void AssertEquivalent(ResultStatus secondResultStatus)
        {
            Assert.Equal(resultStatus.IsSuccessful, secondResultStatus.IsSuccessful);
            Assert.Equal(resultStatus.IsFailure, secondResultStatus.IsFailure);
            Assert.Equal(resultStatus.PrimaryFailureType, secondResultStatus.PrimaryFailureType);
            Assert.Equal(resultStatus.CurrentLayer, secondResultStatus.CurrentLayer);
            Assert.Equal(resultStatus.Errors, secondResultStatus.Errors);
            Assert.Equal(resultStatus.ErrorMessages, secondResultStatus.ErrorMessages);
        }
        
        public void AssertFailure(FailureType expectedFailureType, int expectedErrors)
        {
            AssertFailure(resultStatus, expectedFailureType, ResultLayer.Unknown, expectedErrors);
        }
        
        public void AssertSuccessful(ResultLayer expectedLayer = ResultLayer.Unknown)
        {
            Assert.True(resultStatus.IsSuccessful);
            Assert.False(resultStatus.IsFailure);
            Assert.Equal(FailureType.None, resultStatus.PrimaryFailureType);
            Assert.Equal(expectedLayer, resultStatus.CurrentLayer);
            Assert.Empty(resultStatus.Errors);
            Assert.Empty(resultStatus.ErrorMessages);
        }
        
        public void AssertFailure(FailureType expectedFailureType, ResultLayer expectedLayer, int expectedErrors)
        {
            Assert.True(resultStatus.IsFailure);
            Assert.False(resultStatus.IsSuccessful);
            Assert.Equal(expectedFailureType, resultStatus.PrimaryFailureType);
            Assert.Equal(expectedLayer, resultStatus.CurrentLayer);
            Assert.Equal(expectedErrors, resultStatus.Errors.Count);
        }

        public void AssertFailureType(params FailureType[] expectedFailureTypes)
        {
            Assert.Equal(expectedFailureTypes.Contains(FailureType.OperationTimeout), resultStatus.OperationTimedOut);
            Assert.Equal(expectedFailureTypes.Contains(FailureType.InvalidRequest), resultStatus.IsAnInvalidRequest);
            Assert.Equal(expectedFailureTypes.Contains(FailureType.DomainViolation), resultStatus.IsDomainViolation);
            Assert.Equal(expectedFailureTypes.Contains(FailureType.NotAllowed), resultStatus.IsNotAllowed);
            Assert.Equal(expectedFailureTypes.Contains(FailureType.InvalidInput), resultStatus.IsInvalidInput);
            Assert.Equal(expectedFailureTypes.Contains(FailureType.NotFound), resultStatus.IsNotFound);
            Assert.Equal(expectedFailureTypes.Contains(FailureType.AlreadyExists), resultStatus.AlreadyExisting);
            Assert.Equal(expectedFailureTypes.Contains(FailureType.InvariantViolation), resultStatus.IsInvariantViolation);
            Assert.Equal(expectedFailureTypes.Contains(FailureType.ConcurrencyViolation), resultStatus.IsConcurrencyViolation);
            Assert.Equal(expectedFailureTypes.Contains(FailureType.OperationCancelled), resultStatus.OperationIsCancelled);
        }
        
    }
}