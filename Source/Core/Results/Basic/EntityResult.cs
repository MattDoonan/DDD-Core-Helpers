using Core.Entities.Regular;
using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;
using Core.Results.Basic.Abstract;
using Core.Results.Basic.Interfaces;
using Core.Results.Helpers;

namespace Core.Results.Basic;

public class EntityResult : MapperConvertable, IResultFactory<EntityResult>
{
    private EntityResult(IMapperConvertable resultStatus) : base(resultStatus)
    {
    }
    
    private EntityResult(FailureType failureType, string because) : base(failureType, because)
    {
    }

    private EntityResult()
    {
    }
    
    public static EntityResult Pass()
    {
        return new EntityResult();
    }
    
    public static EntityResult Fail(string because = "")
    {
        return new EntityResult(FailureType.Generic, because);
    }
    
    public static EntityResult Copy(EntityResult result)
    {
        return new EntityResult(result);
    }
    
    internal static EntityResult Create(IMapperConvertable result)
    {
        return new EntityResult(result);
    }
    
    public static EntityResult DomainViolation(string because = "")
    {
        return new EntityResult(FailureType.DomainViolation, because);
    }
    
    public static EntityResult InvalidInput(string because = "")
    {
        return new EntityResult(FailureType.InvalidInput, because);
    }
    
    public static EntityResult Merge(params MapperConvertable[] results)
    {
        return ResultCreationHelper.Merge<EntityResult, MapperConvertable>(results);
    }
    
    public static EntityResult<T> Pass<T>(T value)
        where T : IEntity
    {
        return EntityResult<T>.Pass(value);
    }
    
    public static EntityResult<IEnumerable<T>> Pass<T>(IEnumerable<T> value)
        where T : IEntity
    {
        return EntityResult<IEnumerable<T>>.Pass(value);
    }
    
    public static EntityResult<T> Fail<T>(string because = "")
        where T : IEntity
    {
        return EntityResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static EntityResult<T> DomainViolation<T>(string because = "")
        where T : IEntity
    {
        return EntityResult<T>.Fail(FailureType.DomainViolation, because);
    }
    
    public static EntityResult<T> InvalidInput<T>(string because = "")
        where T : IEntity
    {
        return EntityResult<T>.Fail(FailureType.InvalidInput, because);
    }
    
    public static EntityResult<T> Copy<T>(EntityResult<T> result)
        where T : IEntity
    {
        return EntityResult<T>.Create(result);
    }
}

public class EntityResult<T> : MapperConvertable<T>
{
    private EntityResult(T value) : base(value)
    {
    }

    private EntityResult(FailureType failureType, string because) : base(failureType, because)
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
        return EntityResult.Create(this);
    }
    
    internal static EntityResult<T> Pass(T value)
    {
        return new EntityResult<T>(value);
    }

    internal static EntityResult<T> Fail(FailureType failureType, string because = "")
    {
        return new EntityResult<T>(failureType, because);
    }
    
    internal static EntityResult<T> Create(IMapperConvertable<T> result)
    {
        return new EntityResult<T>(result);
    }
    
    internal static EntityResult<T> Create(IMapperConvertable result)
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