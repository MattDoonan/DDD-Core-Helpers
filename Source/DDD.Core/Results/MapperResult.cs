using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Convertibles.Interfaces;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

public class MapperResult : InfraConvertible, IResultFactory<MapperResult>
{
    private MapperResult(IResultStatus resultStatus) 
        : base(resultStatus)
    {
    }
    
    private MapperResult(FailureType failureType, string because) 
        : base(failureType, ResultLayer.Unknown, because)
    {
    }

    private MapperResult() 
        : base(ResultLayer.Unknown)
    {
    }
    
    public MapperResult<T> ToTypedMapperResult<T>()
    {
        return MapperResult<T>.From(this);
    }

    public static MapperResult Pass()
    {
        return new MapperResult();
    }
    
    public static MapperResult Fail(string because = "")
    {
        return new MapperResult(FailureType.Generic, because);
    }
    
    public static MapperResult DomainViolation(string because = "")
    {
        return new MapperResult(FailureType.DomainViolation, because);
    }
    
    public static MapperResult InvariantViolation(string because = "")
    {
        return new MapperResult(FailureType.InvariantViolation, because);
    }
    
    public static MapperResult InvalidInput(string because = "")
    {
        return new MapperResult(FailureType.InvalidInput, because);
    }
    
    public static MapperResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<MapperResult>();
    }
    
    public static MapperResult Copy(MapperResult result)
    {
        return From(result);
    }
    
    public static MapperResult From(IResultStatus status)
    {
        return new MapperResult(status);
    }
    
    public static MapperResult<T> From<T>(ITypedResult<T> result)
    {
        return MapperResult<T>.From(result);
    }
    
    public static MapperResult<T> From<T>(IResultStatus result)
    {
        return MapperResult<T>.From(result);
    }
    
    public static MapperResult<T> Pass<T>(T value)
    {
        return MapperResult<T>.Pass(value);
    }
    
    public static MapperResult<T> Fail<T>(string because = "")
    {
        return MapperResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static MapperResult<T> DomainViolation<T>(string because = "")
    {
        return MapperResult<T>.Fail(FailureType.DomainViolation, because);
    }
    
    public static MapperResult<T> InvalidInput<T>(string because = "")
    {
        return MapperResult<T>.Fail(FailureType.InvalidInput, because);
    }
    
    public static MapperResult<T> InvariantViolation<T>(string because = "")
    {
        return MapperResult<T>.Fail(FailureType.InvariantViolation, because);
    }
    
    public static MapperResult<T> Copy<T>(MapperResult<T> result)
    {
        return MapperResult<T>.From(result);
    }
}

public class MapperResult<T> : InfraConvertible<T>
{
    private MapperResult(T value) : base(value, ResultLayer.Unknown)
    {
    }

    private MapperResult(FailureType failureType, string because) 
        : base(failureType, ResultLayer.Unknown, because)
    {
    }
    
    private MapperResult(ITypedResult<T> result) 
        : base(result)
    {
    }
    
    private MapperResult(IResultStatus result) 
        : base(result)
    {
    }
    
    public MapperResult RemoveType()
    {
        return MapperResult.From((IResultStatus)this);
    }
    
    public MapperResult<T2> ToTypedMapperResult<T2>()
    {
        return MapperResult<T2>.From(this);
    }

    internal static MapperResult<T> Pass(T value)
    {
        return new MapperResult<T>(value);
    }

    internal static MapperResult<T> Fail(FailureType failureType, string because = "")
    {
        return new MapperResult<T>(failureType, because);
    }
    
    internal static MapperResult<T> From(ITypedResult<T> result)
    {
        return new MapperResult<T>(result);
    }
    
    internal static MapperResult<T> From(IResultStatus result)
    {
        return new MapperResult<T>(result);
    }
    
    public static implicit operator MapperResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator MapperResult(MapperResult<T> result)
    {
        return result.RemoveType();
    }
    
    public static implicit operator MapperResult<T>(InfraConvertible result)
    {
        return new MapperResult<T>(result);
    }
}