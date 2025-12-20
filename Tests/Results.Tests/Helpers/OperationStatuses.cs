using DDD.Core.Operations.Statuses.Abstract;

namespace Results.Tests.Helpers;

internal class OperationStatuses
{
    public static IEnumerable<FailedOperationStatus> GetAllFailures()
    {
        yield return OperationStatus.AlreadyExists();
        yield return OperationStatus.Cancelled();
        yield return OperationStatus.ConcurrencyViolation();
        yield return OperationStatus.DomainViolation();
        yield return OperationStatus.Failure();
        yield return OperationStatus.InvalidInput();
        yield return OperationStatus.InvalidRequest();
        yield return OperationStatus.InvariantViolation();
        yield return OperationStatus.NotAllowed();
        yield return OperationStatus.NotFound();
        yield return OperationStatus.TimedOut();
        yield return OperationStatus.AlreadyExists<string>();
        yield return OperationStatus.Cancelled<int>();
        yield return OperationStatus.ConcurrencyViolation<long>();
        yield return OperationStatus.DomainViolation<string>();
        yield return OperationStatus.Failure<object>();
        yield return OperationStatus.InvalidInput<string>();
        yield return OperationStatus.InvalidRequest<int?>();
        yield return OperationStatus.InvariantViolation<string>();
        yield return OperationStatus.NotAllowed<byte>();
        yield return OperationStatus.NotFound<short>();
        yield return OperationStatus.TimedOut<string?>();
    }
}