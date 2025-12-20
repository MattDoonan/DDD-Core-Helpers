using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Xunit;

namespace Results.Tests.Operations.Statuses.TestStructures;

public abstract class OperationStatusTestFixture<TOperationStatus>
    where TOperationStatus : OperationStatus
{
    
    [Fact]
    public void CreateOperationStatus_Should_SetExpectedStatusType_AndMessage()
    {
        var status = CreateStatus();
        var expectedType = GetExpectedStatusType();
        var expectedMessage = GetExpectedMessage();
        Assert.Equal(expectedType, status.Type);
        Assert.Equal(expectedMessage, status.Message);
    }

    [Fact]
    public void CreateOperationStatus_WithType_Should_SetExpectedStatusType_AndMessage()
    {
        var status = CreateStatus<object>();
        var expectedType = GetExpectedStatusType();
        var expectedMessage = GetExpectedMessage<object>();
        Assert.Equal(expectedType, status.Type);
        Assert.Equal(expectedMessage, status.Message);
    }
    
    protected abstract TOperationStatus CreateStatus();
    protected abstract TOperationStatus CreateStatus<T>();
    protected abstract StatusType GetExpectedStatusType();
    protected abstract string GetExpectedMessage();
    protected abstract string GetExpectedMessage<T>();
    
}