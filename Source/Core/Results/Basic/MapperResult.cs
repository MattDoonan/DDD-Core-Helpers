using Core.Results.Advanced.Abstract;
using Core.Results.Advanced.Interfaces;
using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;
using Core.Results.Helpers;

namespace Core.Results.Basic;

public class MapperResult : InfraConvertable, IResultFactory<MapperResult>
{
    private MapperResult(IInfraConvertable resultStatus) : base(resultStatus)
    {
    }
    
    private MapperResult(FailureType failureType, string because) : base(failureType, because)
    {
    }

    private MapperResult()
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
    
    public static MapperResult Merge(params IInfraConvertable[] results)
    {
        return ResultCreationHelper.Merge<MapperResult, IInfraConvertable>(results);
    }
    
    public static MapperResult Copy(MapperResult result)
    {
        return From(result);
    }
    
    public static MapperResult From(IInfraConvertable status)
    {
        return new MapperResult(status);
    }
    
    public static MapperResult<T> From<T>(IInfraConvertable<T> result)
    {
        return MapperResult<T>.From(result);
    }
    
    public static MapperResult<T> From<T>(IInfraConvertable result)
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

public class MapperResult<T> : InfraConvertable<T>
{
    private MapperResult(T value) : base(value)
    {
    }

    private MapperResult(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    private MapperResult(IInfraConvertable<T> result) : base(result)
    {
    }
    
    private MapperResult(IInfraConvertable result) : base(result)
    {
    }
    
    public MapperResult RemoveType()
    {
        return MapperResult.From((IInfraConvertable)this);
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
    
    internal static MapperResult<T> From(IInfraConvertable<T> result)
    {
        return new MapperResult<T>(result);
    }
    
    internal static MapperResult<T> From(IInfraConvertable result)
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
    
    public static implicit operator MapperResult<T>(InfraConvertable result)
    {
        return new MapperResult<T>(result);
    }
}