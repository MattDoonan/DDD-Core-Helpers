using Xunit;

namespace OutputTests.Helpers;

public abstract class BasicResultTests : BasicValueResultTests
{
    [Fact]
    public abstract void WhenIPassTheResult_Then_TheResultIsSuccessful();

    [Fact]
    public abstract void WhenIFailTheResult_Then_TheResultIsAFailure();

    [Fact]
    public abstract void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage();
    
    [Fact]
    public abstract void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully();
    
    [Fact]
    public abstract void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully();
    
    [Fact]
    public abstract void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully();
    
    [Fact]
    public abstract void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully();

}