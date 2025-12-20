using DDD.Core.Constants;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Abstract;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

public class Result : UntypedResult, IResultFactory<Result>
{
    private Result(ResultLayer resultLayer = ResultLayer.Unknown) 
        : base(resultLayer)
    {
    }
    
    private Result(IResultStatus resultStatus, ResultLayer? newLayer = null) 
        : base(resultStatus, newLayer)
    {
    }
    
    private Result(FailedOperationStatus failedOperationStatus, ResultLayer failedLayer, string because) 
        : base(new ResultError(failedOperationStatus, failedLayer, because))
    {
    }

    public int ToExitCode()
    {
        return ExitCode.FromBool(IsSuccessful);
    }
    
    public Result<T> ToTypedResult<T>()
    {
        return Result<T>.From(this);
    }

    public static Result Pass()
    {
        return new Result();
    }
    
    public static Result Pass(ResultLayer resultLayer)
    {
        return new Result(resultLayer);
    }
    
    public static Result Fail(string because = "")
    {
        return new Result(OperationStatus.Failure(), ResultLayer.Unknown, because);
    }

    public static Result Fail(FailedOperationStatus failedStatus, string because)
    {
        return new Result(failedStatus, ResultLayer.Unknown, because);
    }
    
    public static Result Fail(ResultLayer failedLayer, string because)
    {
        return new Result(OperationStatus.Failure(), failedLayer, because);
    }
    
    public static Result Fail(FailedOperationStatus failedStatus, ResultLayer failedLayer, string because)
    {
        return new Result(failedStatus, failedLayer, because);
    }
    
    public static Result Copy(Result result)
    {
        return new Result(result);
    }
    
    public static Result From(IResultStatus result)
    {
        return new Result(result);
    }
    
    public static Result From(IResultStatus result, ResultLayer newResultLayer)
    {
        return new Result(result, newResultLayer);
    }
    
    public static Result<T> From<T>(ITypedResult<T> result)
    {
        return Result<T>.From(result);
    }
    
    public static Result<T> From<T>(IResultStatus result)
    {
        return Result<T>.From(result);
    }
    
    public static Result Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<Result>();
    }
    
    public static Result<T> Pass<T>(T value)
    {
        return Result<T>.Pass(value);
    }
    
    public static Result<T> Pass<T>(T value, ResultLayer resultLayer)
    {
        return Result<T>.Pass(value, resultLayer);
    }
    
    public static Result<T> Copy<T>(Result<T> result)
    {
        return Result<T>.From(result);
    }
    
    public static Result<T> Fail<T>(string because = "")
    {
        return Result<T>.Fail(OperationStatus.Failure<T>(), ResultLayer.Unknown, because);
    }
    
    public static Result<T> Fail<T>(FailedOperationStatus failedStatus, string because = "")
    {
        return Result<T>.Fail(failedStatus, ResultLayer.Unknown, because);
    }
    
    public static Result<T> Fail<T>(ResultLayer failedLayer, string because = "")
    {
        return Result<T>.Fail(OperationStatus.Failure<T>(), failedLayer, because);
    }
    
    public static Result<T> Fail<T>(FailedOperationStatus failedStatus, ResultLayer failedLayer, string because = "")
    {
        return Result<T>.Fail(failedStatus, failedLayer, because);
    }
}

public class Result<T> : TypedResult<T>
{
    private Result(T value, ResultLayer resultLayer = ResultLayer.Unknown) 
        : base(value, resultLayer)
    {
    }
    
    private Result(FailedOperationStatus failedOperationStatus, ResultLayer failedLayer, string because) 
        : base(new ResultError(failedOperationStatus, failedLayer, because))
    {
    }
    
    private Result(ITypedResult<T> result) : base(result)
    {
    }
    
    private Result(IResultStatus result) : base(result)
    {
    }
    
    public int ToExitCode()
    {
        return ExitCode.FromBool(IsSuccessful);
    }
    
    public Result RemoveType()
    {
        return Result.From((IResultStatus)this);
    }
    
    public Result<T2> ToTypedResult<T2>()
    {
        return Result<T2>.From(this);
    }
    
    internal static Result<T> Pass(T value, ResultLayer resultLayer = ResultLayer.Unknown)
    {
        return new Result<T>(value, resultLayer);
    }
    
    internal static Result<T> Fail(FailedOperationStatus failedOperationStatus, ResultLayer failedLayer, string because = "")
    {
        return new Result<T>(failedOperationStatus, failedLayer, because);
    }
    
    internal static Result<T> From(ITypedResult<T> result)
    {
        return new Result<T>(result);
    }
    
    internal static Result<T> From(IResultStatus result)
    {
        return new Result<T>(result);
    }
    
    public static implicit operator Result<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator Result(Result<T> result)
    {
        return result.RemoveType();
    }
    
    public static implicit operator Result<T>(UntypedResult result)
    {
        return new Result<T>(result);
    }
}

public static class ResultExtensions
{
    public static async Task<int> ToExitCodeAsync(this Task<Result> resultTask)
    {
        return (await resultTask).ToExitCode();
    }
    
    public static async Task<int> ToExitCodeAsync<T>(this Task<Result<T>> resultTask)
    {
        return (await resultTask).ToExitCode();
    }
}