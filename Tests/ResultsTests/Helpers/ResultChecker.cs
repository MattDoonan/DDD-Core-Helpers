using DDD.Core.Results.Base;
using DDD.Core.Results.ValueObjects;
using Xunit;

namespace OutputTests.Helpers;

public static class ResultChecker
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
            Assert.ThrowsAny<Exception>(() => typedResult.Output);
        }

        public void AssertSuccessful(T expectedValue, ResultLayer expectedLayer = ResultLayer.Unknown)
        {
            AssertSuccessful(typedResult, expectedLayer);
            Assert.Equal(expectedValue, typedResult.Output);
        }
    }


    extension(ResultStatus resultStatus)
    {
        public void AssertEquivalent(ResultStatus secondResultStatus)
        {
            Assert.Equal(resultStatus.IsSuccessful, secondResultStatus.IsSuccessful);
            Assert.Equal(resultStatus.IsFailure, secondResultStatus.IsFailure);
            Assert.Equal(resultStatus.CurrentFailureType, secondResultStatus.CurrentFailureType);
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
            Assert.Equal(FailureType.None, resultStatus.CurrentFailureType);
            Assert.Equal(expectedLayer, resultStatus.CurrentLayer);
            Assert.Empty(resultStatus.Errors);
            Assert.Empty(resultStatus.ErrorMessages);
            AssertFailureTypes(resultStatus, FailureType.None);
        }
        
        public void AssertFailure(FailureType expectedFailureType, ResultLayer expectedLayer, int expectedErrors)
        {
            Assert.True(resultStatus.IsFailure);
            Assert.False(resultStatus.IsSuccessful);
            Assert.Equal(expectedFailureType, resultStatus.CurrentFailureType);
            Assert.Equal(expectedLayer, resultStatus.CurrentLayer);
            Assert.Equal(expectedErrors, resultStatus.Errors.Count);
            AssertFailureTypes(resultStatus, expectedFailureType);
        }

        private void AssertFailureTypes(FailureType expectedFailureType)
        {
            Assert.Equal(FailureType.OperationTimeout == expectedFailureType, resultStatus.OperationTimedOut);
            Assert.Equal(FailureType.InvalidRequest == expectedFailureType, resultStatus.IsAnInvalidRequest);
            Assert.Equal(FailureType.DomainViolation == expectedFailureType, resultStatus.IsADomainViolation);
            Assert.Equal(FailureType.NotAllowed == expectedFailureType, resultStatus.IsNotAllowed);
            Assert.Equal(FailureType.InvalidInput == expectedFailureType, resultStatus.IsInvalidInput);
            Assert.Equal(FailureType.NotFound == expectedFailureType, resultStatus.IsNotFound);
            Assert.Equal(FailureType.AlreadyExists == expectedFailureType, resultStatus.DoesAlreadyExists);
            Assert.Equal(FailureType.InvariantViolation == expectedFailureType, resultStatus.IsInvariantViolation);
            Assert.Equal(FailureType.ConcurrencyViolation == expectedFailureType, resultStatus.IsConcurrencyViolation);
            Assert.True(resultStatus.ContainsFailureType(expectedFailureType));
        }
    }
}