using Core.Results.Base.Enums;
using Core.Results.Basic.Interfaces;

namespace Core.Results.Basic.Abstract;

public abstract class EntityConvertable : MapperConvertable, IEntityConvertable
{
    protected EntityConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    protected EntityConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }

    protected EntityConvertable(IMapperConvertable result) : base(result)
    {
    }
    
    protected EntityConvertable()
    {
    }

    public EntityResult ToEntityResult()
    {
        return EntityResult.Create(this);
    }

    public static implicit operator EntityResult(EntityConvertable result)
    {
        return result.ToEntityResult();
    }
}

public abstract class EntityConvertable<T> : MapperConvertable<T>, IEntityConvertable<T>
{
    protected EntityConvertable(T value) : base(value)
    {
    }

    protected EntityConvertable(IMapperConvertable valueResult) : base(valueResult)
    {
    }

    protected EntityConvertable(IMapperConvertable<T> valueResult) : base(valueResult)
    {
    }

    protected EntityConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }

    protected EntityConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }
    
    public EntityResult<T> ToTypedEntityResult()
    {
        return EntityResult<T>.Create(this);
    }

    public EntityResult ToEntityResult()
    {
        return EntityResult.Create(this);
    }
    
    
    public static implicit operator EntityResult<T>(EntityConvertable<T> result)
    {
        return result.ToTypedEntityResult();
    }
    
    public static implicit operator EntityResult(EntityConvertable<T> result)
    {
        return result.ToEntityResult();
    }
}