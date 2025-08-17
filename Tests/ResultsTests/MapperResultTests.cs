using Outputs.Results;
using Outputs.Results.Abstract;
using OutputTests.Helpers;
using ValueObjects.Results;
using Xunit;

namespace OutputTests;

public class MapperResultTests : BasicResultTests
{
    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = MapperResult.Pass();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = MapperResult.Fail();
        ResultTestHelper.CheckFailure(result, FailureType.Mapper, FailureType.Mapper.ToMessage());
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Mapper, $"{FailureType.Mapper.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var mapperResult = MapperResult.Pass();
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckSuccess(result);
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = MapperResult.Fail(errorMessage);
        var result = mapperResult.ToResult();
        ResultTestHelper.CheckFailure(result, FailureType.Mapper, $"{FailureType.Mapper.ToMessage()} because {errorMessage}");
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;    
        var mapperResult = MapperResult.Pass(value);
        var result = mapperResult.RemoveValue();
        Assert.IsType<MapperResult>(result);
        ResultTestHelper.CheckSuccess(result);
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        const int value = 10;
        var result = MapperResult.Pass(value);
        ResultTestHelper.CheckSuccess(result, value);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = MapperResult.Fail<int>();
        ResultTestHelper.CheckFailure(result, FailureType.Mapper, FailureType.Mapper.ToMessage<int>());    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = MapperResult.Fail<int>(errorMessage);
        ResultTestHelper.CheckFailure(result, FailureType.Mapper, $"{FailureType.Mapper.ToMessage<int>()} because {errorMessage}");
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        const int value = 10;
        MapperResult<int> convertedResult = value;
        ResultTestHelper.CheckSuccess(convertedResult, value);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const int value = 10;
        var mapperResult = MapperResult.Pass(value);
        var result = mapperResult.ToResult();
        Assert.IsType<Result<int>>(result);
        ResultTestHelper.CheckSuccess(result, value);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var mapperResult = MapperResult.Fail<int>(errorMessage);
        var result = mapperResult.ToResult();
        Assert.IsType<Result<int>>(result);
        ResultTestHelper.CheckFailure(result, FailureType.Mapper, $"{FailureType.Mapper.ToMessage<int>()} because {errorMessage}");
    }
}