using Base.Results.Advanced;
using Base.Results.Base.Enums;
using OutputTests.Helpers;
using Xunit;

namespace OutputTests;

public class ResultTests : BasicResultTests
{
    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = Result.Pass();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = Result.Fail();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailureType.Generic.ToMessage());
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var mapperResult = Result.Pass();
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = Result.Fail(errorMessage);
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;    
        var mapperResult = Result.Pass(value);
        var result = mapperResult.RemoveValue();
        Assert.IsType<Result>(result);
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail(errorMessage);
        var copiedResult = Result.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = Result.Pass();    
        var copiedResult = Result.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        const int value = 10;
        var result = Result.Pass(value);
        ResultTestHelper.CheckSuccess(result, value);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = Result.Fail<int>();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailureType.Generic.ToMessage<int>());    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        const int value = 10;
        Result<int> convertedResult = value;
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;
        var mapperResult = Result.Pass(value);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        ResultTestHelper.CheckSuccess(result, value);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = Result.Fail<int>(errorMessage);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = Result.Fail<int>(errorMessage);
        var copiedResult = Result.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const byte value = 20;
        var result = Result.Pass(value);    
        var copiedResult = Result.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public static IEnumerable<object[]> FailureTypes =>
        new List<object[]>
        {
            new object[] { FailureType.Generic },
            new object[] { FailureType.OperationTimeout },
            new object[] { FailureType.InvalidRequest },
            new object[] { FailureType.DomainViolation },
            new object[] { FailureType.NotAllowed },
            new object[] { FailureType.ValueObject },
            new object[] { FailureType.Mapper },
            new object[] { FailureType.Entity },
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
            new object[] { FailureType.InvalidRequest, FailedLayer.Infrastructure },
            new object[] { FailureType.DomainViolation, FailedLayer.Unknown },
            new object[] { FailureType.NotAllowed, FailedLayer.Infrastructure },
            new object[] { FailureType.ValueObject, FailedLayer.Service },
            new object[] { FailureType.Mapper, FailedLayer.UseCase },
            new object[] { FailureType.Entity, FailedLayer.Infrastructure },
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