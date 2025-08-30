using Core.Results.Advanced;
using Core.Results.Base.Enums;
using OutputTests.Helpers;
using Xunit;

namespace OutputTests;

public class ResultTests
{
    [Fact]
    public void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = Result.Pass();
        ResultTestHelper.CheckSuccess(result);
    }

    [Fact]
    public void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = Result.Fail();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailureType.Generic.ToMessage());
    }

    [Fact]
    public void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    [Fact]
    public void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(errorMessage);
        var copiedResult = Result.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    [Fact]
    public void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = Result.Pass();    
        var copiedResult = Result.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 1;
        var result = Result.Pass(value);
        var resultConverted = result.RemoveType();
        Assert.IsType<Result>(resultConverted);
        ResultTestHelper.CheckSuccess(resultConverted);
    }

    [Fact]
    public void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(errorMessage);
        Result<int> convertedResult = result;
        ResultTestHelper.Equivalent(result, convertedResult);
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
        ResultTestHelper.CheckSuccess(result, value);    
    }

    [Fact]
    public void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = Result.Fail<int>();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailureType.Generic.ToMessage<int>());    
    }

    [Fact]
    public void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }

    [Fact]
    public void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        const int value = 10;
        Result<int> convertedResult = value;
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }

    [Fact]
    public void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(errorMessage);
        var copiedResult = Result.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const byte value = 20;
        var result = Result.Pass(value);    
        var copiedResult = Result.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }
    
    [Fact]
    public void GivenIHaveAManySuccessfulResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully()
    {
        var r1 = Result.Pass();
        var r2 = Result.Pass();
        var r3 = Result.Pass(5);
        var mergedResult = Result.Merge(r1, r2, r3);
        ResultTestHelper.CheckSuccess(mergedResult);
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
        Assert.Equal(FailureType.Generic, mergedResult.FailureType);
        Assert.Equal(FailedLayer.Unknown, mergedResult.FailedLayer);
        Assert.Equal(3, mergedResult.ErrorMessages.Count);
    }

    [Fact]
    public void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        const int value = 1111;
        Result<int> result = value;
        ResultTestHelper.CheckSuccess(result, value);
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
            new object[] { FailedLayer.Unknown },
            new object[] { FailedLayer.Infrastructure },
            new object[] { FailedLayer.Service },
            new object[] { FailedLayer.UseCase },
            new object[] { FailedLayer.Web },
        };
    
    public static IEnumerable<object[]> FailureTypesAndLayers =>
        new List<object[]>
        {
            new object[] { FailureType.Generic, FailedLayer.Unknown },
            new object[] { FailureType.OperationTimeout, FailedLayer.Infrastructure },
            new object[] { FailureType.InvalidRequest, FailedLayer.UseCase },
            new object[] { FailureType.DomainViolation, FailedLayer.Unknown },
            new object[] { FailureType.NotAllowed, FailedLayer.Infrastructure },
            new object[] { FailureType.InvalidInput, FailedLayer.Service },
            new object[] { FailureType.NotFound, FailedLayer.Unknown },
            new object[] { FailureType.AlreadyExists, FailedLayer.Web },
        };
    
    [Theory, MemberData(nameof(FailureTypes))]

    public void WhenIFailTheResult_WithAFailureType_Then_TheResultIsAFailure_TheCorrespondingFailureType(FailureType failureType)
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(failureType, errorMessage);
        ResultTestHelper.CheckFailure(result, failureType, $"{failureType.ToMessage()} because {errorMessage}");
    }
    
    [Theory, MemberData(nameof(FailureTypes))]

    public void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAFailureType_Then_TheResultIsAFailure_TheCorrespondingFailureType(FailureType failureType)
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(failureType, errorMessage);
        ResultTestHelper.CheckFailure(result, failureType, $"{failureType.ToMessage<int>()} because {errorMessage}");
    }
    
    [Theory, MemberData(nameof(FailedLayers))]

    public void WhenIFailTheResult_WithAFailedLayer_Then_TheResultIsAFailure_TheCorrespondingFailedLayer(FailedLayer failedLayer)
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(failedLayer, errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, failedLayer, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
    
    [Theory, MemberData(nameof(FailedLayers))]

    public void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAFailedLayer_Then_TheResultIsAFailure_TheCorrespondingFailedLayer(FailedLayer failedLayer)
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(failedLayer, errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, failedLayer, $"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }
    
    [Theory, MemberData(nameof(FailureTypesAndLayers))]

    public void WhenIFailTheResult_WithAFailureType_AndWithAFailedLayer_Then_TheResultIsAFailure_TheCorrespondingFailedLayer(FailureType failureType, FailedLayer failedLayer)
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(failureType, failedLayer, errorMessage);
        ResultTestHelper.CheckFailure(result, failureType, failedLayer, $"{failureType.ToMessage()} because {errorMessage}");
    }
    
    [Theory, MemberData(nameof(FailureTypesAndLayers))]

    public void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAFailureType_AndWithAFailedLayer_Then_TheResultIsAFailure_TheCorrespondingFailedLayer(FailureType failureType, FailedLayer failedLayer)
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(failureType, failedLayer, errorMessage);
        ResultTestHelper.CheckFailure(result, failureType, failedLayer, $"{failureType.ToMessage<int>()} because {errorMessage}");
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
    public void WhenIFailTheResult_WithNoFailedLevel_Then_TheResultShouldThrowError()
    {
        const string errorMessage = "I want it to fail";
        Assert.ThrowsAny<Exception>(() => Result.Fail(FailedLayer.None, errorMessage));
    }
    
    [Fact]
    public void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithANoFailedLevel_Then_TheResultShouldThrowError()
    {
        const string errorMessage = "I want it to fail";
        Assert.ThrowsAny<Exception>(() => Result.Fail<long>(FailedLayer.None, errorMessage));
    }
}