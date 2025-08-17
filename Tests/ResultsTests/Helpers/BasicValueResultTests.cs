using Xunit;

namespace OutputTests.Helpers;

public abstract class BasicValueResultTests
{
    [Fact]
    public abstract void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue();

    [Fact]
    public abstract void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure();

    [Fact]
    public abstract void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage();
    
    [Fact]
    public abstract void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully();
    
    [Fact]
    public abstract void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully();
    
    [Fact]
    public abstract void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully();

}