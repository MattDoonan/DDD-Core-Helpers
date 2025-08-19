using Outputs.Results;
using Outputs.Results.Abstract;
using OutputTests.Helpers;
using ValueObjects.Results;
using ValueObjects.Types.Regular.Base;
using ValueObjects.Types.Regular.Numbers;
using Xunit;

namespace OutputTests;

public class ValueObjectResultTests : BasicValueResultTests
{
    private class TestValueObject(int value) : NumberValueObjectBase<int, TestValueObject>(value), IValueObject<int, TestValueObject>
    {
        public static ValueObjectResult<TestValueObject> Create(int value)
        {
            return new TestValueObject(value);
        }
    }
    
    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        var obj = TestValueObject.Create(1).Content;
        var result = ValueObjectResult.Pass(obj);
        ResultTestHelper.CheckSuccess(result, obj);
    }
    
    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = ValueObjectResult.Fail<TestValueObject>();
        ResultTestHelper.CheckFailure(result, FailureType.ValueObject, FailureType.ValueObject.ToMessage<TestValueObject>());
    }
    
    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = ValueObjectResult.Fail<TestValueObject>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.ValueObject, $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        var obj = TestValueObject.Create(1).Content;
        ValueObjectResult<TestValueObject> convertedResult = obj;
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var obj = TestValueObject.Create(1).Content;
        var valueObjectResult = ValueObjectResult.Pass(obj);
        var convertedResult = valueObjectResult.ToResult();
        Assert.IsType<Result<TestValueObject>>(convertedResult);
        ResultTestHelper.CheckSuccess(convertedResult, obj);
    }
    
    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var valueObjectResult = ValueObjectResult.Fail<TestValueObject>(errorMessage);
        var convertedResult = valueObjectResult.ToResult();
        Assert.IsType<Result<TestValueObject>>(convertedResult);
        ResultTestHelper.CheckFailure(convertedResult, FailureType.ValueObject, $"{FailureType.ValueObject.ToMessage<TestValueObject>()} because {errorMessage}");
    }
}