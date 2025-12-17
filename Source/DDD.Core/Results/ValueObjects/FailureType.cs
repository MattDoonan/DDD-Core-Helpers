namespace DDD.Core.Results.ValueObjects;

public enum FailureType
{
    None,
    Generic,
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