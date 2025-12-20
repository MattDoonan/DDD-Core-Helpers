using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using Results.Tests.Operations.Statuses.TestStructures;

namespace Results.Tests.Operations.Statuses;

public class SuccessTests : OperationStatusTestFixture<Success>
{
    protected override Success CreateStatus()
    {
        return OperationStatus.Success();
    }

    protected override Success CreateStatus<T>()
    {
        return OperationStatus.Success<T>();
    }

    protected override StatusType GetExpectedStatusType()
    {
        return StatusType.Success;
    }

    protected override string GetExpectedMessage()
    {
        return "The operation completed successfully";
    }

    protected override string GetExpectedMessage<T>()
    {
        return $"The operation retrieving {typeof(T).Name} completed successfully";
    }
}