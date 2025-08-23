using Entities.AggregateRoot;
using Outputs.Results.Advanced;
using Outputs.Results.Base.Enums;
using Outputs.Results.Basic;
using OutputTests.Helpers;
using ValueObjects.AggregateRootIdentifiers.Base;
using Xunit;

namespace OutputTests;

public class RepoResultTests : BasicValueResultTests
{
    
    private class TestId: AggregateRootIdBase<int>, IAggregateRootId<int, TestId>
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
        ResultTestHelper.CheckSuccess(result, obj);
    }
    
    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = RepoResult.Fail<TestRepoResult>();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.Infrastructure, FailureType.Generic.ToMessage<TestRepoResult>());
    }
    
    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<TestRepoResult>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.Infrastructure, $"{FailureType.Generic.ToMessage<TestRepoResult>()} because {errorMessage}");
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        RepoResult<TestRepoResult> convertedResult = obj;
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var repoResult = RepoResult.Pass(obj);
        var convertedResult = repoResult.ToTypedResult();
        Assert.IsType<Result<TestRepoResult>>(convertedResult);
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var repoResult = RepoResult.Fail<TestRepoResult>(errorMessage);
        var convertedResult = repoResult.ToTypedResult();
        Assert.IsType<Result<TestRepoResult>>(convertedResult);
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure, $"{FailureType.Generic.ToMessage<TestRepoResult>()} because {errorMessage}");
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<TestRepoResult>(errorMessage);
        var copiedResult = RepoResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var result = RepoResult.Pass(obj);    
        var copiedResult = RepoResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    [Fact]
    public void WhenICantFindTheValue_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.NotFound<TestRepoResult>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.NotFound, FailedLayer.Infrastructure, $"{FailureType.NotFound.ToMessage<TestRepoResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenITheValueAlreadyExists_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.AlreadyExists<TestRepoResult>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.AlreadyExists, FailedLayer.Infrastructure, $"{FailureType.AlreadyExists.ToMessage<TestRepoResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenIDoAnInvalidRequest_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.InvalidRequest<TestRepoResult>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.InvalidRequest, FailedLayer.Infrastructure, $"{FailureType.InvalidRequest.ToMessage<TestRepoResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void WhenITimoutAnOperation_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.OperationTimout<TestRepoResult>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.OperationTimeout, FailedLayer.Infrastructure, $"{FailureType.OperationTimeout.ToMessage<TestRepoResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var result = RepoResult.Pass(obj);    
        var convertedResult = result.ToTypedServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<TestRepoResult>(errorMessage);    
        var convertedResult = result.ToTypedServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure,$"{FailureType.Generic.ToMessage<TestRepoResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var result = RepoResult.Pass(obj);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAServiceResult()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<TestRepoResult>(errorMessage);    
        var convertedResult = result.ToServiceResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure,$"{FailureType.Generic.ToMessage<TestRepoResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var result = RepoResult.Pass(obj);    
        var convertedResult = result.ToTypedUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToATypedUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<TestRepoResult>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure,$"{FailureType.Generic.ToMessage<TestRepoResult>()} because {errorMessage}");
    }
    
    [Fact]
    public void GivenIHaveASuccessfulResult_WithAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        var result = RepoResult.Pass(obj);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckSuccess(convertedResult);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToAUseCaseResult()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail<TestRepoResult>(errorMessage);    
        var convertedResult = result.ToUseCaseResult();
        ResultTestHelper.CheckFailure(convertedResult, FailureType.Generic, FailedLayer.Infrastructure,$"{FailureType.Generic.ToMessage<TestRepoResult>()} because {errorMessage}");
    }
}