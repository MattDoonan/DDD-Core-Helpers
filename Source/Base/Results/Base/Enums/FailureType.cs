namespace Outputs.Results.Base.Enums;

public enum FailureType
{
    None,
    
    Generic,
    OperationTimout,
    InvalidRequest,
    
    ValueObject,
    Mapper,
    Entity,
    
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
            FailureType.OperationTimout => "Operation timed out",
            FailureType.InvalidRequest => "Invalid request",
            FailureType.ValueObject => "Failure to create Value Object",
            FailureType.Mapper => "Failure during Mapping operation",
            FailureType.Entity => "Failure during Entity operation",
            FailureType.NotFound => "Resource not found",
            FailureType.AlreadyExists => "Resource already exists",
            _ => "Unknown failure"
        };
    }
    
    public static string ToMessage<T>(this FailureType failureType)
    {
        return failureType switch
        {
            FailureType.None  => $"Result retrieving {typeof(T)} was successful",
            FailureType.Generic => $"Failed to get {typeof(T)}",
            FailureType.OperationTimout => $"Operation for {typeof(T)} timed out",
            FailureType.InvalidRequest => $"Invalid request to retrieve {typeof(T)}",
            FailureType.ValueObject => $"Failure to create {typeof(T)} Value Object",
            FailureType.Mapper => $"Failure during Mapping operation for {typeof(T)}",
            FailureType.Entity => $"Failure during Entity operation for {typeof(T)}",
            FailureType.NotFound => $"{typeof(T)} not found",
            FailureType.AlreadyExists => $"{typeof(T)} resource already exists",
            _ => $"Unknown failure to retrieve {typeof(T)}"
        };
    }
    
}