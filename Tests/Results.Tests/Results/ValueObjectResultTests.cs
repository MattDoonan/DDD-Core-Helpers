using DDD.Core.Results;
using DDD.Core.Results.ValueObjects;
using DDD.Core.ValueObjects.Base;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.SingleValueObjects.Types;
using Results.Tests.Results.Extensions;
using Results.Tests.Results.TestStructures;
using Xunit;

namespace Results.Tests.Results;

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
        result.AssertSuccessful(obj);
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ValueObjectResult.Fail<TestValueObject>();
        result.AssertFailure(FailureType.Generic, 1);
    }

    public override void
        WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);
        result.AssertFailure(FailureType.Generic, 1);
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        var obj = TestValueObject.Create(1).Output;
        ValueObjectResult<TestValueObject> convertedResult = obj;
        convertedResult.AssertSuccessful(obj);
    }

    public override void
        GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var obj = TestValueObject.Create(1).Output;
        var valueObjectResult = ValueObjectResult.Pass(obj);
        var convertedResult = valueObjectResult.ToTypedResult();
        Assert.IsType<Result<TestValueObject>>(convertedResult);
        convertedResult.AssertSuccessful(obj);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var valueObjectResult = ValueObjectResult.Fail<TestValueObject>(errorMessage);
        var convertedResult = valueObjectResult.ToTypedResult();
        Assert.IsType<Result<TestValueObject>>(convertedResult);
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);
        var copiedResult = ValueObjectResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var copiedResult = ValueObjectResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        var obj = TestValueObject.Create(1).Output;
        ValueObjectResult<TestValueObject> result = obj;
        result.AssertSuccessful( obj);
    }

    [Fact]
    public void WhenIFailTheResult_BecauseOfADomainViolation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ValueObjectResult.DomainViolation<TestValueObject>();
        result.AssertFailure(FailureType.DomainViolation, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvalidInput_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ValueObjectResult.InvalidInput<TestValueObject>();
        result.AssertFailure(FailureType.InvalidInput, 1);    
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedEntityResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToTypedEntityResult();
        convertedResult.AssertSuccessful(obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedEntityResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedEntityResult();
        convertedResult.AssertFailure( FailureType.Generic, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAEntityResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToEntityResult();
        result.AssertSuccessful();
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAEntityResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToEntityResult();
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedMapperResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToTypedMapperResult();
        convertedResult.AssertSuccessful(obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedMapperResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedMapperResult();
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAMapperResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToMapperResult();
        result.AssertSuccessful();
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAMapperResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToMapperResult();
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToTypedInfraResult();
        convertedResult.AssertSuccessful(obj, ResultLayer.Infrastructure);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedInfraResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAResponse()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToInfraResult();
        result.AssertSuccessful();
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToInfraResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToTypedServiceResult();
        convertedResult.AssertSuccessful(obj, ResultLayer.Service);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedServiceResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToServiceResult();
        result.AssertSuccessful();
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToTypedUseCaseResult();
        convertedResult.AssertSuccessful(obj, ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var convertedResult = result.ToUseCaseResult();
        result.AssertSuccessful();
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase,1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToADifferentTypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);    
        var convertedResult = result.ToTypedValueObjectResult<TestValueObject2>();
        convertedResult.AssertFailure(FailureType.Generic,1);
    }
}