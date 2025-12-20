using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Results.Tests.Operations.Statuses.TestStructures;

namespace Results.Tests.Operations.Statuses;

public class TimedOutTests : FailureOperationStatusTextFixture<TimedOut, OperationTimeoutException>
{
    protected override TimedOut CreateStatus()
    {
        return OperationStatus.TimedOut();
    }

    protected override TimedOut CreateStatus<T>()
    {
        return OperationStatus.TimedOut<T>();
    }

    protected override StatusType GetExpectedStatusType()
    {
        return StatusType.OperationTimeout;
    }

    protected override string GetExpectedMessage()
    {
        return "The operation has timed out";
    }

    protected override string GetExpectedMessage<T>()
    {
        return $"The operation to retrieve {typeof(T).Name} has timed out";
    }
}