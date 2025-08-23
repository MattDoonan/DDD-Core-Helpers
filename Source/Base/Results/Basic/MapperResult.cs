using Outputs.ObjectTypes;
using Outputs.Results.Advanced;
using Outputs.Results.Base.Abstract;
using Outputs.Results.Base.Enums;
using Outputs.Results.Base.Interfaces;

namespace Outputs.Results.Basic;

public class MapperResult : CoreResult<MapperResult>, IResultFactory<MapperResult>
{
    private MapperResult(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private MapperResult(FailureType failureType, string because) : base(failureType, because)
    {
    }

    private MapperResult()
    {
    }

    public static MapperResult Pass()
    {
        return new MapperResult();
    }
    
    public static MapperResult<T> Pass<T>(T value)
    {
        return MapperResult<T>.Pass(value);
    }

    public static MapperResult Fail(string because = "")
    {
        return new MapperResult(FailureType.Mapper, because);
    }
    
    public static MapperResult<T> Fail<T>(string because = "")
    {
        return MapperResult<T>.Fail(because);
    }

    public static MapperResult RemoveValue(IResultStatus status)
    {
        return new MapperResult(status);
    }
}

public class MapperResult<T> : CoreResult<T, MapperResult>
{
    private MapperResult(T value) : base(value)
    {
    }

    private MapperResult(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    private MapperResult(ITypedResult<T> result) : base(result)
    {
    }

    internal static MapperResult<T> Pass(T value)
    {
        return new MapperResult<T>(value);
    }

    internal static MapperResult<T> Fail(string because = "")
    {
        return new MapperResult<T>(FailureType.Mapper, because);
    }
    
    internal static MapperResult<T> Create(ITypedResult<T> result)
    {
        return new MapperResult<T>(result);
    }
    
    public static implicit operator MapperResult<T>(T value)
    {
        return Pass(value);
    }
}

public static class MapperResultExtensions
{
    public static MapperResult<T> AsMapperResult<T>(this T value)
    {
        return value;
    }
    
    public static MapperResult<T> AsMapperResult<T>(this EntityResult<T> result)
        where T : IEntity
    {
        return result;
    }
    
    public static MapperResult<T> AsMapperResult<T>(this ValueObjectResult<T> result)
        where T : IValueObject
    {
        return result;
    }
}