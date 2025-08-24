using Core.Results.Advanced;
using Core.Results.Base.Enums;
using OutputTests.Helpers;
using Xunit;

namespace OutputTests;

public class ServiceResultTests : BasicResultTests
{
    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = ServiceResult.Pass();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = ServiceResult.Fail();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.Service, FailureType.Generic.ToMessage());
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic,  FailedLayer.Service, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var mapperResult = ServiceResult.Pass();
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = ServiceResult.Fail(errorMessage);
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.Service, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;    
        var mapperResult = ServiceResult.Pass(value);
        var result = mapperResult.RemoveValue();
        Assert.IsType<ServiceResult>(result);
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail(errorMessage);
        var copiedResult = ServiceResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = ServiceResult.Pass();    
        var copiedResult = ServiceResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        const int value = 10;
        var result = ServiceResult.Pass(value);
        ResultTestHelper.CheckSuccess(result, value);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ServiceResult.Fail<int>();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.Service, FailureType.Generic.ToMessage<int>());    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail<int>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.Service, $"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        const int value = 10;
        ServiceResult<int> convertedResult = value;
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;
        var mapperResult = ServiceResult.Pass(value);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        ResultTestHelper.CheckSuccess(result, value);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = ServiceResult.Fail<int>(errorMessage);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.Service, $"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail<int>(errorMessage);
        var copiedResult = ServiceResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const byte value = 20;
        var result = ServiceResult.Pass(value);    
        var copiedResult = ServiceResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    [Fact]
    public void WhenIHaveADomainViolation_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.DomainViolation(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.DomainViolation, FailedLayer.Service, $"{FailureType.DomainViolation.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenIAmNotAllowedToExecuteFunction_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.NotAllowed(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.NotAllowed, FailedLayer.Service, $"{FailureType.NotAllowed.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenIHaveADomainViolation_WithAValue_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.DomainViolation<int>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.DomainViolation, FailedLayer.Service, $"{FailureType.DomainViolation.ToMessage<int>()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenIAmNotAllowedToExecuteFunction_WithAValue_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.NotAllowed<int>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.NotAllowed, FailedLayer.Service, $"{FailureType.NotAllowed.ToMessage<int>()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenIPassTheResult_WithANullableValue_AndThatValueIsNull_Then_TheResultIsSuccessful_AndHasTheNullValue()
    {
        string? value = null;
        var result = ServiceResult.Pass(value);
        ResultTestHelper.CheckSuccess(result, value);  
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const long value = 10;
        var result = ServiceResult.Pass(value);    
        var convertedResult = result.ToTypedUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Service,$"{FailureType.Generic.ToMessage<string>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const long value = 10;
        var result = ServiceResult.Pass(value);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Service,$"{FailureType.Generic.ToMessage<string>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var result = ServiceResult.Pass();    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Service,$"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
}