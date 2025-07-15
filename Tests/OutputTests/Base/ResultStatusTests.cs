using Outputs.Base;
using Outputs.Base.Interfaces;
using Xunit;

namespace OutputTests.Base;

public class ResultStatusTests
{
    private class TestResultStatus : ResultStatus
    {
        public TestResultStatus(string failureMessageStarter, string because) : base(failureMessageStarter, because)
        {
        }

        public TestResultStatus(string successLog) : base(successLog)
        {
        }
    }

    [Fact]
    public void IHaveASuccessfulResult_ThenSuccessful_ShouldBeTrue_AndFailed_ShouldBeFalse()
    {
        const string expectedLog = "";
        var result = new TestResultStatus(expectedLog);
        Assert.True(result.IsSuccessful);
        Assert.False(result.IsFailure);
        Assert.Empty(result.SuccessLogs);
        Assert.Empty(result.ErrorMessages);
    }
    
    [Fact]
    public void IHaveASuccessfulResult_WithAnInputtedSuccessLog_ThenTheLog_ShouldContainInput()
    {
        const string expectedLog = "This passed the test";
        var result = new TestResultStatus(expectedLog);
        Assert.True(result.IsSuccessful);
        Assert.False(result.IsFailure);
        Assert.Contains(expectedLog, result.SuccessLogs);
        Assert.Empty(result.ErrorMessages);
    }
    
    [Fact]
    public void IHaveAFailedResult_ThenSuccessful_ShouldBeFalse_AndFailed_ShouldBeTrue()
    {
        const string failureMessageStarter = "This passed the test";
        const string because = "we want to test the failure";
        var result = new TestResultStatus(failureMessageStarter, because);
        Assert.False(result.IsSuccessful);
        Assert.True(result.IsFailure);
        Assert.Empty(result.SuccessLogs);
    }
    
    [Theory] 
    [InlineData("This passed the test", "", "This passed the test")]
    [InlineData("This passed the test", "we want to test the failure", "This passed the test because we want to test the failure")]
    public void IHaveAFailedResult_WithAValidErrorMessage_ShouldFormatErrorMessage(string failureMessageStarter, string because, string expectedErrorMessage)
    {
        var result = new TestResultStatus(failureMessageStarter, because);
        Assert.False(result.IsSuccessful);
        Assert.True(result.IsFailure);
        Assert.Contains(expectedErrorMessage, result.ErrorMessages);
    }
    
}