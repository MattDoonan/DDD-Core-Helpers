using DDD.Core.Results;
using DDD.Core.Results.ValueObjects;
using OutputTests.Helpers;
using Xunit;

namespace OutputTests;

public class InfraResultTests : BasicResultTests
{
    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = InfraResult.Pass();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = InfraResult.Fail();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, ResultLayer.Infrastructure, FailureType.Generic.ToMessage());
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, ResultLayer.Infrastructure, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var mapperResult = InfraResult.Pass();
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = InfraResult.Fail(errorMessage);
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, ResultLayer.Infrastructure, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;    
        var mapperResult = InfraResult.Pass(value);
        var result = mapperResult.RemoveType();
        Assert.IsType<InfraResult>(result);
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail(errorMessage);
        var copiedResult = InfraResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = InfraResult.Pass();    
        var copiedResult = InfraResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail(errorMessage);
        InfraResult<int> convertedResult = result;
        ResultTestHelper.Equivalent(result, convertedResult);
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
        ResultTestHelper.CheckSuccess(mergedResult);
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
        Assert.Equal(FailureType.Generic, mergedResult.FailureType);
        Assert.Equal(ResultLayer.Infrastructure, mergedResult.FailedLayer);
        Assert.Equal(3, mergedResult.ErrorMessages.Count);
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        const int value = 10;
        var result = InfraResult.Pass(value);
        ResultTestHelper.CheckSuccess(result, value);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = InfraResult.Fail<int>();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, ResultLayer.Infrastructure, FailureType.Generic.ToMessage<int>());    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<int>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, ResultLayer.Infrastructure, $"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        const int value = 10;
        InfraResult<int> convertedResult = value;
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;
        var mapperResult = InfraResult.Pass(value);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        ResultTestHelper.CheckSuccess(result, value);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = InfraResult.Fail<int>(errorMessage);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<int>>(result);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, ResultLayer.Infrastructure, $"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<int>(errorMessage);
        var copiedResult = InfraResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const long value = 10;
        var result = InfraResult.Pass(value);    
        var copiedResult = InfraResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        const int value = 1;
        InfraResult<int> result = value;
        ResultTestHelper.CheckSuccess(result, value);
    }

    [Fact]
    public void WhenICantFindTheValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.NotFound(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.NotFound, ResultLayer.Infrastructure, $"{FailureType.NotFound.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenITheValueAlreadyExists_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.AlreadyExists(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.AlreadyExists, ResultLayer.Infrastructure, $"{FailureType.AlreadyExists.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenIDoAnInvalidRequest_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.InvalidRequest(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.InvalidRequest, ResultLayer.Infrastructure, $"{FailureType.InvalidRequest.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenITimoutAnOperation_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.OperationTimout(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.OperationTimeout, ResultLayer.Infrastructure, $"{FailureType.OperationTimeout.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenICantFindTheValue_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.NotFound<int>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.NotFound, ResultLayer.Infrastructure, $"{FailureType.NotFound.ToMessage<int>()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenITheValueAlreadyExists_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.AlreadyExists<int>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.AlreadyExists, ResultLayer.Infrastructure, $"{FailureType.AlreadyExists.ToMessage<int>()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenIDoAnInvalidRequest_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.InvalidRequest<int>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.InvalidRequest, ResultLayer.Infrastructure, $"{FailureType.InvalidRequest.ToMessage<int>()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenITimoutAnOperation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.OperationTimout<int>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.OperationTimeout, ResultLayer.Infrastructure, $"{FailureType.OperationTimeout.ToMessage<int>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const long value = 10;
        var result = InfraResult.Pass(value);    
        var convertedResult = result.ToTypedServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToTypedServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, ResultLayer.Infrastructure,$"{FailureType.Generic.ToMessage<string>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const long value = 10;
        var result = InfraResult.Pass(value);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, ResultLayer.Infrastructure,$"{FailureType.Generic.ToMessage<string>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAServiceResult()
    {
        var result = InfraResult.Pass();    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail(errorMessage);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, ResultLayer.Infrastructure,$"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const long value = 10;
        var result = InfraResult.Pass(value);    
        var convertedResult = result.ToTypedUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, ResultLayer.Infrastructure,$"{FailureType.Generic.ToMessage<string>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const long value = 10;
        var result = InfraResult.Pass(value);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<string>(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, ResultLayer.Infrastructure,$"{FailureType.Generic.ToMessage<string>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var result = InfraResult.Pass();    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, ResultLayer.Infrastructure,$"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail(errorMessage);    
        var convertedResult = result.ToTypedInfraResult<string>();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, ResultLayer.Infrastructure,$"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToADifferentTypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = InfraResult.Fail<int>(errorMessage);    
        var convertedResult = result.ToTypedInfraResult<string>();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, ResultLayer.Infrastructure,$"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }
}