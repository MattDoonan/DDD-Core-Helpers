using DDD.Core.Results;
using DDD.Core.Results.ValueObjects;
using Results.Tests.Extensions;
using Results.Tests.TestStructures;
using Xunit;

namespace Results.Tests.Results;

public class ServiceResultTests : BasicResultTests
{
    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = ServiceResult.Pass();
        result.AssertSuccessful(ResultLayer.Service);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = ServiceResult.Fail();
        result.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail(errorMessage);
        result.AssertFailure(FailureType.Generic,  ResultLayer.Service, 1);
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var serviceResult = ServiceResult.Pass();
        var result = serviceResult.ToResult();
        result.AssertSuccessful(ResultLayer.Service);
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var serviceResult = ServiceResult.Fail(errorMessage);
        var result = serviceResult.ToResult();
        result.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;    
        var serviceResult = ServiceResult.Pass(value);
        var result = serviceResult.RemoveType();
        Assert.IsType<ServiceResult>(result);
        result.AssertSuccessful(ResultLayer.Service);
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail(errorMessage);
        var copiedResult = ServiceResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = ServiceResult.Pass();    
        var copiedResult = ServiceResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail(errorMessage);
        ServiceResult<int> convertedResult = result;
        result.AssertEquivalent(convertedResult);
        Assert.ThrowsAny<Exception>(() => convertedResult.Output);
    }

    public override void GivenIHaveASuccessfulResult_WithoutAValue_WhenIConvertItIntoATypedResult_Then_AnErrorIsThrown()
    {
        var result = ServiceResult.Pass();
        Assert.ThrowsAny<Exception>(() =>
        {
            ServiceResult<int> _ = result;
        });
    }

    public override void GivenIHaveAManySuccessfulResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully()
    {
        var r1 = ServiceResult.Pass();
        var r2 = ServiceResult.Pass();
        var r3 = ServiceResult.Pass(5);
        var mergedResult = ServiceResult.Merge(r1, r2, r3);
        mergedResult.AssertSuccessful(ResultLayer.Service);
    }

    public override void GivenIHaveASomeSuccessfulAndSomeFailureResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully_AsAFailureResult()
    {
        var r1 = ServiceResult.Pass();
        var r2 = ServiceResult.Fail();
        var r3 = ServiceResult.Pass(1);
        var r4 = ServiceResult.Fail<string>("Error");
        var mergedResult = ServiceResult.Merge(r1, r2, r3, r4);
        Assert.True(mergedResult.IsFailure);
        Assert.False(mergedResult.IsSuccessful);
        Assert.Equal(FailureType.Generic, mergedResult.PrimaryFailureType);
        Assert.Equal(ResultLayer.Service, mergedResult.CurrentLayer);
        Assert.Equal(3, mergedResult.ErrorMessages.Count());
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        const int value = 10;
        var result = ServiceResult.Pass(value);
        result.AssertSuccessful(value, ResultLayer.Service);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ServiceResult.Fail<int>();
        result.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail<int>(errorMessage);
        result.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        const int value = 10;
        ServiceResult<int> convertedResult = value;
        convertedResult.AssertSuccessful(value, ResultLayer.Service);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;
        var serviceResult = ServiceResult.Pass(value);
        var result = serviceResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        result.AssertSuccessful(value, ResultLayer.Service);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var serviceResult = ServiceResult.Fail<int>(errorMessage);
        var result = serviceResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        result.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail<int>(errorMessage);
        var copiedResult = ServiceResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const byte value = 20;
        var result = ServiceResult.Pass(value);    
        var copiedResult = ServiceResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        const uint value = 11;
        ServiceResult<uint> result = value;
        result.AssertSuccessful(value, ResultLayer.Service);
    }

    [Fact]
    public void WhenIHaveADomainViolation_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.DomainViolation(errorMessage);
        result.AssertFailure(FailureType.DomainViolation, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void WhenIAmNotAllowedToExecuteFunction_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.NotAllowed(errorMessage);
        result.AssertFailure(FailureType.NotAllowed, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void WhenIHaveADomainViolation_WithAValue_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.DomainViolation<int>(errorMessage);
        result.AssertFailure(FailureType.DomainViolation, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void WhenIAmNotAllowedToExecuteFunction_WithAValue_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.NotAllowed<int>(errorMessage);
        result.AssertFailure(FailureType.NotAllowed, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfANotFoundError_Then_TheResultIsAFailure()
    {
        var result = ServiceResult.NotFound();
        result.AssertFailure(FailureType.NotFound, ResultLayer.Service, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfANotFoundError_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ServiceResult.NotFound<int>();
        result.AssertFailure(FailureType.NotFound, ResultLayer.Service, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvariantViolation_Then_TheResultIsAFailure()
    {
        var result = ServiceResult.InvariantViolation();
        result.AssertFailure(FailureType.InvariantViolation, ResultLayer.Service, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvariantViolation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ServiceResult.InvariantViolation<int>();
        result.AssertFailure(FailureType.InvariantViolation, ResultLayer.Service, 1);    
    }
    
    [Fact]
    public void WhenIPassTheResult_WithANullableValue_AndThatValueIsNull_Then_TheResultIsSuccessful_AndHasTheNullValue()
    {
        string? value = null;
        var result = ServiceResult.Pass(value);
        result.AssertSuccessful(value, ResultLayer.Service);  
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const long value = 10;
        var result = ServiceResult.Pass(value);    
        var convertedResult = result.ToTypedUseCaseResult();
        convertedResult.AssertSuccessful(value, ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const long value = 10;
        var result = ServiceResult.Pass(value);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertSuccessful(ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var result = ServiceResult.Pass();    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertSuccessful(ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase,1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail(errorMessage);    
        var convertedResult = result.ToTypedServiceResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service,1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToADifferentTypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = ServiceResult.Fail<int>(errorMessage);    
        var convertedResult = result.ToTypedServiceResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service,1);
    }
}