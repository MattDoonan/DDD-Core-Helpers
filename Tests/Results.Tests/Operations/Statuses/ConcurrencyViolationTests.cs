using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Results.Tests.Operations.Statuses.TestStructures;

namespace Results.Tests.Operations.Statuses;

public class ConcurrencyViolationTests : FailureOperationStatusTextFixture<ConcurrencyViolation, ConcurrencyViolationException>
{
    protected override ConcurrencyViolation CreateStatus()
    {
        return OperationStatus.ConcurrencyViolation();
    }

    protected override ConcurrencyViolation CreateStatus<T>()
    {
        return OperationStatus.ConcurrencyViolation<T>();
    }

    protected override StatusType GetExpectedStatusType()
    {
        return StatusType.ConcurrencyViolation;
    }

    protected override string GetExpectedMessage()
    {
        return "The operation failed due to a concurrency violation";
    }

    protected override string GetExpectedMessage<T>()
    {
        return $"The operation retrieving {typeof(T).Name} failed due to a concurrency violation";
    }
}