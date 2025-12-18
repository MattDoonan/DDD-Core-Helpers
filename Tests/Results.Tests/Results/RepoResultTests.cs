using DDD.Core.Entities;
using DDD.Core.Results;
using DDD.Core.Results.ValueObjects;
using DDD.Core.ValueObjects.Identifiers;
using Results.Tests.Results.Extensions;
using Results.Tests.TestStructures;
using Xunit;

namespace Results.Tests.Results;

public class RepoResultTests : BasicResultTests
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

    private class TestRepoResult(TestId id) : AggregateRoot<TestId>(id);
    
    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var result = RepoResult.Pass(obj);
        result.AssertSuccessful(obj, ResultLayer.Infrastructure);
    }
    
    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = RepoResult.Fail<TestRepoResult>();
        result.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }
    
    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<TestRepoResult>(errorMessage);
        result.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        RepoResult<TestRepoResult> convertedResult = obj;
        convertedResult.AssertSuccessful(obj, ResultLayer.Infrastructure);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var repoResult = RepoResult.Pass(obj);
        var convertedResult = repoResult.ToTypedResult();
        Assert.IsType<Result<TestRepoResult>>(convertedResult);
        convertedResult.AssertSuccessful(obj, ResultLayer.Infrastructure);
    }
    
    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var repoResult = RepoResult.Fail<TestRepoResult>(errorMessage);
        var convertedResult = repoResult.ToTypedResult();
        Assert.IsType<Result<TestRepoResult>>(convertedResult);
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<TestRepoResult>(errorMessage);
        var copiedResult = RepoResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var result = RepoResult.Pass(obj);    
        var copiedResult = RepoResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        RepoResult<TestRepoResult> convertedResult = obj;
        convertedResult.AssertSuccessful(obj, ResultLayer.Infrastructure);
    }

    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = RepoResult.Pass();
        result.AssertSuccessful(ResultLayer.Infrastructure);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = RepoResult.Fail();
        result.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail(errorMessage);
        result.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var mapperResult = RepoResult.Pass();
        var result = mapperResult.ToResult();
        result.AssertSuccessful(ResultLayer.Infrastructure);
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = RepoResult.Fail(errorMessage);
        var result = mapperResult.ToResult();
        result.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        var value = new TestRepoResult(TestId.Create(1).Output);
        var repoResult = RepoResult.Pass(value);
        var result = repoResult.RemoveType();
        Assert.IsType<RepoResult>(result);
        result.AssertSuccessful(ResultLayer.Infrastructure);
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail(errorMessage);
        var copiedResult = RepoResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = RepoResult.Pass();    
        var copiedResult = RepoResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail(errorMessage);
        RepoResult<TestRepoResult> convertedResult = result;
        result.AssertEquivalent(convertedResult);
        Assert.ThrowsAny<Exception>(() => convertedResult.Output);
    }

    public override void GivenIHaveASuccessfulResult_WithoutAValue_WhenIConvertItIntoATypedResult_Then_AnErrorIsThrown()
    {
        var result = RepoResult.Pass();
        Assert.ThrowsAny<Exception>(() =>
        {
            RepoResult<TestRepoResult> _ = result;
        });
    }

    public override void GivenIHaveAManySuccessfulResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully()
    {
        var r1 = RepoResult.Pass();
        var r2 = RepoResult.Pass();
        var r3 = RepoResult.Pass(new TestRepoResult(TestId.Create(1).Output));
        var mergedResult = RepoResult.Merge(r1, r2, r3);
        mergedResult.AssertSuccessful(ResultLayer.Infrastructure);
    }

    public override void GivenIHaveASomeSuccessfulAndSomeFailureResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully_AsAFailureResult()
    {
        var r1 = RepoResult.Pass();
        var r2 = RepoResult.Fail();
        var r3 = RepoResult.Pass(new TestRepoResult(TestId.Create(1).Output));
        var r4 = RepoResult.Fail<TestRepoResult>("Error");
        var mergedResult = RepoResult.Merge(r1, r2, r3, r4);
        Assert.True(mergedResult.IsFailure);
        Assert.False(mergedResult.IsSuccessful);
        Assert.Equal(FailureType.Generic, mergedResult.PrimaryFailureType);
        Assert.Equal(ResultLayer.Infrastructure, mergedResult.CurrentLayer);
        Assert.Equal(3, mergedResult.ErrorMessages.Count());
    }

    [Fact]
    public void WhenICantFindTheValue_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.NotFound<TestRepoResult>(errorMessage);
        result.AssertFailure(FailureType.NotFound, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void WhenITheValueAlreadyExists_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.AlreadyExists<TestRepoResult>(errorMessage);
        result.AssertFailure(FailureType.AlreadyExists, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void WhenIDoAnInvalidRequest_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.InvalidRequest<TestRepoResult>(errorMessage);
        result.AssertFailure(FailureType.InvalidRequest, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void WhenITimeoutAnOperation_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.OperationTimeout(errorMessage);
        result.AssertFailure(FailureType.OperationTimeout, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void WhenHaveAConcurrencyViolation_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.ConcurrencyViolation(errorMessage);
        result.AssertFailure(FailureType.ConcurrencyViolation, ResultLayer.Infrastructure, 1);
    }

    [Fact]
    public void WhenHaveAConcurrencyViolation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.ConcurrencyViolation<TestRepoResult>(errorMessage);
        result.AssertFailure(FailureType.ConcurrencyViolation, ResultLayer.Infrastructure, 1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var result = RepoResult.Pass(obj);    
        var convertedResult = result.ToTypedServiceResult();
        convertedResult.AssertSuccessful(obj, ResultLayer.Service);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<TestRepoResult>(errorMessage);    
        var convertedResult = result.ToTypedServiceResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service,1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var result = RepoResult.Pass(obj);    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertSuccessful(ResultLayer.Service);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<TestRepoResult>(errorMessage);    
        var convertedResult = result.ToServiceResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Service,1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var result = RepoResult.Pass(obj);    
        var convertedResult = result.ToTypedUseCaseResult();
        convertedResult.AssertSuccessful(obj, ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<TestRepoResult>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase,1);
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var result = RepoResult.Pass(obj);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertSuccessful(ResultLayer.UseCase);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<TestRepoResult>(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase,1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail(errorMessage);    
        var convertedResult = result.ToTypedRepoResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure,1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToADifferentTypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<int>(errorMessage);    
        var convertedResult = result.ToTypedRepoResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.Infrastructure, 1);
    }
}