using Outputs.Base;
using Outputs.Base.Interfaces;

namespace ValueObjects.Results;

public class MapperResult : BasicResult<MapperResult>, IResultStatusBase<MapperResult>
{
    private const string BaseErrorMessage = "Failed to map result";

    private MapperResult(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private MapperResult(string baseMessage, string because) : base(baseMessage, because)
    {
    }

    private MapperResult(string successLog) : base(successLog)
    {
    }

    public static MapperResult Pass(string successLog = "")
    {
        return new MapperResult(successLog);
    }
    
    public static MapperResult<T> Pass<T>(T value, string successLog = "")
    {
        return MapperResult<T>.Pass(value, successLog);
    }

    public static MapperResult Fail(string because = "")
    {
        return new MapperResult(BaseErrorMessage, because);
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

public class MapperResult<T> :  ResultValue<T>
{
    private MapperResult(T value, string successLog) : base(value, successLog)
    {
    }

    private MapperResult(string because) : base($"Failed to map object to {typeof(T).Name}", because)
    {
    }

    internal static MapperResult<T> Pass(T value, string successLog = "")
    {
        return new MapperResult<T>(value, successLog);
    }

    internal static MapperResult<T> Fail(string because = "")
    {
        return new MapperResult<T>(because);
    }
}