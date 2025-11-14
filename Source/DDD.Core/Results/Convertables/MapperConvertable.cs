using DDD.Core.Results.Convertables.Interfaces;
using DDD.Core.Results.Enums;

namespace DDD.Core.Results.Convertables;

public abstract class MapperConvertable : InfraConvertable, IMapperConvertable
{
    protected MapperConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    protected MapperConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }

    protected MapperConvertable(IMapperConvertable result) : base(result)
    {
    }
    
    protected MapperConvertable()
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
    protected MapperConvertable(T value) : base(value)
    {
    }

    protected MapperConvertable(IMapperConvertable valueResult) : base(valueResult)
    {
    }

    protected MapperConvertable(IMapperConvertable<T> valueResult) : base(valueResult)
    {
    }

    protected MapperConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }

    protected MapperConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
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