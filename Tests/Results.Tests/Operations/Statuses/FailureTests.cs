using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Results.Tests.Operations.Statuses.TestStructures;

namespace Results.Tests.Operations.Statuses;

public class FailureTests : FailureOperationStatusTextFixture<Failure, OperationException>
{
    protected override Failure CreateStatus()
    {
        return OperationStatus.Failure();
    }

    protected override Failure CreateStatus<T>()
    {
        return OperationStatus.Failure<T>();
    }

    protected override StatusType GetExpectedStatusType()
    {
        return StatusType.GenericFailure;
    }

    protected override string GetExpectedMessage()
    {
        return "The operation was a failure";
    }

    protected override string GetExpectedMessage<T>()
    {
        return $"The operation to retrieve {typeof(T).Name} was a failure";
    }
}