using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Abstract;
using DDD.Core.Results.Exceptions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;
using Results.Tests.Helpers;
using Results.Tests.Results.Extensions;
using Xunit;

namespace Results.Tests.Results;

public class ResultStatusTests
{
    [Fact]
    public void Create_ExpectedSuccessfulResultStatus_Should_BeSuccessful()
    {
        const ResultLayer layer = ResultLayer.Infrastructure;
        var result = new TestResult(OperationStatus.Success(), layer);
        result.AssertSuccessful(layer);
    }
    
    [Fact]
    public void Create_ExpectedFailureResultStatus_Should_BeFailure()
    {
        var operationStatus = OperationStatus.DomainViolation();
        const ResultLayer layer = ResultLayer.Infrastructure;
        const string message = "i said so";
        var expectedError = new ResultError(operationStatus, layer, message);
        var result = new TestResult(expectedError);
        result.AssertFailure(operationStatus, layer, 1);
        Assert.Equivalent(expectedError, result.Errors.FirstOrDefault());
    }
    
    [Fact]
    public void Create_ResultStatusFromPreviousSuccessfulResult_Should_BeSuccessful()
    {
        const ResultLayer layer = ResultLayer.Infrastructure;
        var firsResult = new TestResult(OperationStatus.Success(), layer);
        var secondResult = new TestResult(firsResult);
        secondResult.AssertSuccessful(layer);
    }
    
    [Fact]
    public void Create_ResultStatusFromPreviousFailureResult_Should_BeFailure()
    {
        var operationStatus = OperationStatus.DomainViolation<string>();
        const ResultLayer layer = ResultLayer.Infrastructure;
        const string message = "i said so";
        var expectedError = new ResultError(operationStatus, layer, message);
        var firstResult = new TestResult(expectedError);
        var secondResult = new TestResult(firstResult);
        secondResult.AssertFailure(operationStatus, layer, 1);
        firstResult.AssertEquivalent(secondResult);
        Assert.Equivalent(expectedError, secondResult.Errors.FirstOrDefault());
    }

    [Theory, MemberData(nameof(AllFailureTypes))]
    public void Create_Failure_WithFailureType_Should_SetCorrectVariables(FailedOperationStatus failedOperationStatus)
    {
        var error = new ResultError(failedOperationStatus, ResultLayer.Unknown, string.Empty);
        var result = new TestResult(error);
        result.AssertFailureType(failedOperationStatus.Type);
    }
    
    [Theory, MemberData(nameof(FailureTypeInErrorsTestCases))]
    public void Create_Failure_WithManyFailureTypes_Should_SetCorrectVariables(TestResult result)
    {
        var expectedFailureType = result.Errors.Select(e => e.Failure.Type).ToList();
        expectedFailureType.Add(result.PrimaryStatus.Type);
        result.AssertFailureType(expectedFailureType.ToArray());
    }
    
    [Theory, MemberData(nameof(AllFailureTypes))]
    public void IsPrimaryStatus_Should_BeTrue_When_InputIsPrimaryFailureType(FailedOperationStatus operationStatus)
    {
        var error = new ResultError(operationStatus, ResultLayer.Unknown, string.Empty);
        var result = new TestResult(error);
        Assert.True(result.IsPrimaryStatus(operationStatus));
        Assert.True(result.IsPrimaryStatus(operationStatus.Type));
    }
    
    [Theory, MemberData(nameof(AllFailureTypes))]
    public void IsPrimaryStatus_Should_BeFalse_WhenInputIsNotPrimaryFailureType(FailedOperationStatus operationStatus)
    {
        var result = new TestResult(OperationStatus.Success<string>(), ResultLayer.Unknown);
        Assert.False(result.IsPrimaryStatus(operationStatus));
        Assert.False(result.IsPrimaryStatus(operationStatus.Type));
    }
    
    [Theory, MemberData(nameof(AllFailureTypes))]
    public void SetPrimaryFailure_Should_UpdatePrimaryFailureType(FailedOperationStatus operationStatus)
    {
        var result = new TestResult(OperationStatus.Success<string>(), ResultLayer.Unknown);
        result.SetPrimaryStatus(operationStatus);
        result.AssertFailure(operationStatus, ResultLayer.Unknown, 0);
    }
    
    [Fact]
    public void SetPrimaryFailure_WhenNewFailureTypeIsNone_ButThereAreNoErrors_Should_ConvertResultToSuccess()
    {
        var result = new TestResult(OperationStatus.Success(), ResultLayer.Unknown);
        result.SetPrimaryStatus(OperationStatus.AlreadyExists<string>());
        result.SetPrimaryStatus(OperationStatus.Success<int>());
        result.AssertSuccessful(OperationStatus.Success<int>());
    }
    
    [Fact]
    public void SetPrimaryFailure_WhenNewFailureTypeIsNone_AndThereAreErrors_Should_ConvertResultToSuccess()
    {
        var error = new ResultError(OperationStatus.InvalidInput(), ResultLayer.Unknown, "test");
        var result = new TestResult(error);
        Assert.Throws<ResultException>(() => result.SetPrimaryStatus(OperationStatus.Success()));
        result.AssertFailure(OperationStatus.InvalidInput(), ResultLayer.Unknown, 1);
    }
    
    [Theory, MemberData(nameof(AllLayers))]
    public void IsCurrentLayer_Should_BeTrue_WhenInputIsCurrentLayer(ResultLayer resultLayer)
    {
        var result = new TestResult(OperationStatus.Success<byte>(), resultLayer);
        Assert.True(result.IsCurrentLayer(resultLayer));
    }
    
    [Theory, MemberData(nameof(AllLayers))]
    public void IsCurrentLayer_Should_BeFalse_WhenInputIsNotCurrentLayer(ResultLayer resultLayer)
    {
        var insertedLayer = (ResultLayer)(((int)resultLayer + 1) % Enum.GetValues<ResultLayer>().Length);
        var result = new TestResult(OperationStatus.Success(), insertedLayer);
        Assert.False(result.IsCurrentLayer(resultLayer));
    }

    [Theory, MemberData(nameof(AllLayers))]
    public void ContainsErrorWithLayer_Should_BeTrue_WhenErrorsContainLayer(ResultLayer resultLayer)
    {
        var errors = CreateErrorsWithAllLayers().ToList();
        var error = new ResultError(OperationStatus.Failure(), resultLayer, "test");
        var result = new TestResult(error);
        result.AddErrors(errors);
        Assert.True(result.ContainsErrorWith(resultLayer));
    }
    
    [Theory, MemberData(nameof(AllFailureTypes))]
    public void ContainsErrorWithFailureType_Should_BeTrue_WhenErrorsContainFailureType(FailedOperationStatus operationStatus)
    {
        var errors = CreateErrorsWithAllFailureTypes().ToList();
        var error = new ResultError(operationStatus, ResultLayer.Unknown, "test");
        var result = new TestResult(error);
        result.AddErrors(errors);
        Assert.True(result.ContainsErrorWith(operationStatus.Type));
    }
    
    [Theory, MemberData(nameof(CombineWithSuccessfulResultTestCases))]
    public void CombineWith_WithASuccessfulResult_AndAnotherSuccessfulResult_Should_RemainSuccessful(ResultLayer firstResultsLayer, ResultLayer secondResultsLayer)
    {
        var firstResult = new TestResult(OperationStatus.Success(), firstResultsLayer);
        var secondResult = new TestResult(OperationStatus.Success(), secondResultsLayer);
        firstResult.CombineWith(secondResult);
        firstResult.AssertSuccessful(firstResultsLayer);
    }
    
    [Theory, MemberData(nameof(CombineWithOneFailureResultTestCases))]
    public void CombineWith_WithASuccessfulResult_AndAnotherFailureResult_Should_TurnToFailure(TestResult successfulResult, TestResult failureResult)
    {
        successfulResult.CombineWith(failureResult);
        successfulResult.AssertFailure(OperationStatus.Failure(), successfulResult.CurrentLayer, failureResult.Errors.Count);
    }
    
    [Theory, MemberData(nameof(CombineWithOneFailureResultTestCases))]
    public void CombineWith_WithAFailureResult_AndAnotherSuccessfulResult_Should_TurnToFailure(TestResult successfulResult, TestResult failureResult)
    {
        failureResult.CombineWith(successfulResult);
        failureResult.AssertFailure((FailedOperationStatus) failureResult.PrimaryStatus, failureResult.CurrentLayer, failureResult.Errors.Count);
    }
    
    [Theory, MemberData(nameof(CombineWithBothFailureResultTestCases))]
    public void CombineWith_WithTwoFailureResults_Should_RemainFailure(TestResult firstFailureResult, TestResult secondFailureResult)
    {
        var errorsCount = firstFailureResult.Errors.Count + secondFailureResult.Errors.Count;
        firstFailureResult.CombineWith(secondFailureResult);
        firstFailureResult.AssertFailure(OperationStatus.Failure(), firstFailureResult.CurrentLayer, errorsCount);
    }
    
    [Fact]
    public void CombineWith_WithTwoFailureResults_AndFailureType_Should_RemainFailure_AndSetFailure()
    {
        var firstError = new ResultError(OperationStatus.InvalidInput(), ResultLayer.Service, "first failure");
        var firstFailureResult = new TestResult(firstError);
        var secondError = new ResultError(OperationStatus.DomainViolation(), ResultLayer.Infrastructure, "second failure");
        var secondFailureResult = new TestResult(secondError);
        var errorsCount = firstFailureResult.Errors.Count + secondFailureResult.Errors.Count;
        firstFailureResult.CombineWith(OperationStatus.NotAllowed(), secondFailureResult);
        firstFailureResult.AssertFailure(OperationStatus.NotAllowed(), firstFailureResult.CurrentLayer, errorsCount);
    }
    
    [Fact]
    public void CombineWithMany_WithMoreThanOneResult_Should_Update()
    {
        var startingResult = new TestResult(OperationStatus.Success(), ResultLayer.Unknown);
        var result1 = new TestResult(new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, "first failure"));
        var result2 = new TestResult(OperationStatus.Success<string>(), ResultLayer.Service);
        var result3 = CreateMultiErroredResult(
            new TestResult(new ResultError(OperationStatus.DomainViolation(), ResultLayer.Infrastructure, "second failure")),
            new ResultError(OperationStatus.InvalidInput(), ResultLayer.Service, "extra error"));
        var result4 = CreateMultiErroredResult(
            new TestResult(new ResultError(OperationStatus.InvariantViolation(), ResultLayer.Infrastructure, "second failure")),
            new ResultError(OperationStatus.NotAllowed(), ResultLayer.UseCase, "extra error"),
            new ResultError(OperationStatus.Cancelled(), ResultLayer.UseCase, "extra error"));
        startingResult.CombineWith(result1, result2, result3, result4);
        startingResult.AssertFailure(OperationStatus.Failure(), ResultLayer.Unknown, 6);
    }
    
    [Fact]
    public void CombineWithMany_WithMoreThanOneResult_AndAFailureType_Should_Update()
    {
        var startingResult = new TestResult(OperationStatus.Success(), ResultLayer.Unknown);
        var result1 = new TestResult(new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, "first failure"));
        var result2 = new TestResult(OperationStatus.Success<byte>(), ResultLayer.Service);
        var result3 = CreateMultiErroredResult(
            new TestResult(new ResultError(OperationStatus.DomainViolation(), ResultLayer.Infrastructure, "second failure")),
            new ResultError(OperationStatus.InvalidInput(), ResultLayer.Service, "extra error"));
        var result4 = CreateMultiErroredResult(
            new TestResult(new ResultError(OperationStatus.InvariantViolation(), ResultLayer.Infrastructure, "second failure")),
            new ResultError(OperationStatus.NotAllowed(), ResultLayer.UseCase, "extra error"),
            new ResultError(OperationStatus.Cancelled(), ResultLayer.UseCase, "extra error"));
        startingResult.CombineWith(OperationStatus.InvariantViolation(), result1, result2, result3, result4);
        startingResult.AssertFailure(OperationStatus.InvariantViolation(), ResultLayer.Unknown, 6);
    }
    
    [Fact]
    public void AddError_WhenResultIsFailure_Should_AddError()
    {
        var oldError = new ResultError(OperationStatus.NotFound(), ResultLayer.Unknown, "initial failure");
        var result = new TestResult(oldError);
        var newError = new ResultError(OperationStatus.InvalidInput<string>(), ResultLayer.Service, "added failure");
        result.AddError(newError);
        result.AssertFailure(OperationStatus.Failure(), ResultLayer.Unknown, 2);
        Assert.Contains(oldError, result.Errors);
        Assert.Contains(newError, result.Errors);
    }
    
    [Fact]
    public void AddError_WhenResultIsSuccess_Should_TurnResultToFailure_AndAddError()
    {
        var result = new TestResult(OperationStatus.Success<string>(), ResultLayer.Unknown);
        var newError = new ResultError(OperationStatus.Cancelled(), ResultLayer.Service, "added failure");
        result.AddError(newError);
        result.AssertFailure(OperationStatus.Failure(), ResultLayer.Unknown, 1);
        Assert.Contains(newError, result.Errors);
    }
    
    [Fact]
    public void AddErrors_WhenResultIsFailure_Should_AddErrors()
    {
        var originalError = new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, "initial failure");
        var result = new TestResult(originalError);
        var newError1 = new ResultError(OperationStatus.InvalidInput(), ResultLayer.Service, "added failure");
        var newError2 = new ResultError(OperationStatus.NotFound(), ResultLayer.Infrastructure, "added failure 2");
        result.AddErrors(new List<ResultError> { newError1, newError2 });
        result.AssertFailure(OperationStatus.Failure(), ResultLayer.Unknown, 3);
        Assert.Contains(newError1, result.Errors);
        Assert.Contains(newError2, result.Errors);
    }
    
    [Fact]
    public void AddError_WhenResultIsSuccess_Should_TurnResultToFailure_AndAddErrors()
    {
        var result = new TestResult(OperationStatus.Success(), ResultLayer.Unknown);
        var newError1 = new ResultError(OperationStatus.ConcurrencyViolation(), ResultLayer.Unknown, "added failure");
        var newError2 = new ResultError(OperationStatus.AlreadyExists(), ResultLayer.UseCase, "added failure 2");
        result.AddErrors(new List<ResultError> { newError1, newError2 });
        result.AssertFailure(OperationStatus.Failure(), ResultLayer.Unknown, 2);
        Assert.Contains(newError1, result.Errors);
        Assert.Contains(newError2, result.Errors);
    }
    
    [Fact]
    public void AddError_WithFailureType_Should_SetPrimaryType()
    {
        var result = new TestResult(OperationStatus.Success(), ResultLayer.Unknown);
        var newError1 = new ResultError(OperationStatus.ConcurrencyViolation(), ResultLayer.Unknown, "added failure");
        result.AddError(OperationStatus.AlreadyExists(), newError1);
        result.AssertFailure(OperationStatus.AlreadyExists(), ResultLayer.Unknown, 1);
        Assert.Contains(newError1, result.Errors);
    }
    
    [Theory, MemberData(nameof(AllFailureTypes))]
    public void GetErrorsByFailureType_WhenResultContainsErrorType_Should_ReturnErrors(FailedOperationStatus failedOperationStatus)
    {
        var errors = CreateErrorsWithAllFailureTypes().ToList();
        var originalError = new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, "initial failure");
        var result = new TestResult(originalError);
        result.AddErrors(errors);
        var retrievedErrors = result.GetErrorsBy(failedOperationStatus.Type).ToList();
        var expectedErrors = result.Errors.Where(e => e.Failure.Type == failedOperationStatus.Type).ToList();
        Assert.Equal(expectedErrors.Count, retrievedErrors.Count);
        Assert.All(retrievedErrors, re => Assert.Contains(re, expectedErrors));
    }

    [Theory, MemberData(nameof(AllFailureTypes))]
    public void GetErrorsByFailureType_WhenResultDoesNotContainErrorType_Should_ReturnNoErrors(FailedOperationStatus failedOperationStatus)
    {
        var result = new TestResult(OperationStatus.Success(), ResultLayer.Unknown);
        var retrievedErrors = result.GetErrorsBy(failedOperationStatus.Type).ToList();
        Assert.Empty(retrievedErrors);
    }

    [Theory, MemberData(nameof(AllLayers))]
    public void GetErrorsByLayer_WhenResultContainsErrorLayer_Should_ReturnErrors(ResultLayer layer)
    {
        var errors = CreateErrorsWithAllLayers().ToList();
        var originalError = new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, "initial failure");
        var result = new TestResult(originalError);
        result.AddErrors(errors);
        var retrievedErrors = result.GetErrorsBy(layer).ToList();
        var expectedErrors = result.Errors.Where(e => e.ResultLayer == layer).ToList();
        Assert.Equal(expectedErrors.Count, retrievedErrors.Count);
        Assert.All(retrievedErrors, re => Assert.Contains(re, expectedErrors));
    }

    [Theory, MemberData(nameof(AllLayers))]
    public void GetErrorsByLayer_WhenResultDoesNotContainErrorLayer_Should_ReturnNoErrors(ResultLayer layer)
    {
        var result = new TestResult(OperationStatus.Success(), ResultLayer.Unknown);
        var retrievedErrors = result.GetErrorsBy(layer).ToList();
        Assert.Empty(retrievedErrors);
    }
    
    [Fact]
    public void GetErrorsOfType_WhenResultContainsErrorsOfType_Should_ReturnErrors()
    {
        var errors = new List<ResultError>
        {
            new (OperationStatus.Failure<string>(), ResultLayer.Unknown, "test"),
            new (OperationStatus.InvalidInput<int>(), ResultLayer.Service, "test"),
            new (OperationStatus.DomainViolation<string>(), ResultLayer.Infrastructure, "test"),
        };
        var originalError = new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, "initial failure");
        var result = new TestResult(originalError);
        result.AddErrors(errors);
        var retrievedErrors = result.GetErrorsOfType<string>().ToList();
        var expectedErrors = result.Errors.Where(e => e.OutputType == typeof(string)).ToList();
        Assert.Equal(expectedErrors.Count, retrievedErrors.Count);
        Assert.All(retrievedErrors, re => Assert.Contains(re, expectedErrors));
    }

    [Fact]
    public void GetErrorsOfType_WhenResultDoesNotContainErrorsOfType_Should_ReturnNoErrors()
    {
        var errors = new List<ResultError>
        {
            new (OperationStatus.Failure<int>(), ResultLayer.Unknown, "test"),
            new (OperationStatus.InvalidInput<double>(), ResultLayer.Service, "test"),
        };
        var originalError = new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, "initial failure");
        var result = new TestResult(originalError);
        result.AddErrors(errors);
        var retrievedErrors = result.GetErrorsOfType<string>().ToList();
        Assert.Empty(retrievedErrors);
    }

    [Fact]
    public void BothSuccessful_WhenBothResultsAreSuccessful_Should_ReturnTrue()
    {
        var result1 = new TestResult(OperationStatus.Success(), ResultLayer.Service);
        var result2 = new TestResult(OperationStatus.Success<double>(), ResultLayer.Service);
        Assert.True(result1.BothSuccessful(result2));
    }

    [Fact]
    public void BothSuccessful_WhenOneResultIsFailure_Should_ReturnFalse()
    {
        var result1 = new TestResult(OperationStatus.Success(), ResultLayer.Service);
        var error = new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, "test");
        var result2 = new TestResult(error);
        Assert.False(result1.BothSuccessful(result2));
    }

    [Fact]
    public void BothSuccessful_WhenBothResultsAreFailure_Should_ReturnFalse()
    {
        var error = new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, "test");
        var result1 = new TestResult(error);
        var result2 = new TestResult(error);
        Assert.False(result1.BothSuccessful(result2));
    }
    
    [Fact]
    public void Throw_Should_ThrowException()
    {
        var result = new TestResult(OperationStatus.Success(), ResultLayer.Unknown);
        var exception = Record.Exception(() => result.Throw());
        Assert.NotNull(exception);
        Assert.IsType<ResultException>(exception);
    }
    
    [Fact]
    public void Throw_WithString_Should_ThrowException_WithMessage()
    {
        var result = new TestResult(OperationStatus.Success(), ResultLayer.Unknown);
        var exception = Record.Exception(() => result.Throw("Test exception"));
        Assert.NotNull(exception);
        Assert.IsType<ResultException>(exception);
        Assert.Equal("Test exception", exception.Message);
    }
    
    public sealed class TestResult : ResultStatus
    {
        public TestResult(Success success, ResultLayer layer) : base(success, layer)
        {
        }

        public TestResult(IResultStatus result, ResultLayer? newResultLayer = null) : base(result, newResultLayer)
        {
        }

        public TestResult(ResultError error) : base(error)
        {
        }
    }
    
    public static IEnumerable<object[]> FailureTypeInErrorsTestCases =>
        new List<object[]>
        {
            new object[] { 
                CreateMultiErroredResult(
                    new TestResult(new ResultError(OperationStatus.Failure(), ResultLayer.Infrastructure, null)), 
                    new ResultError(OperationStatus.AlreadyExists(), ResultLayer.Unknown),
                    new ResultError(OperationStatus.InvalidInput(), ResultLayer.Unknown)),
            },
            
            new object[] { 
                CreateMultiErroredResult(
                    new TestResult(new ResultError(OperationStatus.ConcurrencyViolation(), ResultLayer.UseCase, null)), 
                    new ResultError(OperationStatus.InvariantViolation(), ResultLayer.Infrastructure),
                    new ResultError(OperationStatus.DomainViolation(), ResultLayer.Unknown),
                    new ResultError(OperationStatus.NotAllowed(), ResultLayer.Infrastructure)),
            },
            new object[] { 
                CreateMultiErroredResult(
                    new TestResult(new ResultError(OperationStatus.ConcurrencyViolation(), ResultLayer.UseCase, null)), 
                    new ResultError(OperationStatus.TimedOut(), ResultLayer.Service)),
            },
            new object[] { 
                CreateMultiErroredResult(
                    new TestResult(new ResultError(OperationStatus.InvalidRequest(), ResultLayer.Unknown)),
                    new ResultError(OperationStatus.InvalidRequest(), ResultLayer.Service)),
            },
        };

    public static IEnumerable<object[]> AllFailureTypes =>
        OperationStatuses.GetAllFailures().Select(fs => new object[] { fs });
    
    public static IEnumerable<object[]> AllLayers =>
        Enum.GetValues<ResultLayer>()
            .Select(rl => new object[] { rl });
    
    public static IEnumerable<object[]> CombineWithBothFailureResultTestCases =>
        new List<object[]>
        {
            new object[] { 
                new TestResult(new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, null)), 
                CreateMultiErroredResult(
                    new TestResult(new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, null)), 
                    new ResultError(OperationStatus.AlreadyExists(), ResultLayer.Unknown, "extra error"),
                    new ResultError(OperationStatus.InvalidInput(), ResultLayer.Service, "another one"))
            },
            new object[]
            {
                CreateMultiErroredResult(
                    new TestResult(new ResultError(OperationStatus.InvalidInput(), ResultLayer.Service, "test")),
                    new ResultError(OperationStatus.DomainViolation(), ResultLayer.Infrastructure)),
                new TestResult(new ResultError(OperationStatus.DomainViolation(), ResultLayer.Unknown, "test"))
            },
            new object[] { new TestResult(new ResultError(OperationStatus.NotAllowed(), ResultLayer.Infrastructure, "test 2")),  new TestResult(new ResultError(OperationStatus.InvariantViolation(), ResultLayer.Infrastructure, null)) },
            new object[] { new TestResult(new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, "of something")),  new TestResult(new ResultError(OperationStatus.AlreadyExists(), ResultLayer.Service, "why")) },
            new object[] { new TestResult(new ResultError(OperationStatus.Cancelled(), ResultLayer.UseCase, "a thing happened")), new TestResult(new ResultError(OperationStatus.Cancelled(), ResultLayer.Service, null)) },
        };
    
    public static IEnumerable<object[]> CombineWithOneFailureResultTestCases =>
        new List<object[]>
        {
            new object[] { new TestResult(OperationStatus.Success(), ResultLayer.Unknown), new TestResult(new ResultError(OperationStatus.Failure(), ResultLayer.Unknown, null)) },
            new object[] { new TestResult(OperationStatus.Success<int>(), ResultLayer.Service),  new TestResult(new ResultError(OperationStatus.DomainViolation(), ResultLayer.Unknown, "test")) },
            new object[] { new TestResult(OperationStatus.Success(), ResultLayer.Infrastructure),  new TestResult(new ResultError(OperationStatus.InvariantViolation(), ResultLayer.Infrastructure, null)) },
            new object[] { new TestResult(OperationStatus.Success<object>(), ResultLayer.Unknown),  new TestResult(new ResultError(OperationStatus.AlreadyExists(), ResultLayer.Service, "why")) },
            new object[] { new TestResult(OperationStatus.Success(), ResultLayer.UseCase), new TestResult(new ResultError(OperationStatus.Cancelled(), ResultLayer.Service, null)) },
        };
    
    public static IEnumerable<object[]> CombineWithSuccessfulResultTestCases =>
        new List<object[]>
        {
            new object[] { ResultLayer.Unknown, ResultLayer.Unknown },
            new object[] { ResultLayer.Service, ResultLayer.Unknown },
            new object[] { ResultLayer.Infrastructure, ResultLayer.Infrastructure },
            new object[] { ResultLayer.Unknown, ResultLayer.Service },
            new object[] { ResultLayer.UseCase, ResultLayer.Service },
        };
    
    private static IEnumerable<ResultError> CreateErrorsWithAllLayers()
    {
        return Enum.GetValues<ResultLayer>().Select(layer => new ResultError(OperationStatus.Failure(), layer, "test"));
    }
    
    private static IEnumerable<ResultError> CreateErrorsWithAllFailureTypes()
    {
        return OperationStatuses.GetAllFailures().Select(failureType => new ResultError(failureType, ResultLayer.Unknown, "test"));
    }
    
    private static TestResult CreateMultiErroredResult(TestResult result, params ResultError[] errors)
    {
        result.AddErrors(errors);
        return result;
    }
}