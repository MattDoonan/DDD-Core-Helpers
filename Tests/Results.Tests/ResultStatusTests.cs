using DDD.Core.Results.Base;
using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.ValueObjects;
using Results.Tests.Extensions;
using Xunit;

namespace Results.Tests;

public class ResultStatusTests
{
    private sealed class TestResult : ResultStatus
    {
        public TestResult(FailureType failureType, ResultLayer failedLayer, string? because) : base(failureType, failedLayer, because)
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
    
    public static IEnumerable<object[]> AllFailureTypes =>
        Enum.GetValues<FailureType>().Where(ft => ft != FailureType.None)
            .Select(ft => new object[] { ft });

    [Theory, MemberData(nameof(AllFailureTypes))]
    public void Create_Failure_WithFailureType_Should_SetCorrectVariables(FailureType failureType)
    {
        var result = new TestResult(failureType, ResultLayer.Unknown, string.Empty);
        result.AssertFailureType(failureType);
    }
    
    
}