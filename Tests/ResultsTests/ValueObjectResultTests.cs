using Outputs.Results;
using Outputs.Results.Advanced;
using Outputs.Results.Base.Enums;
using Outputs.Results.Basic;
using OutputTests.Helpers;
using ValueObjects.Regular.Base;
using ValueObjects.Regular.Numbers;
using Xunit;

namespace OutputTests;

public class ValueObjectResultTests : BasicValueResultTests
{
    private class TestValueObject(int value)
        : NumberValueObjectBase<int, TestValueObject>(value), IValueObject<int, TestValueObject>
    {
        public static ValueObjectResult<TestValueObject> Create(int value)
        {
            return new TestValueObject(value);
        }
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);
        ResultTestHelper.CheckSuccess(result, obj);
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ValueObjectResult.Fail<TestValueObject>();
        ResultTestHelper.CheckFailure(result, FailureType.ValueObject,
            FailureType.ValueObject.ToMessage<TestValueObject>());
    }

    public override void
        WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.ValueObject,
            $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        var obj = TestValueObject.Create(1).Output;
        ValueObjectResult<TestValueObject> convertedResult = obj;
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }

    public override void
        GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var obj = TestValueObject.Create(1).Output;
        var valueObjectResult = ValueObjectResult.Pass(obj);
        var convertedResult = valueObjectResult.ToTypedResult();
        Assert.IsType<Result<TestValueObject>>(convertedResult);
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }

    public override void
        GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var valueObjectResult = ValueObjectResult.Fail<TestValueObject>(errorMessage);
        var convertedResult = valueObjectResult.ToTypedResult();
        Assert.IsType<Result<TestValueObject>>(convertedResult);
        ResultTestHelper.CheckFailure(convertedResult, FailureType.ValueObject,
            $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);
        var copiedResult = ValueObjectResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var obj = TestValueObject.Create(1).Output;
        var result = ValueObjectResult.Pass(obj);    
        var copiedResult = ValueObjectResult.Copy(result);
        ResultTestHelper.Equivalent(result, copiedResult);
    }
}