using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Convertibles.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

public static class ValueObjectResult
{
    public static ValueObjectResult<T> Pass<T>(T value)
    {
        return ValueObjectResult<T>.Pass(value);
    }
    
    public static ValueObjectResult<T> Fail<T>(string because = "")
    {
        return ValueObjectResult<T>.Fail(OperationStatus.Failure<T>(), because);
    }
    
    public static ValueObjectResult<T> DomainViolation<T>(string because = "")
    {
        return ValueObjectResult<T>.Fail(OperationStatus.DomainViolation<T>(), because);
    }
    
    public static ValueObjectResult<T> InvalidInput<T>(string because = "")
    {
        return ValueObjectResult<T>.Fail(OperationStatus.InvalidInput<T>(), because);
    }
    
    public static ValueObjectResult<T> Copy<T>(ValueObjectResult<T> result)
    {
        return ValueObjectResult<T>.Create(result);
    }
}


public class ValueObjectResult<T> : EntityConvertible<T>
{
    private ValueObjectResult(T value) 
        : base(value, ResultLayer.Unknown)
    {
    }
    
    private ValueObjectResult(FailedOperationStatus failedOperationStatus, string because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Unknown, because))
    {
    }
    
    private ValueObjectResult(IEntityConvertible<T> result) : base(result)
    {
    }
    
    private ValueObjectResult(IEntityConvertible result) : base(result)
    {
    }
    
    public ValueObjectResult<T2> ToTypedValueObjectResult<T2>()
    {
        return ValueObjectResult<T2>.Create(this);
    }

    internal static ValueObjectResult<T> Pass(T value)
    {
        return new ValueObjectResult<T>(value);
    }

    internal static ValueObjectResult<T> Fail(FailedOperationStatus failedOperationStatus, string because = "")
    {
        return new ValueObjectResult<T>(failedOperationStatus, because);
    }
    
    internal static ValueObjectResult<T> Create(IEntityConvertible<T> result)
    {
        return new ValueObjectResult<T>(result);
    }
    
    private static ValueObjectResult<T> Create(IEntityConvertible result)
    {
        return new ValueObjectResult<T>(result);
    }
    
    public static implicit operator ValueObjectResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator ValueObjectResult<T>(EntityConvertible result)
    {
        return new ValueObjectResult<T>(result);
    }
    
}