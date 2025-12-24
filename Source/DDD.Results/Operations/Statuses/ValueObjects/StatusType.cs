namespace DDD.Core.Operations.Statuses.ValueObjects;

/// <summary>
/// Represents the various types of operation statuses.
/// </summary>
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