using Core.Results.Advanced;
using Core.Results.Base.Enums;
using Core.Results.Basic;
using OutputTests.Helpers;
using Xunit;

namespace OutputTests;

public class MapperResultTests : BasicResultTests
{
    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = MapperResult.Pass();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = MapperResult.Fail();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailureType.Generic.ToMessage());
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var mapperResult = MapperResult.Pass();
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = MapperResult.Fail(errorMessage);
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;    
        var mapperResult = MapperResult.Pass(value);
        var result = mapperResult.RemoveValue();
        Assert.IsType<MapperResult>(result);
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);
        var copiedResult = MapperResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = MapperResult.Pass();    
        var copiedResult = MapperResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        const int value = 10;
        var result = MapperResult.Pass(value);
        ResultTestHelper.CheckSuccess(result, value);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = MapperResult.Fail<int>();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailureType.Generic.ToMessage<int>());    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<int>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        const int value = 10;
        MapperResult<int> convertedResult = value;
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;
        var mapperResult = MapperResult.Pass(value);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        ResultTestHelper.CheckSuccess(result, value);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = MapperResult.Fail<int>(errorMessage);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<int>(errorMessage);
        var copiedResult = MapperResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var copiedResult = MapperResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfADomainViolation_Then_TheResultIsAFailure()
    {
        var result = MapperResult.DomainViolation();
        ResultTestHelper.CheckFailure(result, FailureType.DomainViolation, FailureType.DomainViolation.ToMessage());    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfADomainViolation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = MapperResult.DomainViolation<int>();
        ResultTestHelper.CheckFailure(result, FailureType.DomainViolation, FailureType.DomainViolation.ToMessage<int>());    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvalidInput_Then_TheResultIsAFailure()
    {
        var result = MapperResult.InvalidInput();
        ResultTestHelper.CheckFailure(result, FailureType.InvalidInput, FailureType.InvalidInput.ToMessage());    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvalidInput_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = MapperResult.InvalidInput<int>();
        ResultTestHelper.CheckFailure(result, FailureType.InvalidInput, FailureType.InvalidInput.ToMessage<int>());    
    }

    [Fact]
    public void WhenIPassTheResult_WithANullableValue_AndThatValueIsNull_Then_TheResultIsSuccessful_AndHasTheNullValue()
    {
        int? value = null;
        var result = MapperResult.Pass(value);
        ResultTestHelper.CheckSuccess(result, value);  
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var convertedResult = result.ToTypedInfraResult();
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<byte>(errorMessage);    
        var convertedResult = result.ToTypedInfraResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure, $"{FailureType.Generic.ToMessage<byte>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAResponse()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var convertedResult = result.ToInfraResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToInfraResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure,$"{FailureType.Generic.ToMessage<string>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAResponse()
    {
        var result = MapperResult.Pass();    
        var convertedResult = result.ToInfraResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);    
        var convertedResult = result.ToInfraResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure,$"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var convertedResult = result.ToTypedServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<short>(errorMessage);    
        var convertedResult = result.ToTypedServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Service, $"{FailureType.Generic.ToMessage<short>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic,  FailedLayer.Service,$"{FailureType.Generic.ToMessage<string>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAServiceResult()
    {
        var result = MapperResult.Pass();    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Service, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var convertedResult = result.ToTypedUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<long>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.UseCase, $"{FailureType.Generic.ToMessage<long>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<int>(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.UseCase, $"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var result = MapperResult.Pass();    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.UseCase, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
}