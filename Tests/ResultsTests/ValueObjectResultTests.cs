using Core.Results.Advanced;
using Core.Results.Base.Enums;
using Core.Results.Basic;
using Core.ValueObjects.Regular.Base;
using Core.ValueObjects.Regular.Numbers;
using OutputTests.Helpers;
using Xunit;

namespace OutputTests;

public class ValueObjectResultTests : BasicValueResultTests
{
    private class TestValueObject(int value)
        : NumberValueObjectBase<int, TestValueObject>(value), IValueObject<int, TestValueObject>
    {
        public static ValueObjectResult<TestValueObject> Create(int value)
        {
            return new TestValueObject(value);
        }
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);
        ResultTestHelper.CheckSuccess(result, obj);
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ValueObjectResult.Fail<TestValueObject>();
        ResultTestHelper.CheckFailure(result, FailureType.ValueObject,
            FailureType.ValueObject.ToMessage<TestValueObject>());
    }

    public override void
        WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.ValueObject,
            $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        var obj = TestValueObject.Create(1).Output;
        ValueObjectResult<TestValueObject> convertedResult = obj;
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }

    public override void
        GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var obj = TestValueObject.Create(1).Output;
        var valueObjectResult = ValueObjectResult.Pass(obj);
        var convertedResult = valueObjectResult.ToTypedResult();
        Assert.IsType<Result<TestValueObject>>(convertedResult);
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var valueObjectResult = ValueObjectResult.Fail<TestValueObject>(errorMessage);
        var convertedResult = valueObjectResult.ToTypedResult();
        Assert.IsType<Result<TestValueObject>>(convertedResult);
        ResultTestHelper.CheckFailure(convertedResult, FailureType.ValueObject, $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);
        var copiedResult = ValueObjectResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var copiedResult = ValueObjectResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedMapperResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToTypedMapperResult();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedMapperResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedMapperResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.ValueObject, $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAMapperResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToMapperResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAMapperResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToMapperResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.ValueObject, $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToTypedResponse();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedResponse();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.ValueObject, FailedLayer.Infrastructure, $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAResponse()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToResponse();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToResponse();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.ValueObject, FailedLayer.Infrastructure, $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToTypedServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.ValueObject, FailedLayer.Service, $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.ValueObject, FailedLayer.Service, $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToTypedUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.ValueObject, FailedLayer.UseCase, $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.ValueObject, FailedLayer.UseCase, $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }
}