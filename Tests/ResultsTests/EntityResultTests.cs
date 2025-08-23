using Entities.AggregateRoot;
using Outputs.Results;
using Outputs.Results.Advanced;
using Outputs.Results.Base.Enums;
using Outputs.Results.Basic;
using OutputTests.Helpers;
using ValueObjects.AggregateRootIdentifiers.Base;
using Xunit;

namespace OutputTests;

public class EntityResultTests : BasicResultTests
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

    private class TestEntityResult(TestId id) : AggregateRoot<TestId>(id);
    
    
    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = EntityResult.Pass();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = EntityResult.Fail();
        ResultTestHelper.CheckFailure(result, FailureType.Entity, FailureType.Entity.ToMessage());
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Entity, $"{FailureType.Entity.ToMessage()} because {errorMessage}");
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
        ResultTestHelper.CheckFailure(result, FailureType.Entity, $"{FailureType.Entity.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        var mapperResult = EntityResult.Pass(value);
        var result = mapperResult.RemoveValue();
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

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        var value = new TestEntityResult(TestId.Create(1).Output);
        var result = EntityResult.Pass(value);
        ResultTestHelper.CheckSuccess(result, value);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = EntityResult.Fail<TestEntityResult>();
        ResultTestHelper.CheckFailure(result, FailureType.Entity, FailureType.Entity.ToMessage<TestEntityResult>());    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Entity, $"{FailureType.Entity.ToMessage<TestEntityResult>()} because {errorMessage}");
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
        ResultTestHelper.CheckFailure(result, FailureType.Entity, $"{FailureType.Entity.ToMessage<TestEntityResult>()} because {errorMessage}");
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = EntityResult.Fail<TestEntityResult>(errorMessage);
        var copiedResult = EntityResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }
}