namespace Outputs.Base;

public enum FailureType
{
    ValueObject,
    Mapper,
    Entity,
    NotFound,
    Timout,
    InvalidCall,
    AlreadyExists
}

public static class FailureTypeExtensions
{
    public static string ToMessage(this FailureType failureType)
    {
        return failureType switch
        {
            FailureType.ValueObject => "Value object validation failed",
            FailureType.Mapper => "Mapping failed between layers",
            FailureType.Entity => "Entity processing failed",
            FailureType.NotFound => "Resource not found",
            FailureType.Timout => "Operation timed out",
            FailureType.InvalidCall => "Invalid method or API call",
            FailureType.AlreadyExists => "Resource already exists",
            _ => "Unknown failure"
        };
    }
    
}