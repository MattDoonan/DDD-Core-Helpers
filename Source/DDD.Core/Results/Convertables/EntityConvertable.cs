using DDD.Core.Results.Convertables.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertables;

public abstract class EntityConvertable : MapperConvertable, IEntityConvertable
{
    protected EntityConvertable(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }

    protected EntityConvertable(IMapperConvertable result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected EntityConvertable(ResultLayer resultLayer) 
        : base(resultLayer)
    {
    }
    
    public EntityResult<T> ToTypedEntityResult<T>()
    {
        return EntityResult<T>.From(this);
    }

    public EntityResult ToEntityResult()
    {
        return EntityResult.From(this);
    }

    public static implicit operator EntityResult(EntityConvertable result)
    {
        return result.ToEntityResult();
    }
}

public abstract class EntityConvertable<T> : MapperConvertable<T>, IEntityConvertable<T>
{
    protected EntityConvertable(T value, ResultLayer resultLayer) : base(value, resultLayer)
    {
    }

    protected EntityConvertable(IMapperConvertable valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected EntityConvertable(IMapperConvertable<T> valueResult, ResultLayer? newResultLayer = null)
        : base(valueResult, newResultLayer)
    {
    }

    protected EntityConvertable(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }
    
    public EntityResult<T2> ToTypedEntityResult<T2>()
    {
        return EntityResult<T2>.From(this);
    }
    
    public EntityResult<T> ToTypedEntityResult()
    {
        return EntityResult<T>.From(this);
    }

    public EntityResult ToEntityResult()
    {
        return EntityResult.From((IMapperConvertable)this);
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