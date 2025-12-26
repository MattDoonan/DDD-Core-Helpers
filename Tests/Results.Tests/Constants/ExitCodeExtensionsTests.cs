using DDD.Core.Constants;
using DDD.Core.Results;
using DDD.Core.Results.Extensions;
using Xunit;

namespace Results.Tests.Constants;

public class ExitCodeExtensionsTests
{
    [Fact]
    public async Task ToExitCodeAsync_WithSuccessfulResult_Should_Return1()
    {
        var resultTask = Task.FromResult(Result.Pass());
        var exitCode = await resultTask.ToExitCodeAsync();
        Assert.Equal(ExitCode.Success, exitCode);
    }

    [Fact]
    public async Task ToExitCodeAsync_WithFailedResult_Should_Return0()
    {
        var resultTask = Task.FromResult(Result.Fail("Error"));
        var exitCode = await resultTask.ToExitCodeAsync();
        Assert.Equal(ExitCode.Failure, exitCode);
    }
    
    [Fact]
    public async Task ToExitCodeAsync_WithSuccessfulTypedResult_Should_Return1()
    {
        var resultTask = Task.FromResult(Result.Pass("Success"));
        var exitCode = await resultTask.ToExitCodeAsync();
        Assert.Equal(ExitCode.Success, exitCode);
    }

    [Fact]
    public async Task ToExitCodeAsync_WithFailedTypedResult_Should_Return0()
    {
        var resultTask = Task.FromResult(Result.Fail<string>("Error"));
        var exitCode = await resultTask.ToExitCodeAsync();
        Assert.Equal(ExitCode.Failure, exitCode);
    }
}