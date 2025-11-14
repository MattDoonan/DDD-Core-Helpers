using DDD.Core.Results;
using DDD.Core.Results.Enums;
using DDD.Core.ValueObjects.Base;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.SingleValueObjects.Types;
using OutputTests.Helpers;
using Xunit;

namespace OutputTests;

public class ValueObjectResultTests : BasicValueResultTests
{
    private record TestValueObject(int Value)
        : NumberValueObject<int, TestValueObject>(Value), ISingleValueObjectFactory<int , TestValueObject>
    {
        public static ValueObjectResult<TestValueObject> Create(int value)
        {
            return new TestValueObject(value);
        }
    }

    private record TestValueObject2 : ValueObject;

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);
        ResultTestHelper.CheckSuccess(result, obj);
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ValueObjectResult.Fail<TestValueObject>();
        ResultTestHelper.CheckFailure(result, FailureType.Generic,
            FailureType.Generic.ToMessage<TestValueObject>());
    }

    public override void
        WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic,
            $"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
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
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, $"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
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

    public override void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        var obj = TestValueObject.Create(1).Output;
        ValueObjectResult<TestValueObject> result = obj;
        ResultTestHelper.CheckSuccess(result, obj);
    }

    [Fact]
    public void WhenIFailTheResult_BecauseOfADomainViolation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ValueObjectResult.DomainViolation<TestValueObject>();
        ResultTestHelper.CheckFailure(result, FailureType.DomainViolation, FailureType.DomainViolation.ToMessage<TestValueObject>());    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvalidInput_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ValueObjectResult.InvalidInput<TestValueObject>();
        ResultTestHelper.CheckFailure(result, FailureType.InvalidInput, FailureType.InvalidInput.ToMessage<TestValueObject>());    
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedEntityResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToTypedEntityResult();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedEntityResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedEntityResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, $"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAEntityResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToEntityResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAEntityResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToEntityResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, $"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
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
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, $"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
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
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, $"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToTypedInfraResult();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedInfraResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure, $"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAResponse()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToInfraResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToInfraResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure, $"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
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
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Service, $"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
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
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Service, $"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
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
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.UseCase, $"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
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
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.UseCase,$"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToADifferentTypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedValueObjectResult<TestValueObject2>();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic,$"{FailureType.Generic.ToMessage<TestValueObject>()} because {errorMessage}");
    }
}