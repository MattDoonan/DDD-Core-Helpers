using DDD.Core.Results.Convertibles.Interfaces;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertibles;

public abstract class MapperConvertible : InfraConvertible, IMapperConvertible
{
    protected MapperConvertible(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }

    protected MapperConvertible(IResultStatus result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected MapperConvertible(ResultLayer resultLayer) 
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
    
    public static implicit operator MapperResult(MapperConvertible result)
    {
        return result.ToMapperResult();
    }
}

public abstract class MapperConvertible<T> : InfraConvertible<T>, IMapperConvertible<T>
{
    protected MapperConvertible(T value, ResultLayer resultLayer) 
        : base(value, resultLayer)
    {
    }

    protected MapperConvertible(IResultStatus valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected MapperConvertible(ITypedResult<T> valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected MapperConvertible(FailureType failureType, ResultLayer failedLayer, string? because) 
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
        return MapperResult.From((IInfraConvertible)this);
    }
    
    public static implicit operator MapperResult<T>(MapperConvertible<T> result)
    {
        return result.ToTypedMapperResult();
    }
    
    public static implicit operator MapperResult(MapperConvertible<T> result)
    {
        return result.ToMapperResult();
    }
}