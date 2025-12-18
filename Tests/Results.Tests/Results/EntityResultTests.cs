using DDD.Core.Entities;
using DDD.Core.Results;
using DDD.Core.Results.ValueObjects;
using DDD.Core.ValueObjects.Identifiers;
using Results.Tests.Results.Extensions;
using Results.Tests.TestStructures;
using Xunit;

namespace Results.Tests.Results;

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
        result.AssertSuccessful();
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = EntityResult.Fail();
        result.AssertFailure(FailureType.Generic, 1);
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);
        result.AssertFailure(FailureType.Generic, 1);
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var mapperResult = EntityResult.Pass();
        var result = mapperResult.ToResult();
        result.AssertSuccessful();
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = EntityResult.Fail(errorMessage);
        var result = mapperResult.ToResult();
        result.AssertFailure(FailureType.Generic, 1);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        var entityResult = EntityResult.Pass(value);
        var result = entityResult.RemoveType();
        Assert.IsType<EntityResult>(result);
        result.AssertSuccessful();
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);
        var copiedResult = EntityResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = EntityResult.Pass();    
        var copiedResult = EntityResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);
        EntityResult<TestEntityResult> convertedResult = result;
        result.AssertEquivalent(convertedResult);
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
        mergedResult.AssertSuccessful();    
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
        Assert.Equal(FailureType.Generic, mergedResult.PrimaryFailureType);
        Assert.Equal(ResultLayer.Unknown, mergedResult.CurrentLayer);
        Assert.Equal(3, mergedResult.ErrorMessages.Count());
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(value);
        result.AssertSuccessful(value);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = EntityResult.Fail<TestEntityResult>();
        result.AssertFailure(FailureType.Generic, 1);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);
        result.AssertFailure(FailureType.Generic, 1);
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        EntityResult<TestEntityResult> convertedResult = value;
        convertedResult.AssertSuccessful(value);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        var mapperResult = EntityResult.Pass(value);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<TestEntityResult>>(result);
        result.AssertSuccessful(value);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = EntityResult.Fail<TestEntityResult>(errorMessage);
        var result = mapperResult.ToTypedResult();
        Assert.IsType<Result<TestEntityResult>>(result);
        result.AssertFailure(FailureType.Generic, 1);
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);
        var copiedResult = EntityResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(value);    
        var copiedResult = EntityResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        EntityResult<TestEntityResult> convertedResult = value;
        convertedResult.AssertSuccessful(value);
    }

    [Fact]
    public void WhenIFailTheResult_BecauseOfADomainViolation_Then_TheResultIsAFailure()
    {
        var result = EntityResult.DomainViolation();
        result.AssertFailure(FailureType.DomainViolation, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfADomainViolation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = EntityResult.DomainViolation<TestEntityResult>();
        result.AssertFailure(FailureType.DomainViolation, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvalidInput_Then_TheResultIsAFailure()
    {
        var result = EntityResult.InvalidInput();
        result.AssertFailure(FailureType.InvalidInput, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvalidInput_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = EntityResult.InvalidInput<TestEntityResult>();
        result.AssertFailure(FailureType.InvalidInput, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfANotFoundError_Then_TheResultIsAFailure()
    {
        var result = EntityResult.NotFound();
        result.AssertFailure(FailureType.NotFound, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfANotFoundError_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = EntityResult.NotFound<int>();
        result.AssertFailure(FailureType.NotFound, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvariantViolation_Then_TheResultIsAFailure()
    {
        var result = EntityResult.InvariantViolation();
        result.AssertFailure(FailureType.InvariantViolation, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvariantViolation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = EntityResult.InvariantViolation<int>();
        result.AssertFailure(FailureType.InvariantViolation, 1);    
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedMapperResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToTypedMapperResult();
        convertedResult.AssertSuccessful(obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedMapperResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToTypedMapperResult();
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAMapperResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToMapperResult();
        convertedResult.AssertSuccessful();
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAMapperResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToMapperResult();
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAMapperResult()
    {
        var result = EntityResult.Pass();    
        var convertedResult = result.ToMapperResult();
        convertedResult.AssertSuccessful();
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAMapperResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);    
        var convertedResult = result.ToMapperResult();
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulAggregateRootResult_WithAValue_Then_ItCanBeConvertedToATypedRepoResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToTypedRepoResult();
        convertedResult.AssertSuccessful(convertedResult, ResultLayer.Infrastructure);
    }
    
    [Fact]
    public void GivenIHaveAFailureAggregateRootResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToARepoResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToTypedRepoResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToTypedInfraResult();
        convertedResult.AssertSuccessful(obj, ResultLayer.Infrastructure);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToTypedInfraResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAResponse()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToInfraResult();
        convertedResult.AssertSuccessful(ResultLayer.Infrastructure);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToInfraResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAResponse()
    {
        var result = EntityResult.Pass();    
        var convertedResult = result.ToInfraResult();
        convertedResult.AssertSuccessful(ResultLayer.Infrastructure);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAResponse()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);    
        var convertedResult = result.ToInfraResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToTypedServiceResult();
        convertedResult.AssertSuccessful(obj, ResultLayer.Service);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToTypedServiceResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertSuccessful(ResultLayer.Service);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAServiceResult()
    {
        var result = EntityResult.Pass();    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertSuccessful(ResultLayer.Service);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToTypedUseCaseResult();
        convertedResult.AssertSuccessful(obj, ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var obj = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(obj);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertSuccessful(ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var result = EntityResult.Pass();    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertSuccessful(ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);    
        var convertedResult = result.ToTypedEntityResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToADifferentTypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<int>(errorMessage);    
        var convertedResult = result.ToTypedEntityResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, 1);
    }
}