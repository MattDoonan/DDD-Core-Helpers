using Xunit;

namespace Results.Tests.TestStructures;

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
    
    [Fact]
    public abstract void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully();
    [Fact]
    public abstract void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult();
    
    [Fact]
    public abstract void GivenIHaveASuccessfulResult_WithoutAValue_WhenIConvertItIntoATypedResult_Then_AnErrorIsThrown();
    
    [Fact]
    public abstract void GivenIHaveAManySuccessfulResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully();

    [Fact]
    public abstract void GivenIHaveASomeSuccessfulAndSomeFailureResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully_AsAFailureResult();

}