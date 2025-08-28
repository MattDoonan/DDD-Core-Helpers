namespace Core.Results.Base.Enums;

public enum FailureType
{
    None,
    Generic,
    OperationTimeout,
    InvalidRequest,
    InvalidInput,
    DomainViolation,
    NotAllowed,
    NotFound,
    AlreadyExists
}

public static class FailureTypeExtensions
{
    
    public static string ToMessage(this FailureType failureType)
    {
        return failureType switch
        {
            FailureType.None  => "Result was successful",
            FailureType.Generic => "Result was a failure",
            FailureType.OperationTimeout => "Operation timed out",
            FailureType.InvalidRequest => "Invalid request",
            FailureType.InvalidInput => "Invalid input",
            FailureType.DomainViolation => "The application's business logic was violated",
            FailureType.NotAllowed => "Operation not permitted",
            FailureType.NotFound => "Resource not found",
            FailureType.AlreadyExists => "Resource already exists",
            _ => "Unknown failure"
        };
    }
    
    public static string ToMessage<T>(this FailureType failureType)
    {
        var objectName = typeof(T).Name;
        return failureType switch
        {
            FailureType.None  => $"Result retrieving {objectName} was successful",
            FailureType.Generic => $"Failed to get {objectName}",
            FailureType.OperationTimeout => $"Operation for {objectName} timed out",
            FailureType.InvalidRequest => $"Invalid request to retrieve {objectName}",
            FailureType.InvalidInput => $"Invalid input trying to retrieve {objectName}",
            FailureType.DomainViolation => $"The application's business logic was violated trying to retrieve {objectName}",
            FailureType.NotAllowed => $"Operation to get {objectName} is not permitted",
            FailureType.NotFound => $"{objectName} not found",
            FailureType.AlreadyExists => $"{objectName} resource already exists",
            _ => $"Unknown failure to retrieve {objectName}"
        };
    }
}