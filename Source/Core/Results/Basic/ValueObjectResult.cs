using Core.Results.Base.Enums;
using Core.Results.Basic.Abstract;
using Core.Results.Basic.Interfaces;
using Core.ValueObjects.Regular.Base;

namespace Core.Results.Basic;

public static class ValueObjectResult
{
    public static ValueObjectResult<T> Pass<T>(T value)
        where T : class, IValueObject
    {
        return ValueObjectResult<T>.Pass(value);
    }
    
    public static ValueObjectResult<T> Fail<T>(string because = "")
        where T : class, IValueObject
    {
        return ValueObjectResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static ValueObjectResult<T> DomainViolation<T>(string because = "")
        where T : class, IValueObject
    {
        return ValueObjectResult<T>.Fail(FailureType.DomainViolation, because);
    }
    
    public static ValueObjectResult<T> InvalidInput<T>(string because = "")
        where T : class, IValueObject
    {
        return ValueObjectResult<T>.Fail(FailureType.InvalidInput, because);
    }
    
    public static ValueObjectResult<T> Copy<T>(ValueObjectResult<T> result)
        where T : class, IValueObject
    {
        return ValueObjectResult<T>.Create(result);
    }
}


public class ValueObjectResult<T> : EntityConvertable<T>
{
    private ValueObjectResult(T value) : base(value)
    {
    }
    
    private ValueObjectResult(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    private ValueObjectResult(IEntityConvertable<T> result) : base(result)
    {
    }
    
    private ValueObjectResult(IEntityConvertable result) : base(result)
    {
    }

    internal static ValueObjectResult<T> Pass(T value)
    {
        return new ValueObjectResult<T>(value);
    }

    internal static ValueObjectResult<T> Fail(FailureType failureType, string because = "")
    {
        return new ValueObjectResult<T>(failureType, because);
    }
    
    internal static ValueObjectResult<T> Create(IEntityConvertable<T> result)
    {
        return new ValueObjectResult<T>(result);
    }
    
    public static implicit operator ValueObjectResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator ValueObjectResult<T>(EntityConvertable result)
    {
        return new ValueObjectResult<T>(result);
    }
    
}