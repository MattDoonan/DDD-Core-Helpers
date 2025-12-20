using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Results.Tests.Operations.Statuses.TestStructures;

namespace Results.Tests.Operations.Statuses;

public class DomainViolationTests : FailureOperationStatusTextFixture<DomainViolation, DomainViolationException>
{
    protected override DomainViolation CreateStatus()
    {
        return OperationStatus.DomainViolation();
    }

    protected override DomainViolation CreateStatus<T>()
    {
        return OperationStatus.DomainViolation<T>();
    }

    protected override StatusType GetExpectedStatusType()
    {
        return StatusType.DomainViolation;
    }

    protected override string GetExpectedMessage()
    { 
        return "A domain violation has occurred";
    }

    protected override string GetExpectedMessage<T>()
    {
        return $"A domain violation has occurred while retrieving {typeof(T).Name}";
    }
}