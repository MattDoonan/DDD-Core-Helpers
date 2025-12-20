using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using Xunit;

namespace Results.Tests.Operations.Statuses.TestStructures;

public abstract class FailureOperationStatusTextFixture<TOperationStatus, TException> : OperationStatusTestFixture<TOperationStatus>
    where TOperationStatus : FailedOperationStatus
    where TException : OperationException
{

    [Fact]
    public void Throw_ShouldThrowExpectedException()
    {
        var status = CreateStatus();

        var exception = Assert.Throws<TException>(() => status.Throw());
        Assert.Equal(status, exception.ToStatus());
    }

    [Fact]
    public void ToException_ShouldReturnExpectedException()
    {
        var status = CreateStatus();

        var exception = status.ToException();

        Assert.IsType<TException>(exception);
        Assert.Equal(status, exception.ToStatus());
    }
    
    
}