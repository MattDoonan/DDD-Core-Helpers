using DDD.Core.Results.Convertables.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertables;

public abstract class MapperConvertable : InfraConvertable, IMapperConvertable
{
    protected MapperConvertable(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }

    protected MapperConvertable(IMapperConvertable result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected MapperConvertable(ResultLayer resultLayer) 
        : base(resultLayer)
    {
    }
    
    public MapperResult<T> ToTypedMapperResult<T>()
    {
        return MapperResult<T>.From(this);
    }
    
    public MapperResult ToMapperResult()
    {
        return MapperResult.From(this);
    }
    
    public static implicit operator MapperResult(MapperConvertable result)
    {
        return result.ToMapperResult();
    }
}

public abstract class MapperConvertable<T> : InfraConvertable<T>, IMapperConvertable<T>
{
    protected MapperConvertable(T value, ResultLayer resultLayer) 
        : base(value, resultLayer)
    {
    }

    protected MapperConvertable(IMapperConvertable valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected MapperConvertable(IMapperConvertable<T> valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected MapperConvertable(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }
    
    public MapperResult<T2> ToTypedMapperResult<T2>()
    {
        return MapperResult<T2>.From(this);
    }
    
    public MapperResult<T> ToTypedMapperResult()
    {
        return MapperResult<T>.From(this);
    }

    public MapperResult ToMapperResult()
    {
        return MapperResult.From((IInfraConvertable)this);
    }
    
    public static implicit operator MapperResult<T>(MapperConvertable<T> result)
    {
        return result.ToTypedMapperResult();
    }
    
    public static implicit operator MapperResult(MapperConvertable<T> result)
    {
        return result.ToMapperResult();
    }
}