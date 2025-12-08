using DDD.Core.Results;
using DDD.Core.Results.ValueObjects;
using Results.Tests.Extensions;
using Xunit;

namespace Results.Tests;

public class ResultTests
{
    [Fact]
    public void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = Result.Pass();
        result.AssertSuccessful();
    }

    [Fact]
    public void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = Result.Fail();
        result.AssertFailure(FailureType.Generic, 1);
    }

    [Fact]
    public void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(errorMessage);
        result.AssertFailure(FailureType.Generic, 1);
    }

    [Fact]
    public void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(errorMessage);
        var copiedResult = Result.Copy(result);
       result.AssertEquivalent(copiedResult);
    }

    [Fact]
    public void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = Result.Pass();    
        var copiedResult = Result.Copy(result);
       result.AssertEquivalent(copiedResult);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 1;
        var result = Result.Pass(value);
        var resultConverted = result.RemoveType();
        Assert.IsType<Result>(resultConverted);
        resultConverted.AssertSuccessful();
    }

    [Fact]
    public void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(errorMessage);
        Result<int> convertedResult = result;
       result.AssertEquivalent(convertedResult);
        Assert.ThrowsAny<Exception>(() => convertedResult.Output);
    }

    [Fact]
    public void GivenIHaveASuccessfulResult_WithoutAValue_WhenIConvertItIntoATypedResult_Then_AnErrorIsThrown()
    {
        var result = Result.Pass();
        Assert.ThrowsAny<Exception>(() =>
        {
            Result<int> _ = result;
        });
    }

    [Fact]
    public void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        const int value = 10;
        var result = Result.Pass(value);
        result.AssertSuccessful(value);    
    }

    [Fact]
    public void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = Result.Fail<int>();
        result.AssertFailure(FailureType.Generic, 1);    
    }

    [Fact]
    public void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(errorMessage);
        result.AssertFailure(FailureType.Generic, 1);
    }

    [Fact]
    public void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        const int value = 10;
        Result<int> convertedResult = value;
        convertedResult.AssertSuccessful(value);
    }

    [Fact]
    public void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(errorMessage);
        var copiedResult = Result.Copy(result);
       result.AssertEquivalent(copiedResult);
    }

    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const byte value = 20;
        var result = Result.Pass(value);    
        var copiedResult = Result.Copy(result);
       result.AssertEquivalent(copiedResult);
    }
    
    [Fact]
    public void GivenIHaveAManySuccessfulResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully()
    {
        var r1 = Result.Pass();
        var r2 = Result.Pass();
        var r3 = Result.Pass(5);
        var mergedResult = Result.Merge(r1, r2, r3);
        mergedResult.AssertSuccessful();
    }
    
    [Fact]
    public void GivenIHaveASomeSuccessfulAndSomeFailureResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully_AsAFailureResult()
    {
        var r1 = Result.Pass();
        var r2 = Result.Fail();
        var r3 = Result.Pass(1);
        var r4 = Result.Fail<string>("Error");
        var mergedResult = Result.Merge(r1, r2, r3, r4);
        Assert.True(mergedResult.IsFailure);
        Assert.False(mergedResult.IsSuccessful);
        Assert.Equal(FailureType.Generic, mergedResult.PrimaryFailureType);
        Assert.Equal(ResultLayer.Unknown, mergedResult.CurrentLayer);
        Assert.Equal(3, mergedResult.ErrorMessages.Count());
    }

    [Fact]
    public void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        const int value = 1111;
        Result<int> result = value;
        result.AssertSuccessful(value);
    }

    public static IEnumerable<object[]> FailureTypes =>
        new List<object[]>
        {
            new object[] { FailureType.Generic },
            new object[] { FailureType.OperationTimeout },
            new object[] { FailureType.InvalidRequest },
            new object[] { FailureType.DomainViolation },
            new object[] { FailureType.NotAllowed },
            new object[] { FailureType.InvalidInput },
            new object[] { FailureType.NotFound },
            new object[] { FailureType.AlreadyExists },
        };
    
    public static IEnumerable<object[]> FailedLayers =>
        new List<object[]>
        {
            new object[] { ResultLayer.Unknown },
            new object[] { ResultLayer.Infrastructure },
            new object[] { ResultLayer.Service },
            new object[] { ResultLayer.UseCase },
            new object[] { ResultLayer.Web },
        };
    
    public static IEnumerable<object[]> FailureTypesAndLayers =>
        new List<object[]>
        {
            new object[] { FailureType.Generic, ResultLayer.Unknown },
            new object[] { FailureType.OperationTimeout, ResultLayer.Infrastructure },
            new object[] { FailureType.InvalidRequest, ResultLayer.UseCase },
            new object[] { FailureType.DomainViolation, ResultLayer.Unknown },
            new object[] { FailureType.NotAllowed, ResultLayer.Infrastructure },
            new object[] { FailureType.InvalidInput, ResultLayer.Service },
            new object[] { FailureType.NotFound, ResultLayer.Unknown },
            new object[] { FailureType.AlreadyExists, ResultLayer.Web },
        };
    
    [Theory, MemberData(nameof(FailureTypes))]

    public void WhenIFailTheResult_WithAFailureType_Then_TheResultIsAFailure_TheCorrespondingFailureType(FailureType failureType)
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(failureType, errorMessage);
        result.AssertFailure(failureType, 1);
    }
    
    [Theory, MemberData(nameof(FailureTypes))]

    public void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAFailureType_Then_TheResultIsAFailure_TheCorrespondingFailureType(FailureType failureType)
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(failureType, errorMessage);
        result.AssertFailure(failureType, 1);
    }
    
    [Theory, MemberData(nameof(FailedLayers))]

    public void WhenIFailTheResult_WithAFailedLayer_Then_TheResultIsAFailure_TheCorrespondingFailedLayer(ResultLayer failedLayer)
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(failedLayer, errorMessage);
        result.AssertFailure(FailureType.Generic, failedLayer, 1);
    }
    
    [Theory, MemberData(nameof(FailedLayers))]

    public void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAFailedLayer_Then_TheResultIsAFailure_TheCorrespondingFailedLayer(ResultLayer failedLayer)
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(failedLayer, errorMessage);
        result.AssertFailure(FailureType.Generic, failedLayer, 1);
    }
    
    [Theory, MemberData(nameof(FailureTypesAndLayers))]

    public void WhenIFailTheResult_WithAFailureType_AndWithAFailedLayer_Then_TheResultIsAFailure_TheCorrespondingFailedLayer(FailureType failureType, ResultLayer failedLayer)
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(failureType, failedLayer, errorMessage);
        result.AssertFailure(failureType, failedLayer, 1);
    }
    
    [Theory, MemberData(nameof(FailureTypesAndLayers))]

    public void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAFailureType_AndWithAFailedLayer_Then_TheResultIsAFailure_TheCorrespondingFailedLayer(FailureType failureType, ResultLayer failedLayer)
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(failureType, failedLayer, errorMessage);
        result.AssertFailure(failureType, failedLayer, 1);
    }
    
    [Fact]
    public void WhenIFailTheResult_WithANoneFailureType_Then_TheResultShouldThrowError()
    {
        const string errorMessage = "I want it to fail";
        Assert.ThrowsAny<Exception>(() => Result.Fail(FailureType.None, errorMessage));
    }
    
    [Fact]
    public void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithANoneFailureType_Then_TheResultShouldThrowError()
    {
        const string errorMessage = "I want it to fail";
        Assert.ThrowsAny<Exception>(() => Result.Fail<long>(FailureType.None, errorMessage));
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(errorMessage);    
        var convertedResult = result.ToTypedResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToADifferentTypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(errorMessage);    
        var convertedResult = result.ToTypedResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }
}