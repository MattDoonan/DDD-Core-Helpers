using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Results.Tests.Operations.Statuses.TestStructures;

namespace Results.Tests.Operations.Statuses;

public class NotFoundTests : FailureOperationStatusTextFixture<NotFound, NotFoundException>
{
    protected override NotFound CreateStatus()
    {
        return OperationStatus.NotFound();
    }

    protected override NotFound CreateStatus<T>()
    {
        return OperationStatus.NotFound<T>();
    }

    protected override StatusType GetExpectedStatusType()
    {
        return StatusType.NotFound;
    }

    protected override string GetExpectedMessage()
    {
       return "Requested resource was not found";
    }

    protected override string GetExpectedMessage<T>()
    {
        return $"Requested resource of type {typeof(T).Name} was not found";
    }
}