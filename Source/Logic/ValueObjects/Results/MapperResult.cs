
using Outputs.Results;
using Outputs.Results.Abstract;
using Outputs.Results.Interfaces;

namespace ValueObjects.Results;

public class MapperResult : BasicResult<MapperResult>, IResultStatusBase<MapperResult>
{
    private const string BaseErrorMessage = "Failed to map result";

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

public class MapperResult<T> :  BasicValueResult<T, MapperResult>
{
    private MapperResult(T value) : base(value)
    {
    }

    private MapperResult(FailureType failureType, string because) : base(failureType, because)
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
}