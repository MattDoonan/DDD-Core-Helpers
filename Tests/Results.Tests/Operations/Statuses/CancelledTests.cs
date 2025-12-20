using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Results.Tests.Operations.Statuses.TestStructures;

namespace Results.Tests.Operations.Statuses;

public class CancelledTests : FailureOperationStatusTextFixture<Cancelled, OperationCancelledException>
{
    protected override Cancelled CreateStatus()
    {
        return OperationStatus.Cancelled();
    }

    protected override Cancelled CreateStatus<T>()
    {
        return OperationStatus.Cancelled<T>();
    }

    protected override StatusType GetExpectedStatusType()
    {
        return StatusType.OperationCancelled;
    }

    protected override string GetExpectedMessage()
    {
        return "The operation was cancelled";
    }

    protected override string GetExpectedMessage<T>()
    {
        return $"The operation to retrieve {typeof(T).Name} was cancelled";
    }
}