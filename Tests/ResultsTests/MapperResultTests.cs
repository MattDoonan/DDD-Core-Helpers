using DDD.Core.Results;
using DDD.Core.Results.ValueObjects;
using OutputTests.Extensions;
using OutputTests.TestStructures;
using Xunit;

namespace OutputTests;

public class MapperResultTests : BasicResultTests
{
    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = MapperResult.Pass();
        result.AssertSuccessful();
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = MapperResult.Fail();
        result.AssertFailure(FailureType.Generic, 1);
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);
        result.AssertFailure(FailureType.Generic, 1);
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var mapperResult = MapperResult.Pass();
        var result = mapperResult.ToResult();
        result.AssertSuccessful();
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = MapperResult.Fail(errorMessage);
        var result = mapperResult.ToResult();
        result.AssertFailure(FailureType.Generic, 1);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;    
        var mapperResult = MapperResult.Pass(value);
        var result = mapperResult.RemoveType();
        Assert.IsType<MapperResult>(result);
        result.AssertSuccessful();
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);
        var copiedResult = MapperResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = MapperResult.Pass();    
        var copiedResult = MapperResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);
        MapperResult<int> convertedResult = result;
        result.AssertEquivalent(convertedResult);
        Assert.ThrowsAny<Exception>(() => convertedResult.Output);
    }

    public override void GivenIHaveASuccessfulResult_WithoutAValue_WhenIConvertItIntoATypedResult_Then_AnErrorIsThrown()
    {
        var result = MapperResult.Pass();
        Assert.ThrowsAny<Exception>(() =>
        {
            MapperResult<int> _ = result;
        });
    }

    public override void GivenIHaveAManySuccessfulResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully()
    {
        var r1 = MapperResult.Pass();
        var r2 = MapperResult.Pass();
        var r3 = MapperResult.Pass(20);
        var mergedResult = MapperResult.Merge(r1, r2, r3);
        mergedResult.AssertSuccessful();
    }

    public override void GivenIHaveASomeSuccessfulAndSomeFailureResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully_AsAFailureResult()
    {
        var r1 = MapperResult.Pass();
        var r2 = MapperResult.Fail();
        var r3 = MapperResult.Pass(1);
        var r4 = MapperResult.Fail<int>("Error");
        var mergedResult = MapperResult.Merge(r1, r2, r3, r4);
        Assert.True(mergedResult.IsFailure);
        Assert.False(mergedResult.IsSuccessful);
        Assert.Equal(FailureType.Generic, mergedResult.PrimaryFailureType);
        Assert.Equal(ResultLayer.Unknown, mergedResult.CurrentLayer);
        Assert.Equal(3, mergedResult.ErrorMessages.Count());
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        const int value = 10;
        var result = MapperResult.Pass(value);
        result.AssertSuccessful(value);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = MapperResult.Fail<int>();
        result.AssertFailure(FailureType.Generic, 1);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<int>(errorMessage);
        result.AssertFailure(FailureType.Generic, 1);
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        const int value = 10;
        MapperResult<int> convertedResult = value;
        convertedResult.AssertSuccessful(value);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;
        var mapperResult = MapperResult.Pass(value);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        result.AssertSuccessful(value);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = MapperResult.Fail<int>(errorMessage);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        result.AssertFailure(FailureType.Generic, 1);
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<int>(errorMessage);
        var copiedResult = MapperResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var copiedResult = MapperResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        const int value = 1;
        MapperResult<int> result = value;
        result.AssertSuccessful(value);
    }

    [Fact]
    public void WhenIFailTheResult_BecauseOfADomainViolation_Then_TheResultIsAFailure()
    {
        var result = MapperResult.DomainViolation();
        result.AssertFailure(FailureType.DomainViolation, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfADomainViolation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = MapperResult.DomainViolation<int>();
        result.AssertFailure(FailureType.DomainViolation, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvalidInput_Then_TheResultIsAFailure()
    {
        var result = MapperResult.InvalidInput();
        result.AssertFailure(FailureType.InvalidInput, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvalidInput_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = MapperResult.InvalidInput<int>();
        result.AssertFailure(FailureType.InvalidInput, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvariantViolation_Then_TheResultIsAFailure()
    {
        var result = MapperResult.InvariantViolation();
        result.AssertFailure(FailureType.InvariantViolation, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvariantViolation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = MapperResult.InvariantViolation<int>();
        result.AssertFailure(FailureType.InvariantViolation, 1);    
    }

    [Fact]
    public void WhenIPassTheResult_WithANullableValue_AndThatValueIsNull_Then_TheResultIsSuccessful_AndHasTheNullValue()
    {
        int? value = null;
        var result = MapperResult.Pass(value);
        result.AssertSuccessful(value);  
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var convertedResult = result.ToTypedInfraResult();
        convertedResult.AssertSuccessful(value, ResultLayer.Infrastructure);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<byte>(errorMessage);    
        var convertedResult = result.ToTypedInfraResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAResponse()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var convertedResult = result.ToInfraResult();
        convertedResult.AssertSuccessful(ResultLayer.Infrastructure);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToInfraResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAResponse()
    {
        var result = MapperResult.Pass();    
        var convertedResult = result.ToInfraResult();
        convertedResult.AssertSuccessful(ResultLayer.Infrastructure);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);    
        var convertedResult = result.ToInfraResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure,1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var convertedResult = result.ToTypedServiceResult();
        convertedResult.AssertSuccessful(value, ResultLayer.Service);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<short>(errorMessage);    
        var convertedResult = result.ToTypedServiceResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertSuccessful(ResultLayer.Service);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertFailure(FailureType.Generic,  ResultLayer.Service,1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAServiceResult()
    {
        var result = MapperResult.Pass();    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertSuccessful(ResultLayer.Service);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var convertedResult = result.ToTypedUseCaseResult();
        convertedResult.AssertSuccessful(value, ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<long>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string value = "Hi";
        var result = MapperResult.Pass(value);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertSuccessful(ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<int>(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var result = MapperResult.Pass();    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertSuccessful(ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);    
        var convertedResult = result.ToTypedMapperResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToADifferentTypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<int>(errorMessage);    
        var convertedResult = result.ToTypedMapperResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }
}