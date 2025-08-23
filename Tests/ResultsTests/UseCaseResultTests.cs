using Outputs.Results.Advanced;
using Outputs.Results.Base.Enums;
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
        var result = useCaseResult.RemoveValue();
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
}