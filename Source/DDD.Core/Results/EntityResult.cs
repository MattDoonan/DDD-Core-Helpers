using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Convertables;
using DDD.Core.Results.Convertables.Interfaces;
using DDD.Core.Results.Helpers;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

public class EntityResult : MapperConvertable, IResultFactory<EntityResult>
{
    private EntityResult(IMapperConvertable resultStatus) 
        : base(resultStatus)
    {
    }
    
    private EntityResult(FailureType failureType, string? because) 
        : base(failureType, ResultLayer.Unknown, because)
    {
    }

    private EntityResult() 
        : base(ResultLayer.Unknown)
    {
    }
    
    public EntityResult<T> ToTypedEntityResult<T>()
    {
        return EntityResult<T>.From(this);
    }
    
    public static EntityResult Pass()
    {
        return new EntityResult();
    }
    
    public static EntityResult Fail(string? because = null)
    {
        return new EntityResult(FailureType.Generic, because);
    }
    
    public static EntityResult Copy(EntityResult result)
    {
        return new EntityResult(result);
    }
    
    public static EntityResult DomainViolation(string? because = null)
    {
        return new EntityResult(FailureType.DomainViolation, because);
    }
    
    public static EntityResult InvalidInput(string? because = null)
    {
        return new EntityResult(FailureType.InvalidInput, because);
    }
    
    public static EntityResult NotFound(string? because = null)
    {
        return new EntityResult(FailureType.NotFound, because);
    }
    
    public static EntityResult InvariantViolation(string? because = null)
    {
        return new EntityResult(FailureType.InvariantViolation, because);
    }
    
    public static EntityResult Merge(params IMapperConvertable[] results)
    {
        return ResultCreationHelper.Merge<EntityResult, IMapperConvertable>(results);
    }
    
    public static EntityResult From(IMapperConvertable result)
    {
        return new EntityResult(result);
    }
    
    public static EntityResult<T> From<T>(IMapperConvertable<T> result)
    {
        return EntityResult<T>.From(result);
    }
    
    public static EntityResult<T> From<T>(IMapperConvertable result)
    {
        return EntityResult<T>.From(result);
    }
    
    public static EntityResult<T> Pass<T>(T value)
    {
        return EntityResult<T>.Pass(value);
    }
    
    public static EntityResult<T> Fail<T>(string? because = null)
    {
        return EntityResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static EntityResult<T> DomainViolation<T>(string? because = null)
    {
        return EntityResult<T>.Fail(FailureType.DomainViolation, because);
    }
    
    public static EntityResult<T> InvalidInput<T>(string? because = null)
    {
        return EntityResult<T>.Fail(FailureType.InvalidInput, because);
    }
    
    public static EntityResult<T> NotFound<T>(string? because = null)
    {
        return EntityResult<T>.Fail(FailureType.NotFound, because);
    }
    
    public static EntityResult<T> InvariantViolation<T>(string? because = null)
    {
        return EntityResult<T>.Fail(FailureType.InvariantViolation, because);
    }
    
    public static EntityResult<T> Copy<T>(EntityResult<T> result)
    {
        return EntityResult<T>.From(result);
    }
}

public class EntityResult<T> : MapperConvertable<T>
{
    private EntityResult(T value) 
        : base(value, ResultLayer.Unknown)
    {
    }

    private EntityResult(FailureType failureType, string? because) 
        : base(failureType, ResultLayer.Unknown, because)
    {
    }
    
    private EntityResult(IMapperConvertable<T> result) : base(result)
    {
    }
    
    private EntityResult(IMapperConvertable result) : base(result)
    {
    }
    
    public EntityResult RemoveType()
    {
        return EntityResult.From((IMapperConvertable)this);
    }
    
    public EntityResult<T2> ToTypedEntityResult<T2>()
    {
        return EntityResult<T2>.From(this);
    }
    
    internal static EntityResult<T> Pass(T value)
    {
        return new EntityResult<T>(value);
    }

    internal static EntityResult<T> Fail(FailureType failureType, string? because = null)
    {
        return new EntityResult<T>(failureType, because);
    }
    
    internal static EntityResult<T> From(IMapperConvertable<T> result)
    {
        return new EntityResult<T>(result);
    }
    
    internal static EntityResult<T> From(IMapperConvertable result)
    {
        return new EntityResult<T>(result);
    }
    
    public static implicit operator EntityResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator EntityResult(EntityResult<T> result)
    {
        return result.RemoveType();
    }
    
    public static implicit operator EntityResult<T>(MapperConvertable result)
    {
        return new EntityResult<T>(result);
    }
}