using Outputs.Base;
using Outputs.Base.Interfaces;

namespace ValueObjects.Results;

public class MapperResult :  ResultStatus, IResultStatusBase<MapperResult>
{
    private const string BaseErrorMessage = "Failed to map result";
    
    public MapperResult(string baseMessage, string because) : base(baseMessage, because)
    {
    }

    public MapperResult(string successLog) : base(successLog)
    {
    }

    public static MapperResult Pass(string successLog = "")
    {
        return new MapperResult(successLog);
    }

    public static MapperResult Fail(string because = "")
    {
        return new MapperResult(BaseErrorMessage, because);
    }

    public static MapperResult AllPass(params MapperResult[] result)
    {
        var succeeded = AllSucceeded(result.Cast<IResultStatus>().ToArray());
        return succeeded
            ? new MapperResult("All mapper results were successful")
            : new MapperResult("Error checking mapper results", "not all map results were successful");
    }

    public static MapperResult RemoveValue(IResultStatus status)
    {
        return status.Failed 
            ? new MapperResult(BaseErrorMessage, status.ErrorReason)
            : new MapperResult(status.SuccessLog); 
    }
}

public class MapperResult<T> :  ResultValue<T>, IResultValueBase<T, MapperResult<T>>
{
    private MapperResult(T value, string successLog) : base(value, successLog)
    {
    }

    private MapperResult(string because) : base($"Failed to map object to {typeof(T).Name}", because)
    {
    }

    public static MapperResult<T> Pass(T value, string successLog = "")
    {
        return new MapperResult<T>(value, successLog);
    }

    public static MapperResult<T> Fail(string because = "")
    {
        return new MapperResult<T>(because);
    }
}