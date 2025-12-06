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

public static class FailureTypeExtensions
{
    extension(FailureType failureType)
    {
        public string ToMessage(Type? outputType)
        {
            return outputType is null 
                ? ToMessage(failureType) 
                : ToTypedMessage(failureType, outputType);
        }

        public string ToMessage<T>()
        {
            return ToTypedMessage(failureType, typeof(T));

        }
        
        public string ToMessage()
        {
            return failureType switch
            {
                FailureType.None  => "Result was successful",
                FailureType.Generic => "Result was a failure",
                FailureType.InvariantViolation => "An unexpected failure occured",
                FailureType.OperationCancelled => "The operation was cancelled",
                FailureType.OperationTimeout => "The operation timed out",
                FailureType.ConcurrencyViolation => "A Concurrency violation occured",
                FailureType.InvalidRequest => "Invalid request",
                FailureType.InvalidInput => "Invalid input",
                FailureType.DomainViolation => "The application's business logic was violated",
                FailureType.NotAllowed => "Operation not permitted",
                FailureType.NotFound => "Resource not found",
                FailureType.AlreadyExists => "Resource already exists",
                _ => "Unknown failure"
            };
        }

        private string ToTypedMessage(Type type)
        {
            var objectName = type.Name;
            return failureType switch
            {
                FailureType.None  => $"Result retrieving {objectName} was successful",
                FailureType.Generic => $"Failed to get {objectName}",
                FailureType.InvariantViolation => $"An unexpected failure occured when retrieving {objectName}",
                FailureType.OperationCancelled => $"The operation for {objectName} was cancelled",
                FailureType.OperationTimeout => $"The operation for {objectName} timed out",
                FailureType.ConcurrencyViolation => $"A Concurrency violation occured when retrieving {objectName}",
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
}