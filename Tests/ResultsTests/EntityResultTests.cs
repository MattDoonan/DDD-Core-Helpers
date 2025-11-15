using DDD.Core.Entities;
using DDD.Core.Results;
using DDD.Core.Results.Enums;
using DDD.Core.ValueObjects.Identifiers;
using OutputTests.Helpers;
using Xunit;

namespace OutputTests;

public class EntityResultTests : BasicResultTests
{
    private record TestId: AggregateRootId<int>
    {
        private TestId(int value) : base(value)
        {
            
        }
        
        public static ValueObjectResult<TestId> Create(int value)
        {
            return new TestId(value);
        }
    }

    private class TestEntityResult(TestId id) : AggregateRoot<TestId>(id);
    
    
    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = EntityResult.Pass();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = EntityResult.Fail();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailureType.Generic.ToMessage());
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var mapperResult = EntityResult.Pass();
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = EntityResult.Fail(errorMessage);
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        var mapperResult = EntityResult.Pass(value);
        var result = mapperResult.RemoveType();
        Assert.IsType<EntityResult>(result);
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);
        var copiedResult = EntityResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = EntityResult.Pass();    
        var copiedResult = EntityResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);
        EntityResult<TestEntityResult> convertedResult = result;
        ResultTestHelper.Equivalent(result, convertedResult);
        Assert.ThrowsAny<Exception>(() => convertedResult.Output);
    }

    public override void GivenIHaveASuccessfulResult_WithoutAValue_WhenIConvertItIntoATypedResult_Then_AnErrorIsThrown()
    {
        var result = EntityResult.Pass();
        Assert.ThrowsAny<Exception>(() =>
        {
            EntityResult<TestEntityResult> _ = result;
        });
    }

    public override void GivenIHaveAManySuccessfulResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully()
    {
        var r1 = EntityResult.Pass();
        var r2 = EntityResult.Pass();
        var r3 = EntityResult.Pass(new TestEntityResult(TestId.Create(1).Output));
        var mergedResult = EntityResult.Merge(r1, r2, r3);
        ResultTestHelper.CheckSuccess(mergedResult);    
    }

    public override void GivenIHaveASomeSuccessfulAndSomeFailureResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully_AsAFailureResult()
    {
        var r1 = EntityResult.Pass();
        var r2 = EntityResult.Fail();
        var r3 = EntityResult.Pass(new TestEntityResult(TestId.Create(1).Output));
        var r4 = EntityResult.Fail<TestEntityResult>("Error");
        var mergedResult = EntityResult.Merge(r1, r2, r3, r4);
        Assert.True(mergedResult.IsFailure);
        Assert.False(mergedResult.IsSuccessful);
        Assert.Equal(FailureType.Generic, mergedResult.FailureType);
        Assert.Equal(FailedLayer.Unknown, mergedResult.FailedLayer);
        Assert.Equal(3, mergedResult.ErrorMessages.Count);
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(value);
        ResultTestHelper.CheckSuccess(result, value);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = EntityResult.Fail<TestEntityResult>();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailureType.Generic.ToMessage<TestEntityResult>());    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage<TestEntityResult>()} because {errorMessage}");
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        EntityResult<TestEntityResult> convertedResult = value;
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        var mapperResult = EntityResult.Pass(value);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<TestEntityResult>>(result);
        ResultTestHelper.CheckSuccess(result, value);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = EntityResult.Fail<TestEntityResult>(errorMessage);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<TestEntityResult>>(result);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, $"{FailureType.Generic.ToMessage<TestEntityResult>()} because {errorMessage}");
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);
        var copiedResult = EntityResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(value);    
        var copiedResult = EntityResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        EntityResult<TestEntityResult> convertedResult = value;
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }

    [Fact]
    public void WhenIFailTheResult_BecauseOfADomainViolation_Then_TheResultIsAFailure()
    {
        var result = EntityResult.DomainViolation();
        ResultTestHelper.CheckFailure(result, FailureType.DomainViolation, FailureType.DomainViolation.ToMessage());    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfADomainViolation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = EntityResult.DomainViolation<TestEntityResult>();
        ResultTestHelper.CheckFailure(result, FailureType.DomainViolation, FailureType.DomainViolation.ToMessage<TestEntityResult>());    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvalidInput_Then_TheResultIsAFailure()
    {
        var result = EntityResult.InvalidInput();
        ResultTestHelper.CheckFailure(result, FailureType.InvalidInput, FailureType.InvalidInput.ToMessage());    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvalidInput_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = EntityResult.InvalidInput<TestEntityResult>();
        ResultTestHelper.CheckFailure(result, FailureType.InvalidInput, FailureType.InvalidInput.ToMessage<TestEntityResult>());    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfANotFoundError_Then_TheResultIsAFailure()
    {
        var result = EntityResult.NotFound();
        ResultTestHelper.CheckFailure(result, FailureType.NotFound, FailureType.NotFound.ToMessage());    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfANotFoundError_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = EntityResult.NotFound<int>();
        ResultTestHelper.CheckFailure(result, FailureType.NotFound, FailureType.NotFound.ToMessage<int>());    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvariantViolation_Then_TheResultIsAFailure()
    {
        var result = EntityResult.InvariantViolation();
        ResultTestHelper.CheckFailure(result, FailureType.InvariantViolation, FailureType.InvariantViolation.ToMessage());    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvariantViolation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = EntityResult.InvariantViolation<int>();
        ResultTestHelper.CheckFailure(result, FailureType.InvariantViolation, FailureType.InvariantViolation.ToMessage<int>());    
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedMapperResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToTypedMapperResult();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedMapperResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToTypedMapperResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, $"{FailureType.Generic.ToMessage<TestEntityResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAMapperResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToMapperResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAMapperResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToMapperResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, $"{FailureType.Generic.ToMessage<TestEntityResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAMapperResult()
    {
        var result = EntityResult.Pass();    
        var convertedResult = result.ToMapperResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAMapperResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);    
        var convertedResult = result.ToMapperResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulAggregateRootResult_WithAValue_Then_ItCanBeConvertedToATypedRepoResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToTypedRepoResult();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureAggregateRootResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToARepoResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToTypedRepoResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure, $"{FailureType.Generic.ToMessage<TestEntityResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToTypedInfraResult();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToTypedInfraResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure, $"{FailureType.Generic.ToMessage<TestEntityResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAResponse()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToInfraResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToInfraResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure, $"{FailureType.Generic.ToMessage<TestEntityResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAResponse()
    {
        var result = EntityResult.Pass();    
        var convertedResult = result.ToInfraResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);    
        var convertedResult = result.ToInfraResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure,$"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToTypedServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToTypedServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Service,$"{FailureType.Generic.ToMessage<TestEntityResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Service,$"{FailureType.Generic.ToMessage<TestEntityResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAServiceResult()
    {
        var result = EntityResult.Pass();    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Service, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToTypedUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.UseCase, $"{FailureType.Generic.ToMessage<TestEntityResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.UseCase,$"{FailureType.Generic.ToMessage<TestEntityResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var result = EntityResult.Pass();    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.UseCase, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);    
        var convertedResult = result.ToTypedEntityResult<string>();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToADifferentTypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<int>(errorMessage);    
        var convertedResult = result.ToTypedEntityResult<string>();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, $"{FailureType.Generic.ToMessage<int>()} because {errorMessage}");
    }
}