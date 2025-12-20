using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Results.Tests.Operations.Statuses.TestStructures;

namespace Results.Tests.Operations.Statuses;

public class InvalidInputTests : FailureOperationStatusTextFixture<InvalidInput, InvalidInputException>
{
    protected override InvalidInput CreateStatus()
    {
        return OperationStatus.InvalidInput();
    }

    protected override InvalidInput CreateStatus<T>()
    {
        return OperationStatus.InvalidInput<T>();
    }

    protected override StatusType GetExpectedStatusType()
    {
        return StatusType.InvalidInput;
    }

    protected override string GetExpectedMessage()
    {
        return "The input is invalid";
    }

    protected override string GetExpectedMessage<T>()
    {
        return $"The input to retrieve {typeof(T).Name} is invalid";
    }
}