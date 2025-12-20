using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Results.Tests.Operations.Statuses.TestStructures;

namespace Results.Tests.Operations.Statuses;

public class NotAllowedTests : FailureOperationStatusTextFixture<NotAllowed, NotAllowedException>
{
    protected override NotAllowed CreateStatus()
    {
        return OperationStatus.NotAllowed();
    }

    protected override NotAllowed CreateStatus<T>()
    {
        return OperationStatus.NotAllowed<T>();
    }

    protected override StatusType GetExpectedStatusType()
    {
        return StatusType.NotAllowed;
    }

    protected override string GetExpectedMessage()
    {
        return "The operation is not permitted";
    }

    protected override string GetExpectedMessage<T>()
    {
        return $"The operation to get {typeof(T).Name} is not permitted";
    }
}