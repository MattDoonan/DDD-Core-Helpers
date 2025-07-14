using Outputs.Base;
using Xunit;

namespace OutputTests.Base;

public class ResultValueTests
{
    private class TestResultValue<T> : ResultValue<T>
    {
        public TestResultValue(string failureMessageStarter, string because) : base(failureMessageStarter, because)
        {
        }

        public TestResultValue(T value, string successLog) : base(value, successLog)
        {
        }
    }
    
    private static void AssertSuccessful<T>(T value, string expectedLog = "")
    {
        var result = new TestResultValue<T>(value, expectedLog);
        Assert.True(result.IsSuccessful);
        Assert.False(result.IsFailure);
        Assert.Equal(value, result.Value);
    }
    
    
    [Fact]
    public void IHaveASuccessfulResult_WithStringValue_ShouldBeTrue_AndReturnValue()
    {
        AssertSuccessful("TestResultValue");
    }
    
    [Fact]
    public void IHaveASuccessfulResult_WithBoolValue_ShouldBeTrue_AndReturnValue()
    {
        AssertSuccessful(true);
    }
    
    [Fact]
    public void IHaveASuccessfulResult_WithLongValue_ShouldBeTrue_AndReturnValue()
    {
        AssertSuccessful(100L);
    }
    
    [Fact]
    public void IHaveASuccessfulResult_WithIntValue_ShouldBeTrue_AndReturnValue()
    {
        AssertSuccessful(10);
    }
    
    [Fact]
    public void IHaveASuccessfulResult_WithFloatValue_ShouldBeTrue_AndReturnValue()
    {
        AssertSuccessful(1.1f);
    }
    
    [Fact]
    public void IHaveASuccessfulResult_WithObjectValue_ShouldBeTrue_AndReturnValue()
    {
        AssertSuccessful(new object());
    }
    
    [Fact]
    public void IHaveASuccessfulResult_WithDateTimeValue_ShouldBeTrue_AndReturnValue()
    {
        AssertSuccessful(DateTime.MinValue);
    }
    
    [Fact]
    public void IHaveASuccessfulResult_WithTimeSpanValue_ShouldBeTrue_AndReturnValue()
    {
        AssertSuccessful(TimeSpan.MinValue);  
    }
    
    [Fact]
    public void IHaveASuccessfulResult_WithByteValue_ShouldBeTrue_AndReturnValue()
    {
        AssertSuccessful((byte)0b_1111_0000);
    }
    
    [Fact]
    public void IHaveAFailedResult_WhenIGetValue_ShouldThrowException()
    {
        var result = new TestResultValue<string>("This is a test", "it should fail");
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccessful);
        Assert.ThrowsAny<Exception>(() => result.Value);
    }
}