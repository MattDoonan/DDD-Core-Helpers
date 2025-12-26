using DDD.Core.Constants;
using DDD.Core.Results;
using Xunit;

namespace Results.Tests.Constants;

public class ExitCodeTests
{
    [Fact]
    public void ExitCode_Success_Should_Be0()
    {
        Assert.Equal(0, ExitCode.Success);
    }

    [Fact]
    public void ExitCode_Failure_Should_Be1()
    {
        Assert.Equal(1, ExitCode.Failure);
    }
    
    [Fact]
    public async Task FromResultAsync_Should_ReturnSuccessCode_WhenResultIsSuccessful()
    {
        var resultTask = Task.FromResult(Result.Pass());
        var exitCode = await ExitCode.FromResultAsync(resultTask);
        Assert.Equal(ExitCode.Success, exitCode);
    }

    [Fact]
    public async Task FromResultAsync_Should_ReturnFailureCode_WhenResultIsFailure()
    {
        var resultTask = Task.FromResult(Result.Fail("Error"));
        var exitCode = await ExitCode.FromResultAsync(resultTask);
        Assert.Equal(ExitCode.Failure, exitCode);
    }
    
    [Fact]
    public async Task FromTypedResultAsync_Should_ReturnSuccessCode_WhenResultIsSuccessful()
    {
        var resultTask = Task.FromResult(Result.Pass("Success"));
        var exitCode = await ExitCode.FromResultAsync(resultTask);
        Assert.Equal(ExitCode.Success, exitCode);
    }

    [Fact]
    public async Task FromTypedResultAsync_Should_ReturnFailureCode_WhenResultIsFailure()
    {
        var resultTask = Task.FromResult(Result.Fail<string>("Error"));
        var exitCode = await ExitCode.FromResultAsync(resultTask);
        Assert.Equal(ExitCode.Failure, exitCode);
    }
    
    [Fact]
    public void FromResult_Should_ReturnSuccessCode_WhenResultIsSuccessful()
    {
        var result = Result.Pass();
        var exitCode = ExitCode.FromBool(result.IsSuccessful);
        Assert.Equal(ExitCode.Success, exitCode);
    }

    [Fact]
    public void FromResult_Should_ReturnFailureCode_WhenResultIsFailure()
    {
        var result = Result.Fail("Error");
        var exitCode = ExitCode.FromBool(result.IsSuccessful);
        Assert.Equal(ExitCode.Failure, exitCode);
    }
    
    [Fact]
    public void FromTypedResult_Should_ReturnSuccessCode_WhenResultIsSuccessful()
    {
        var result = Result.Pass("Success");
        var exitCode = ExitCode.FromBool(result.IsSuccessful);
        Assert.Equal(ExitCode.Success, exitCode);
    }

    [Fact]
    public void FromTypedResult_Should_ReturnFailureCode_WhenResultIsFailure()
    {
        var result = Result.Fail<string>("Error");
        var exitCode = ExitCode.FromBool(result.IsSuccessful);
        Assert.Equal(ExitCode.Failure, exitCode);
    }

    [Fact]
    public void FromBool_Should_ReturnSuccess_WhenTrue()
    {
        var exitCode = ExitCode.FromBool(true);
        Assert.Equal(ExitCode.Success, exitCode);
    }

    [Fact]
    public void FromBool_Should_ReturnFailure_WhenFalse()
    {
        var exitCode = ExitCode.FromBool(false);
        Assert.Equal(ExitCode.Failure, exitCode);
    }
}