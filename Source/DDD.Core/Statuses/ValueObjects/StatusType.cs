namespace DDD.Core.Statuses.ValueObjects;

public enum StatusType
{
    Success,
    GenericFailure,
    InvariantViolation,
    OperationCancelled,
    OperationTimeout,
    ConcurrencyViolation,
    InvalidRequest,
    InvalidInput,
    DomainViolation,
    NotAllowed,
    NotFound,
    AlreadyExists
}