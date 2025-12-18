using DDD.Core.Results.Abstract;
using DDD.Core.Results.Exceptions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;
using Results.Tests.Results.Extensions;
using Xunit;

namespace Results.Tests.Results.Abstract;

public class ResultStatusTests
{
    [Fact]
    public void Create_ExpectedSuccessfulResultStatus_Should_BeSuccessful()
    {
        const ResultLayer layer = ResultLayer.Infrastructure;
        var result = new TestResult(layer);
        result.AssertSuccessful(layer);
    }
    
    [Fact]
    public void Create_ExpectedFailureResultStatus_Should_BeFailure()
    {
        const FailureType failureType = FailureType.DomainViolation;
        const ResultLayer layer = ResultLayer.Infrastructure;
        const string message = "i said so";
        var result = new TestResult(failureType, layer, message);
        result.AssertFailure(failureType, layer, 1);
        var expectedError = new ResultError(failureType, layer, message);
        Assert.Equivalent(expectedError, result.Errors.FirstOrDefault());
    }
    
    [Fact]
    public void Create_ResultStatusFromError_Should_BeFailure()
    {
        const FailureType failureType = FailureType.DomainViolation;
        const ResultLayer layer = ResultLayer.Infrastructure;
        const string message = "i said so";
        var expectedError = new ResultError(failureType, layer, message, typeof(string));
        var result = new TestResult(expectedError);
        result.AssertFailure(failureType, layer, 1);
        Assert.Equivalent(expectedError, result.Errors.FirstOrDefault());
    }
    
    [Fact]
    public void Create_ResultStatusFromPreviousSuccessfulResult_Should_BeSuccessful()
    {
        const ResultLayer layer = ResultLayer.Infrastructure;
        var firsResult = new TestResult(layer);
        var secondResult = new TestResult(firsResult);
        secondResult.AssertSuccessful(layer);
    }
    
    [Fact]
    public void Create_ResultStatusFromPreviousFailureResult_Should_BeFailure()
    {
        const FailureType failureType = FailureType.DomainViolation;
        const ResultLayer layer = ResultLayer.Infrastructure;
        const string message = "i said so";
        var expectedError = new ResultError(failureType, layer, message, typeof(string));
        var firstResult = new TestResult(expectedError);
        var secondResult = new TestResult(firstResult);
        secondResult.AssertFailure(failureType, layer, 1);
        Assert.Equivalent(expectedError, secondResult.Errors.FirstOrDefault());
    }

    [Theory, MemberData(nameof(AllFailureTypes))]
    public void Create_Failure_WithFailureType_Should_SetCorrectVariables(FailureType failureType)
    {
        var result = new TestResult(failureType, ResultLayer.Unknown, string.Empty);
        result.AssertFailureType(failureType);
    }
    
    [Theory, MemberData(nameof(FailureTypeInErrorsTestCases))]
    public void Create_Failure_WithManyFailureTypes_Should_SetCorrectVariables(TestResult result)
    {
        var expectedFailureType = result.Errors.Select(e => e.FailureType).ToList();
        expectedFailureType.Add(result.PrimaryFailureType);
        result.AssertFailureType(expectedFailureType.ToArray());
    }
    
    [Theory, MemberData(nameof(AllFailureTypes))]
    public void IsPrimaryFailure_Should_BeTrue_When_InputIsPrimaryFailureType(FailureType failureType)
    {
        var result = new TestResult(failureType, ResultLayer.Unknown, string.Empty);
        Assert.True(result.IsPrimaryFailure(failureType));
    }
    
    [Theory, MemberData(nameof(AllFailureTypes))]
    public void IsPrimaryFailure_Should_BeFalse_WhenInputIsNotPrimaryFailureType(FailureType failureType)
    {
        var result = new TestResult(ResultLayer.Unknown);
        Assert.False(result.IsPrimaryFailure(failureType));
    }
    
    [Theory, MemberData(nameof(AllFailureTypes))]
    public void SetPrimaryFailure_Should_UpdatePrimaryFailureType(FailureType failureType)
    {
        var result = new TestResult(ResultLayer.Unknown);
        result.SetPrimaryFailure(failureType);
        result.AssertFailure(failureType, ResultLayer.Unknown, 0);
    }
    
    [Fact]
    public void SetPrimaryFailure_WhenNewFailureTypeIsNone_ButThereAreNoErrors_Should_ConvertResultToSuccess()
    {
        var result = new TestResult(ResultLayer.Unknown);
        result.SetPrimaryFailure(FailureType.AlreadyExists);
        result.SetPrimaryFailure(FailureType.None);
        result.AssertSuccessful();
    }
    
    [Fact]
    public void SetPrimaryFailure_WhenNewFailureTypeIsNone_AndThereAreErrors_Should_ConvertResultToSuccess()
    {
        var result = new TestResult(FailureType.Generic, ResultLayer.Unknown, "test");
        Assert.Throws<ResultException>(() => result.SetPrimaryFailure(FailureType.None));
        result.AssertFailure(FailureType.Generic, ResultLayer.Unknown, 1);
    }
    
    [Theory, MemberData(nameof(AllLayers))]
    public void IsCurrentLayer_Should_BeTrue_WhenInputIsCurrentLayer(ResultLayer resultLayer)
    {
        var result = new TestResult(resultLayer);
        Assert.True(result.IsCurrentLayer(resultLayer));
    }
    
    [Theory, MemberData(nameof(AllLayers))]
    public void IsCurrentLayer_Should_BeFalse_WhenInputIsNotCurrentLayer(ResultLayer resultLayer)
    {
        var insertedLayer = (ResultLayer)(((int)resultLayer + 1) % Enum.GetValues<ResultLayer>().Length);
        var result = new TestResult(insertedLayer);
        Assert.False(result.IsCurrentLayer(resultLayer));
    }

    [Theory, MemberData(nameof(AllLayers))]
    public void ContainsErrorWithLayer_Should_BeTrue_WhenErrorsContainLayer(ResultLayer resultLayer)
    {
        var errors = CreateErrorsWithAllLayers().ToList();
        var result = new TestResult(FailureType.Generic, ResultLayer.Unknown, "test");
        result.AddErrors(errors);
        Assert.True(result.ContainsErrorWith(resultLayer));
    }
    
    [Theory, MemberData(nameof(AllFailureTypes))]
    public void ContainsErrorWithFailureType_Should_BeTrue_WhenErrorsContainFailureType(FailureType failureType)
    {
        var errors = CreateErrorsWithAllFailureTypes().ToList();
        var result = new TestResult(FailureType.Generic, ResultLayer.Unknown, "test");
        result.AddErrors(errors);
        Assert.True(result.ContainsErrorWith(failureType));
    }
    
    [Theory, MemberData(nameof(CombineWithSuccessfulResultTestCases))]
    public void CombineWith_WithASuccessfulResult_AndAnotherSuccessfulResult_Should_RemainSuccessful(ResultLayer firstResultsLayer, ResultLayer secondResultsLayer)
    {
        var firstResult = new TestResult(firstResultsLayer);
        var secondResult = new TestResult(secondResultsLayer);
        firstResult.CombineWith(secondResult);
        firstResult.AssertSuccessful(firstResultsLayer);
    }
    
    [Theory, MemberData(nameof(CombineWithOneFailureResultTestCases))]
    public void CombineWith_WithASuccessfulResult_AndAnotherFailureResult_Should_TurnToFailure(TestResult successfulResult, TestResult failureResult)
    {
        successfulResult.CombineWith(failureResult);
        successfulResult.AssertFailure(FailureType.Generic, successfulResult.CurrentLayer, failureResult.Errors.Count);
    }
    
    [Theory, MemberData(nameof(CombineWithOneFailureResultTestCases))]
    public void CombineWith_WithAFailureResult_AndAnotherSuccessfulResult_Should_TurnToFailure(TestResult successfulResult, TestResult failureResult)
    {
        failureResult.CombineWith(successfulResult);
        failureResult.AssertFailure(failureResult.PrimaryFailureType, failureResult.CurrentLayer, failureResult.Errors.Count);
    }
    
    [Theory, MemberData(nameof(CombineWithBothFailureResultTestCases))]
    public void CombineWith_WithTwoFailureResults_Should_RemainFailure(TestResult firstFailureResult, TestResult secondFailureResult)
    {
        var errorsCount = firstFailureResult.Errors.Count + secondFailureResult.Errors.Count;
        firstFailureResult.CombineWith(secondFailureResult);
        firstFailureResult.AssertFailure(FailureType.Generic, firstFailureResult.CurrentLayer, errorsCount);
    }
    
    [Fact]
    public void CombineWith_WithTwoFailureResults_AndFailureType_Should_RemainFailure_AndSetFailure()
    {
        var firstFailureResult = new TestResult(FailureType.InvalidInput, ResultLayer.Service, "first failure");
        var secondFailureResult = new TestResult(FailureType.DomainViolation, ResultLayer.Infrastructure, "second failure");
        var errorsCount = firstFailureResult.Errors.Count + secondFailureResult.Errors.Count;
        firstFailureResult.CombineWith(FailureType.NotAllowed, secondFailureResult);
        firstFailureResult.AssertFailure(FailureType.NotAllowed, firstFailureResult.CurrentLayer, errorsCount);
    }
    
    [Fact]
    public void CombineWithMany_WithMoreThanOneResult_Should_Update()
    {
        var startingResult = new TestResult(ResultLayer.Unknown);
        var result1 = new TestResult(FailureType.Generic, ResultLayer.Unknown, "first failure");
        var result2 = new TestResult(ResultLayer.Service);
        var result3 = CreateMultiErroredResult(
            new TestResult(FailureType.DomainViolation, ResultLayer.Infrastructure, "second failure"),
            new ResultError(FailureType.InvalidInput, ResultLayer.Service, "extra error"));
        var result4 = CreateMultiErroredResult(
            new TestResult(FailureType.InvariantViolation, ResultLayer.Infrastructure, "second failure"),
            new ResultError(FailureType.NotAllowed, ResultLayer.UseCase, "extra error"),
            new ResultError(FailureType.OperationCancelled, ResultLayer.UseCase, "extra error"));
        startingResult.CombineWith(result1, result2, result3, result4);
        startingResult.AssertFailure(FailureType.Generic, ResultLayer.Unknown, 6);
    }
    
    [Fact]
    public void CombineWithMany_WithMoreThanOneResult_AndAFailureType_Should_Update()
    {
        var startingResult = new TestResult(ResultLayer.Unknown);
        var result1 = new TestResult(FailureType.Generic, ResultLayer.Unknown, "first failure");
        var result2 = new TestResult(ResultLayer.Service);
        var result3 = CreateMultiErroredResult(
            new TestResult(FailureType.DomainViolation, ResultLayer.Infrastructure, "second failure"),
            new ResultError(FailureType.InvalidInput, ResultLayer.Service, "extra error"));
        var result4 = CreateMultiErroredResult(
            new TestResult(FailureType.InvariantViolation, ResultLayer.Infrastructure, "second failure"),
            new ResultError(FailureType.NotAllowed, ResultLayer.UseCase, "extra error"),
            new ResultError(FailureType.OperationCancelled, ResultLayer.UseCase, "extra error"));
        startingResult.CombineWith(FailureType.InvariantViolation, result1, result2, result3, result4);
        startingResult.AssertFailure(FailureType.InvariantViolation, ResultLayer.Unknown, 6);
    }
    
    [Fact]
    public void AddError_WhenResultIsFailure_Should_AddError()
    {
        var result = new TestResult(FailureType.Generic, ResultLayer.Unknown, "initial failure");
        var newError = new ResultError(FailureType.InvalidInput, ResultLayer.Service, "added failure");
        result.AddError(newError);
        result.AssertFailure(FailureType.Generic, ResultLayer.Unknown, 2);
        Assert.Contains(newError, result.Errors);
    }
    
    [Fact]
    public void AddError_WhenResultIsSuccess_Should_TurnResultToFailure_AndAddError()
    {
        var result = new TestResult(ResultLayer.Unknown);
        var newError = new ResultError(FailureType.OperationCancelled, ResultLayer.Service, "added failure");
        result.AddError(newError);
        result.AssertFailure(FailureType.Generic, ResultLayer.Unknown, 1);
        Assert.Contains(newError, result.Errors);
    }
    
    [Fact]
    public void AddErrors_WhenResultIsFailure_Should_AddErrors()
    {
        var result = new TestResult(FailureType.Generic, ResultLayer.Unknown, "initial failure");
        var newError1 = new ResultError(FailureType.InvalidInput, ResultLayer.Service, "added failure");
        var newError2 = new ResultError(FailureType.NotFound, ResultLayer.Infrastructure, "added failure 2");
        result.AddErrors(new List<ResultError> { newError1, newError2 });
        result.AssertFailure(FailureType.Generic, ResultLayer.Unknown, 3);
        Assert.Contains(newError1, result.Errors);
        Assert.Contains(newError2, result.Errors);
    }
    
    [Fact]
    public void AddError_WhenResultIsSuccess_Should_TurnResultToFailure_AndAddErrors()
    {
        var result = new TestResult(ResultLayer.Unknown);
        var newError1 = new ResultError(FailureType.ConcurrencyViolation, ResultLayer.Unknown, "added failure");
        var newError2 = new ResultError(FailureType.AlreadyExists, ResultLayer.UseCase, "added failure 2");
        result.AddErrors(new List<ResultError> { newError1, newError2 });
        result.AssertFailure(FailureType.Generic, ResultLayer.Unknown, 2);
        Assert.Contains(newError1, result.Errors);
        Assert.Contains(newError2, result.Errors);
    }
    
    [Fact]
    public void AddError_WithFailureType_Should_SetPrimaryType()
    {
        var result = new TestResult(ResultLayer.Unknown);
        var newError1 = new ResultError(FailureType.ConcurrencyViolation, ResultLayer.Unknown, "added failure");
        result.AddError(FailureType.AlreadyExists, newError1);
        result.AssertFailure(FailureType.AlreadyExists, ResultLayer.Unknown, 1);
        Assert.Contains(newError1, result.Errors);
    }
    
    [Theory, MemberData(nameof(AllFailureTypes))]
    public void GetErrorsByFailureType_WhenResultContainsErrorType_Should_ReturnErrors(FailureType failureType)
    {
        var errors = CreateErrorsWithAllFailureTypes().ToList();
        var result = new TestResult(FailureType.Generic, ResultLayer.Unknown, "test");
        result.AddErrors(errors);
        var retrievedErrors = result.GetErrorsBy(failureType).ToList();
        var expectedErrors = result.Errors.Where(e => e.FailureType == failureType).ToList();
        Assert.Equal(expectedErrors.Count, retrievedErrors.Count);
        Assert.All(retrievedErrors, re => Assert.Contains(re, expectedErrors));
    }

    [Theory, MemberData(nameof(AllFailureTypes))]
    public void GetErrorsByFailureType_WhenResultDoesNotContainErrorType_Should_ReturnNoErrors(FailureType failureType)
    {
        var result = new TestResult(ResultLayer.Unknown);
        var retrievedErrors = result.GetErrorsBy(failureType).ToList();
        Assert.Empty(retrievedErrors);
    }

    [Theory, MemberData(nameof(AllLayers))]
    public void GetErrorsByLayer_WhenResultContainsErrorLayer_Should_ReturnErrors(ResultLayer layer)
    {
        var errors = CreateErrorsWithAllLayers().ToList();
        var result = new TestResult(FailureType.Generic, ResultLayer.Unknown, "test");
        result.AddErrors(errors);
        var retrievedErrors = result.GetErrorsBy(layer).ToList();
        var expectedErrors = result.Errors.Where(e => e.ResultLayer == layer).ToList();
        Assert.Equal(expectedErrors.Count, retrievedErrors.Count);
        Assert.All(retrievedErrors, re => Assert.Contains(re, expectedErrors));
    }

    [Theory, MemberData(nameof(AllLayers))]
    public void GetErrorsByLayer_WhenResultDoesNotContainErrorLayer_Should_ReturnNoErrors(ResultLayer layer)
    {
        var result = new TestResult(ResultLayer.Unknown);
        var retrievedErrors = result.GetErrorsBy(layer).ToList();
        Assert.Empty(retrievedErrors);
    }
    
    [Fact]
    public void GetErrorsOfType_WhenResultContainsErrorsOfType_Should_ReturnErrors()
    {
        var errors = new List<ResultError>
        {
            new (FailureType.Generic, ResultLayer.Unknown, "test", typeof(string)),
            new (FailureType.InvalidInput, ResultLayer.Service, "test", typeof(int)),
            new (FailureType.DomainViolation, ResultLayer.Infrastructure, "test", typeof(string)),
        };
        var result = new TestResult(FailureType.Generic, ResultLayer.Unknown, "test");
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
            new (FailureType.Generic, ResultLayer.Unknown, "test", typeof(int)),
            new (FailureType.InvalidInput, ResultLayer.Service, "test", typeof(double)),
        };
        var result = new TestResult(FailureType.Generic, ResultLayer.Unknown, "test");
        result.AddErrors(errors);
        var retrievedErrors = result.GetErrorsOfType<string>().ToList();
        Assert.Empty(retrievedErrors);
    }

    [Fact]
    public void BothSuccessful_WhenBothResultsAreSuccessful_Should_ReturnTrue()
    {
        var result1 = new TestResult(ResultLayer.Service);
        var result2 = new TestResult(ResultLayer.Service);
        Assert.True(result1.BothSuccessful(result2));
    }

    [Fact]
    public void BothSuccessful_WhenOneResultIsFailure_Should_ReturnFalse()
    {
        var result1 = new TestResult(ResultLayer.Service);
        var result2 = new TestResult(FailureType.Generic, ResultLayer.Unknown, "test");
        Assert.False(result1.BothSuccessful(result2));
    }

    [Fact]
    public void BothSuccessful_WhenBothResultsAreFailure_Should_ReturnFalse()
    {
        var result1 = new TestResult(FailureType.Generic, ResultLayer.Unknown, "test");
        var result2 = new TestResult(FailureType.Generic, ResultLayer.Unknown, "test");
        Assert.False(result1.BothSuccessful(result2));
    }
    
    [Fact]
    public void Throw_Should_ThrowException()
    {
        var result = new TestResult(ResultLayer.Unknown);
        var exception = Record.Exception(() => result.Throw());
        Assert.NotNull(exception);
        Assert.IsType<ResultException>(exception);
    }
    
    [Fact]
    public void Throw_WithString_Should_ThrowException_WithMessage()
    {
        var result = new TestResult(ResultLayer.Unknown);
        var exception = Record.Exception(() => result.Throw("Test exception"));
        Assert.NotNull(exception);
        Assert.IsType<ResultException>(exception);
        Assert.Equal("Test exception", exception.Message);
    }
    
    public sealed class TestResult : ResultStatus
    {
        public TestResult(FailureType failureType, ResultLayer failedLayer, string? because) 
            : base(new ResultError(failureType, failedLayer, because))
        {
        }

        public TestResult(ResultLayer layer) : base(layer)
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
                    new TestResult(FailureType.Generic, ResultLayer.Infrastructure, null), 
                    new ResultError(FailureType.AlreadyExists, ResultLayer.Unknown),
                    new ResultError(FailureType.InvalidInput, ResultLayer.Unknown)),
            },
            
            new object[] { 
                CreateMultiErroredResult(
                    new TestResult(FailureType.ConcurrencyViolation, ResultLayer.UseCase, null), 
                    new ResultError(FailureType.InvariantViolation, ResultLayer.Infrastructure),
                    new ResultError(FailureType.DomainViolation, ResultLayer.Unknown),
                    new ResultError(FailureType.NotAllowed, ResultLayer.Infrastructure)),
            },
            new object[] { 
                CreateMultiErroredResult(
                    new TestResult(FailureType.ConcurrencyViolation, ResultLayer.UseCase, null), 
                    new ResultError(FailureType.OperationTimeout, ResultLayer.Service)),
            },
            new object[] { 
                CreateMultiErroredResult(
                    new TestResult(FailureType.OperationCancelled, ResultLayer.UseCase, null), 
                    new ResultError(FailureType.InvalidRequest, ResultLayer.Service)),
            },
        };
    
    public static IEnumerable<object[]> AllFailureTypes =>
        Enum.GetValues<FailureType>().Where(ft => ft != FailureType.None)
            .Select(ft => new object[] { ft });
    
    public static IEnumerable<object[]> AllLayers =>
        Enum.GetValues<ResultLayer>()
            .Select(rl => new object[] { rl });
    
    public static IEnumerable<object[]> CombineWithBothFailureResultTestCases =>
        new List<object[]>
        {
            new object[] { 
                new TestResult(FailureType.Generic, ResultLayer.Unknown, null), 
                CreateMultiErroredResult(
                    new TestResult(FailureType.Generic, ResultLayer.Unknown, null), 
                    new ResultError(FailureType.AlreadyExists, ResultLayer.Unknown, "extra error"),
                    new ResultError(FailureType.InvalidInput, ResultLayer.Service, "another one"))
            },
            new object[]
            {
                CreateMultiErroredResult(
                    new TestResult(FailureType.InvalidInput, ResultLayer.Service, "test"),
                    new ResultError(FailureType.DomainViolation, ResultLayer.Infrastructure)),
                new TestResult(FailureType.DomainViolation, ResultLayer.Unknown, "test")
            },
            new object[] { new TestResult(FailureType.NotAllowed, ResultLayer.Infrastructure, "test 2"),  new TestResult(FailureType.InvariantViolation, ResultLayer.Infrastructure, null) },
            new object[] { new TestResult(FailureType.Generic, ResultLayer.Unknown, "of something"),  new TestResult(FailureType.AlreadyExists, ResultLayer.Service, "why") },
            new object[] { new TestResult(FailureType.OperationCancelled, ResultLayer.UseCase, "a thing happened"), new TestResult(FailureType.OperationCancelled, ResultLayer.Service, null) },
        };
    
    public static IEnumerable<object[]> CombineWithOneFailureResultTestCases =>
        new List<object[]>
        {
            new object[] { new TestResult(ResultLayer.Unknown), new TestResult(FailureType.Generic, ResultLayer.Unknown, null) },
            new object[] { new TestResult(ResultLayer.Service),  new TestResult(FailureType.DomainViolation, ResultLayer.Unknown, "test") },
            new object[] { new TestResult(ResultLayer.Infrastructure),  new TestResult(FailureType.InvariantViolation, ResultLayer.Infrastructure, null) },
            new object[] { new TestResult(ResultLayer.Unknown),  new TestResult(FailureType.AlreadyExists, ResultLayer.Service, "why") },
            new object[] { new TestResult(ResultLayer.UseCase), new TestResult(FailureType.OperationCancelled, ResultLayer.Service, null) },
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
        return Enum.GetValues<ResultLayer>().Select(layer => new ResultError(FailureType.Generic, layer, "test"));
    }
    
    private static IEnumerable<ResultError> CreateErrorsWithAllFailureTypes()
    {
        return Enum.GetValues<FailureType>().Where(ft => ft != FailureType.None).Select(failureType => new ResultError(failureType, ResultLayer.Unknown, "test"));
    }
    
    private static TestResult CreateMultiErroredResult(TestResult result, params ResultError[] errors)
    {
        result.AddErrors(errors);
        return result;
    }
    
    private static ResultLayer GetExpectedLayer(IResultStatus result1, IResultStatus result2)
    {
        return result1.CurrentLayer is not ResultLayer.Unknown 
            ? result1.CurrentLayer 
            : result2.CurrentLayer;
    }
}