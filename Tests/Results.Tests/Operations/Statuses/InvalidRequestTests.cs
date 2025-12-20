using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Results.Tests.Operations.Statuses.TestStructures;

namespace Results.Tests.Operations.Statuses;

public class InvalidRequestTests : FailureOperationStatusTextFixture<InvalidRequest, InvalidRequestException>
{
    protected override InvalidRequest CreateStatus()
    {
        return OperationStatus.InvalidRequest();
    }

    protected override InvalidRequest CreateStatus<T>()
    {
        return OperationStatus.InvalidRequest<T>();
    }

    protected override StatusType GetExpectedStatusType()
    {
        return StatusType.InvalidRequest;
    }

    protected override string GetExpectedMessage()
    {
        return "The request is invalid";
    }

    protected override string GetExpectedMessage<T>()
    {
        return $"The request for {typeof(T).Name} is invalid";
    }
}