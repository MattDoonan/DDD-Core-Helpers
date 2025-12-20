using DDD.Core.Results.Convertibles.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertibles;

public abstract class EntityConvertible : MapperConvertible, IEntityConvertible
{
    protected EntityConvertible(ResultError error) 
        : base(error)
    {
    }

    protected EntityConvertible(IMapperConvertible result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected EntityConvertible(ResultLayer resultLayer) 
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

    public static implicit operator EntityResult(EntityConvertible result)
    {
        return result.ToEntityResult();
    }
}

public abstract class EntityConvertible<T> : MapperConvertible<T>, IEntityConvertible<T>
{
    protected EntityConvertible(T value, ResultLayer resultLayer) : base(value, resultLayer)
    {
    }

    protected EntityConvertible(IMapperConvertible valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected EntityConvertible(IMapperConvertible<T> valueResult, ResultLayer? newResultLayer = null)
        : base(valueResult, newResultLayer)
    {
    }

    protected EntityConvertible(ResultError error) 
        : base(error)
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
        return EntityResult.From((IMapperConvertible)this);
    }
    
    
    public static implicit operator EntityResult<T>(EntityConvertible<T> result)
    {
        return result.ToTypedEntityResult();
    }
    
    public static implicit operator EntityResult(EntityConvertible<T> result)
    {
        return result.ToEntityResult();
    }
}