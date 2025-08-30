using Core.Results.Advanced.Abstract;
using Core.Results.Base.Enums;
using Core.Results.Basic.Interfaces;

namespace Core.Results.Basic.Abstract;

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
        return MapperResult<T>.Create(this);
    }
    
    public MapperResult ToMapperResult()
    {
        return MapperResult.Create(this);
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
        return MapperResult<T2>.Create(this);
    }
    
    public MapperResult<T> ToTypedMapperResult()
    {
        return MapperResult<T>.Create(this);
    }

    public MapperResult ToMapperResult()
    {
        return MapperResult.Create(this);
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