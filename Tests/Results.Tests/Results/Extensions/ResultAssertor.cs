using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
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
        
        public void AssertFailure(FailedOperationStatus expectedStatus, ResultLayer expectedLayer, int expectedErrors)
        {
            AssertFailure((ResultStatus)typedResult, expectedStatus, expectedLayer, expectedErrors);
            Assert.False(typedResult.HasOutput);
            Assert.Throws<ResultOutputAccessException>(() => typedResult.Output);
        }

        public void AssertSuccessful(T expectedValue, ResultLayer expectedLayer = ResultLayer.Unknown)
        {
            AssertSuccessful(typedResult, OperationStatus.Success<T>(), expectedLayer);
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
            Assert.Equal(resultStatus.PrimaryStatus, secondResultStatus.PrimaryStatus);
            Assert.Equal(resultStatus.CurrentLayer, secondResultStatus.CurrentLayer);
            Assert.Equal(resultStatus.Errors, secondResultStatus.Errors);
            Assert.Equal(resultStatus.ErrorMessages, secondResultStatus.ErrorMessages);
        }
        
        public void AssertFailure(FailedOperationStatus expectedFailureType, int expectedErrors)
        {
            AssertFailure(resultStatus, expectedFailureType, ResultLayer.Unknown, expectedErrors);
        }

        public void AssertSuccessful(ResultLayer expectedLayer = ResultLayer.Unknown)
        {
            AssertSuccessful(resultStatus, OperationStatus.Success(), expectedLayer);
        }
        
        public void AssertFailure(FailedOperationStatus expectedStatus, ResultLayer expectedLayer, int expectedErrors)
        {
            Assert.True(resultStatus.IsFailure);
            Assert.False(resultStatus.IsSuccessful);
            Assert.Equal(expectedStatus, resultStatus.PrimaryStatus);
            Assert.Equal(expectedLayer, resultStatus.CurrentLayer);
            Assert.Equal(expectedErrors, resultStatus.Errors.Count);
        }

        public void AssertFailureType(params StatusType[] expectedStatuses)
        {
            Assert.Equal(expectedStatuses.Contains(StatusType.OperationTimeout), resultStatus.OperationTimedOut);
            Assert.Equal(expectedStatuses.Contains(StatusType.InvalidRequest), resultStatus.IsAnInvalidRequest);
            Assert.Equal(expectedStatuses.Contains(StatusType.DomainViolation), resultStatus.IsDomainViolation);
            Assert.Equal(expectedStatuses.Contains(StatusType.NotAllowed), resultStatus.IsNotAllowed);
            Assert.Equal(expectedStatuses.Contains(StatusType.InvalidInput), resultStatus.IsInvalidInput);
            Assert.Equal(expectedStatuses.Contains(StatusType.NotFound), resultStatus.IsNotFound);
            Assert.Equal(expectedStatuses.Contains(StatusType.AlreadyExists), resultStatus.AlreadyExisting);
            Assert.Equal(expectedStatuses.Contains(StatusType.InvariantViolation), resultStatus.IsInvariantViolation);
            Assert.Equal(expectedStatuses.Contains(StatusType.ConcurrencyViolation), resultStatus.IsConcurrencyViolation);
            Assert.Equal(expectedStatuses.Contains(StatusType.OperationCancelled), resultStatus.OperationIsCancelled);
        }
        
        public void AssertSuccessful(Success expectedStatus, ResultLayer expectedLayer = ResultLayer.Unknown)
        {
            Assert.True(resultStatus.IsSuccessful);
            Assert.False(resultStatus.IsFailure);
            Assert.Equal(expectedStatus, resultStatus.PrimaryStatus);
            Assert.Equal(expectedLayer, resultStatus.CurrentLayer);
            Assert.Empty(resultStatus.Errors);
            Assert.Empty(resultStatus.ErrorMessages);
        }
        
    }
}