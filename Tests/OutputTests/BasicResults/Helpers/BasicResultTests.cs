using Outputs.Base.Interfaces;
using Xunit;

namespace OutputTests.BasicResults.Helpers;

public abstract class BasicResultTests<TResult>
    where TResult : IResultStatus, IResultStatusBase<TResult>
{
    [Fact]
    public void IHaveSuccessfulResult_ThenIsSuccessful_ShouldReturnTrue()
    {
        var result = TResult.Pass();
        Assert.True(result.IsSuccessful);
        Assert.False(result.IsFailure);
        Assert.Empty(result.SuccessLogs);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public void IHaveSuccessfulResult_WithSuccessLog_ThenIShouldHaveSuccessLog()
    {
        const string successLog = "Great success";
        var result = TResult.Pass(successLog);
        Assert.True(result.IsSuccessful);
        Assert.False(result.IsFailure);
        Assert.Contains(successLog, result.SuccessLogs);
    }

    [Fact]
    public void IHaveAFailureResult_ThenIsFailure_ShouldReturnTrue()
    {
        var result = TResult.Fail();
        Assert.False(result.IsSuccessful);
        Assert.True(result.IsFailure);
        Assert.Empty(result.SuccessLogs);
        Assert.Single(result.ErrorMessages);
    }

    [Fact]
    public void IHaveAFailureResult_WithAddedBecause_ThenIsErrorMessage_ShouldHaveBecause()
    {
        const string because = "i want it to fail";
        var result = TResult.Fail("i want it to fail");
        Assert.False(result.IsSuccessful);
        Assert.True(result.IsFailure);
        Assert.Empty(result.SuccessLogs);
        Assert.Single(result.ErrorMessages);
        Assert.Contains(because, result.ErrorMessages.FirstOrDefault());
    }


    [Fact]
    public void IHaveManyResults_WhenAllAreSuccessful_ThenMerge_ShouldReturnSuccessfulResults()
    {
        var result1 = TResult.Pass();
        var result2 = TResult.Pass();
        var combinedResults = TResult.Merge(result1, result2);
        Assert.True(combinedResults.IsSuccessful);
        Assert.False(combinedResults.IsFailure);
        Assert.Single(combinedResults.SuccessLogs);
        Assert.Empty(combinedResults.ErrorMessages);
    }

    [Fact]
    public void IHaveManyResults_WhenAllAreSuccessful_WithSuccessLogs_ThenMerge_ShouldReturnLogs()
    {
        const string successLog1 = "Great success";
        const string successLog2 = "Very nice";
        var result1 = TResult.Pass(successLog1);
        var result2 = TResult.Pass(successLog2);
        var result3 = TResult.Pass();
        var combinedResults = TResult.Merge(result1, result2, result3);
        Assert.True(combinedResults.IsSuccessful);
        Assert.False(combinedResults.IsFailure);
        Assert.Equal(3, combinedResults.SuccessLogs.Count);
        var expectedLogs = new[] { successLog1, successLog2 };
        Assert.All(expectedLogs, log => Assert.Contains(log, combinedResults.SuccessLogs));
        Assert.Empty(combinedResults.ErrorMessages);
    }

    [Fact]
    public void IHaveManyResults_WhenOneIsAFailure_ThenMerge_ShouldReturnFailureResult()
    {
        const string successLog = "Great success";
        const string failureMessage = "i want it to fail";
        var result1 = TResult.Pass(successLog);
        var result2 = TResult.Fail(failureMessage);
        var result3 = TResult.Pass();
        var combinedResults = TResult.Merge(result1, result2, result3);
        Assert.False(combinedResults.IsSuccessful);
        Assert.True(combinedResults.IsFailure);
        Assert.Single(combinedResults.SuccessLogs);
        Assert.Contains(successLog, combinedResults.SuccessLogs);
        Assert.Equal(2, combinedResults.ErrorMessages.Count);
        Assert.Contains(combinedResults.ErrorMessages, msg => msg.Contains(failureMessage));
    }
}