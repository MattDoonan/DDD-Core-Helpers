using OutputTests.BasicResults.Helpers;
using ValueObjects.Results;
using Xunit;

namespace OutputTests.BasicResults;

public class MapperResultTests : BasicResultTests<MapperResult>
{
    [Fact]
    public void IHaveASuccessfulResult_WithAValue_ThenIsSuccessful_ShouldReturnTrue()
    {
        const string expectedValue = "A Value";
        const string expectedLog = "A Description";
        var result = MapperResult.Pass<string>(expectedValue, expectedLog);
        Assert.True(result.IsSuccessful);
        Assert.False(result.IsFailure);
        Assert.Equal(expectedValue, result.Value);
        Assert.Contains(expectedLog, result.SuccessLogs);
        Assert.Empty(result.ErrorMessages);
    }
    
    [Fact]
    public void IHaveAFailureResult_ThenIsFailure_ShouldReturnTrue_AndValueShouldThrowError()
    {
        const string expectedError = "this should fail";
        var result = MapperResult.Fail<string>(expectedError);
        Assert.False(result.IsSuccessful);
        Assert.True(result.IsFailure);
        Assert.Empty(result.SuccessLogs);
        Assert.ThrowsAny<Exception>(() => result.Value);
        Assert.Single(result.ErrorMessages);
        Assert.Contains(expectedError, result.ErrorMessages.FirstOrDefault());
    }
}