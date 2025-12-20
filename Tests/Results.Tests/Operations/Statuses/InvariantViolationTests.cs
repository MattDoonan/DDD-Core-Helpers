using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Results.Tests.Operations.Statuses.TestStructures;

namespace Results.Tests.Operations.Statuses;

public class InvariantViolationTests : FailureOperationStatusTextFixture<InvariantViolation, InvariantViolationException>
{
    protected override InvariantViolation CreateStatus()
    {
        return OperationStatus.InvariantViolation();
    }

    protected override InvariantViolation CreateStatus<T>()
    {
        return OperationStatus.InvariantViolation<T>();
    }

    protected override StatusType GetExpectedStatusType()
    {
        return StatusType.InvariantViolation;
    }

    protected override string GetExpectedMessage()
    {
        return "An invariant condition was violated";
    }

    protected override string GetExpectedMessage<T>()
    {
        return $"An invariant condition was violated when retrieving {typeof(T).Name}";
    }
}