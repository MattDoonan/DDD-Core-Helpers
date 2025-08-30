using Core.Results.Advanced;
using Core.Results.Base.Enums;
using OutputTests.Helpers;
using Xunit;

namespace OutputTests;

public class UseCaseResultTests : BasicResultTests
{
    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = UseCaseResult.Pass();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = UseCaseResult.Fail();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.UseCase, FailureType.Generic.ToMessage());
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = UseCaseResult.Fail(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.UseCase,$"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var useCaseResult = UseCaseResult.Pass();
        var result = useCaseResult.ToResult();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var useCaseResult = UseCaseResult.Fail(errorMessage);
        var result = useCaseResult.ToResult();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.UseCase,$"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        const string value = "Test";    
        var useCaseResult = UseCaseResult.Pass(value);
        var result = useCaseResult.RemoveType();
        Assert.IsType<UseCaseResult>(result);
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = UseCaseResult.Fail(errorMessage);
        var copiedResult = UseCaseResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = UseCaseResult.Pass();    
        var copiedResult = UseCaseResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = UseCaseResult.Fail(errorMessage);
        UseCaseResult<int> convertedResult = result;
        ResultTestHelper.Equivalent(result, convertedResult);
        Assert.ThrowsAny<Exception>(() => convertedResult.Output);
    }

    public override void GivenIHaveASuccessfulResult_WithoutAValue_WhenIConvertItIntoATypedResult_Then_AnErrorIsThrown()
    {
        var result = UseCaseResult.Pass();
        Assert.ThrowsAny<Exception>(() =>
        {
            UseCaseResult<int> _ = result;
        });
    }

    public override void GivenIHaveAManySuccessfulResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully()
    {
        var r1 = UseCaseResult.Pass();
        var r2 = UseCaseResult.Pass();
        var r3 = UseCaseResult.Pass(5);
        var mergedResult = UseCaseResult.Merge(r1, r2, r3);
        ResultTestHelper.CheckSuccess(mergedResult);
    }

    public override void GivenIHaveASomeSuccessfulAndSomeFailureResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully_AsAFailureResult()
    {
        var r1 = UseCaseResult.Pass();
        var r2 = UseCaseResult.Fail();
        var r3 = UseCaseResult.Pass(1);
        var r4 = UseCaseResult.Fail<string>("Error");
        var mergedResult = UseCaseResult.Merge(r1, r2, r3, r4);
        Assert.True(mergedResult.IsFailure);
        Assert.False(mergedResult.IsSuccessful);
        Assert.Equal(FailureType.Generic, mergedResult.FailureType);
        Assert.Equal(FailedLayer.UseCase, mergedResult.FailedLayer);
        Assert.Equal(3, mergedResult.ErrorMessages.Count);
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        const string value = "Test";
        var result = UseCaseResult.Pass(value);
        ResultTestHelper.CheckSuccess(result, value);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = UseCaseResult.Fail<string>();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.UseCase, FailureType.Generic.ToMessage<string>());    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = UseCaseResult.Fail<string>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.UseCase,$"{FailureType.Generic.ToMessage<string>()} because {errorMessage}");
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        const string value = "Test";
        UseCaseResult<string> convertedResult = value;
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string value = "Test";
        var useCaseResult = UseCaseResult.Pass(value);
        var result = useCaseResult.ToTypedResult();
        Assert.IsType<Result<string>>(result);
        ResultTestHelper.CheckSuccess(result, value);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var useCaseResult = UseCaseResult.Fail<string>(errorMessage);
        var result = useCaseResult.ToTypedResult();
        Assert.IsType<Result<string>>(result);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.UseCase,$"{FailureType.Generic.ToMessage<string>()} because {errorMessage}");
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = UseCaseResult.Fail<string>(errorMessage);
        var copiedResult = UseCaseResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const byte value = 20;
        var result = UseCaseResult.Pass(value);    
        var copiedResult = UseCaseResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        const byte value = 10;
        UseCaseResult<byte> result = value;
        ResultTestHelper.CheckSuccess(result, value);
    }
}