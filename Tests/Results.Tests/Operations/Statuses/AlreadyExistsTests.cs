using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Results.Tests.Operations.Statuses.TestStructures;

namespace Results.Tests.Operations.Statuses;

public class AlreadyExistsTests : FailureOperationStatusTextFixture<AlreadyExists, AlreadyExistsException>
{
    protected override AlreadyExists CreateStatus()
    {
        return OperationStatus.AlreadyExists();
    }

    protected override AlreadyExists CreateStatus<T>()
    {
        return OperationStatus.AlreadyExists<T>();
    }

    protected override StatusType GetExpectedStatusType()
    {
        return StatusType.AlreadyExists;
    }

    protected override string GetExpectedMessage()
    {
        return "Resource already exists";
    }

    protected override string GetExpectedMessage<T>()
    {
        return $"Resource of type {typeof(T).Name} already exists";
    }
}