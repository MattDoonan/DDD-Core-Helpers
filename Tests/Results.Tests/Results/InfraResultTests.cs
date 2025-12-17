using DDD.Core.Results;
using DDD.Core.Results.ValueObjects;
using Results.Tests.Extensions;
using Results.Tests.TestStructures;
using Xunit;

namespace Results.Tests.Results;

public class InfraResultTests : BasicResultTests
{
    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = InfraResult.Pass();
        result.AssertSuccessful(ResultLayer.Infrastructure);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = InfraResult.Fail();
        result.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail(errorMessage);
        result.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var mapperResult = InfraResult.Pass();
        var result = mapperResult.ToResult();
        result.AssertSuccessful(ResultLayer.Infrastructure);
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = InfraResult.Fail(errorMessage);
        var result = mapperResult.ToResult();
        result.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;    
        var mapperResult = InfraResult.Pass(value);
        var result = mapperResult.RemoveType();
        Assert.IsType<InfraResult>(result);
        result.AssertSuccessful(ResultLayer.Infrastructure);
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail(errorMessage);
        var copiedResult = InfraResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = InfraResult.Pass();    
        var copiedResult = InfraResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail(errorMessage);
        InfraResult<int> convertedResult = result;
        result.AssertEquivalent(convertedResult);
        Assert.ThrowsAny<Exception>(() => convertedResult.Output);
    }

    public override void GivenIHaveASuccessfulResult_WithoutAValue_WhenIConvertItIntoATypedResult_Then_AnErrorIsThrown()
    {
        var result = InfraResult.Pass();
        Assert.ThrowsAny<Exception>(() =>
        {
            InfraResult<int> _ = result;
        });
    }

    public override void GivenIHaveAManySuccessfulResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully()
    {
        var r1 = InfraResult.Pass();
        var r2 = InfraResult.Pass();
        var r3 = InfraResult.Pass(20);
        var mergedResult = InfraResult.Merge(r1, r2, r3);
        mergedResult.AssertSuccessful(ResultLayer.Infrastructure);
    }

    public override void GivenIHaveASomeSuccessfulAndSomeFailureResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully_AsAFailureResult()
    {
        var r1 = InfraResult.Pass();
        var r2 = InfraResult.Fail();
        var r3 = InfraResult.Pass(1);
        var r4 = InfraResult.Fail<int>("Error");
        var mergedResult = InfraResult.Merge(r1, r2, r3, r4);
        Assert.True(mergedResult.IsFailure);
        Assert.False(mergedResult.IsSuccessful);
        Assert.Equal(FailureType.Generic, mergedResult.PrimaryFailureType);
        Assert.Equal(ResultLayer.Infrastructure, mergedResult.CurrentLayer);
        Assert.Equal(3, mergedResult.ErrorMessages.Count());
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        const int value = 10;
        var result = InfraResult.Pass(value);
        result.AssertSuccessful(value, ResultLayer.Infrastructure);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = InfraResult.Fail<int>();
        result.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<int>(errorMessage);
        result.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        const int value = 10;
        InfraResult<int> convertedResult = value;
        convertedResult.AssertSuccessful(value, ResultLayer.Infrastructure);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;
        var mapperResult = InfraResult.Pass(value);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        result.AssertSuccessful(value, ResultLayer.Infrastructure);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = InfraResult.Fail<int>(errorMessage);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        result.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<int>(errorMessage);
        var copiedResult = InfraResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const long value = 10;
        var result = InfraResult.Pass(value);    
        var copiedResult = InfraResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        const int value = 1;
        InfraResult<int> result = value;
        result.AssertSuccessful(value, ResultLayer.Infrastructure);
    }

    [Fact]
    public void WhenICantFindTheValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.NotFound(errorMessage);
        result.AssertFailure(FailureType.NotFound, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void WhenITheValueAlreadyExists_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.AlreadyExists(errorMessage);
        result.AssertFailure(FailureType.AlreadyExists, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void WhenIDoAnInvalidRequest_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.InvalidRequest(errorMessage);
        result.AssertFailure(FailureType.InvalidRequest, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void WhenITimeoutAnOperation_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.OperationTimeout(errorMessage);
        result.AssertFailure(FailureType.OperationTimeout, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void WhenICantFindTheValue_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.NotFound<int>(errorMessage);
        result.AssertFailure(FailureType.NotFound, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void WhenITheValueAlreadyExists_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.AlreadyExists<int>(errorMessage);
        result.AssertFailure(FailureType.AlreadyExists, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void WhenIDoAnInvalidRequest_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.InvalidRequest<int>(errorMessage);
        result.AssertFailure(FailureType.InvalidRequest, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void WhenITimeoutAnOperation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.OperationTimeout<int>(errorMessage);
        result.AssertFailure(FailureType.OperationTimeout, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const long value = 10;
        var result = InfraResult.Pass(value);    
        var convertedResult = result.ToTypedServiceResult();
        convertedResult.AssertSuccessful(value, ResultLayer.Service);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToTypedServiceResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const long value = 10;
        var result = InfraResult.Pass(value);    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertSuccessful(ResultLayer.Service);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAServiceResult()
    {
        var result = InfraResult.Pass();    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertSuccessful(ResultLayer.Service);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail(errorMessage);    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service,1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const long value = 10;
        var result = InfraResult.Pass(value);    
        var convertedResult = result.ToTypedUseCaseResult();
        convertedResult.AssertSuccessful(value, ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const long value = 10;
        var result = InfraResult.Pass(value);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertSuccessful(ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var result = InfraResult.Pass();    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertSuccessful(ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail(errorMessage);    
        var convertedResult = result.ToTypedInfraResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure,1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToADifferentTypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<int>(errorMessage);    
        var convertedResult = result.ToTypedInfraResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure,1);
    }
}