﻿using Core.Entities.AggregateRoot;
using Core.Results.Advanced;
using Core.Results.Base.Enums;
using Core.Results.Basic;
using Core.ValueObjects.AggregateRootIdentifiers.Base;
using OutputTests.Helpers;
using Xunit;

namespace OutputTests;

public class RepoResultTests : BasicResultTests
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

    public override void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        var obj = new TestRepoResult(TestId.Create(1).Output);
        RepoResult<TestRepoResult> convertedResult = obj;
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }

    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = RepoResult.Pass();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = RepoResult.Fail();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.Infrastructure, FailureType.Generic.ToMessage());
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.Infrastructure, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var mapperResult = RepoResult.Pass();
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = RepoResult.Fail(errorMessage);
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckFailure(result, FailureType.Generic, FailedLayer.Infrastructure, $"{FailureType.Generic.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        var value = new TestRepoResult(TestId.Create(1).Output);
        var mapperResult = RepoResult.Pass(value);
        var result = mapperResult.RemoveType();
        Assert.IsType<RepoResult>(result);
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail(errorMessage);
        var copiedResult = RepoResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = RepoResult.Pass();    
        var copiedResult = RepoResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.Fail(errorMessage);
        RepoResult<TestRepoResult> convertedResult = result;
        ResultTestHelper.Equivalent(result, convertedResult);
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
    public void WhenITimeoutAnOperation_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = RepoResult.OperationTimeout(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.OperationTimeout, FailedLayer.Infrastructure, $"{FailureType.OperationTimeout.ToMessage()} because {errorMessage}");
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