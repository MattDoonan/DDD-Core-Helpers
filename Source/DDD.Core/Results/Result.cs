using DDD.Core.Constants;
using DDD.Core.Results.Base;
using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Helpers;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

public class Result : ResultStatus, IResultFactory<Result>
{
    private Result(ResultLayer resultLayer = ResultLayer.Unknown) 
        : base(resultLayer)
    {
    }
    
    private Result(IResultStatus resultStatus, ResultLayer? newLayer = null) 
        : base(resultStatus, newLayer)
    {
    }
    
    private Result(FailureType failureType, ResultLayer failedLayer, string because) 
        : base(failureType, failedLayer, because)
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
        return new Result(FailureType.Generic, ResultLayer.Unknown, because);
    }

    public static Result Fail(FailureType failureType, string because)
    {
        return new Result(failureType, ResultLayer.Unknown, because);
    }
    
    public static Result Fail(ResultLayer failedLayer, string because)
    {
        return new Result(FailureType.Generic, failedLayer, because);
    }
    
    public static Result Fail(FailureType failureType, ResultLayer failedLayer, string because)
    {
        return new Result(failureType, failedLayer, because);
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
        return ResultCreationHelper.Merge<Result, IResultStatus>(results);
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
        return Result<T>.Fail(FailureType.Generic, ResultLayer.Unknown, because);
    }
    
    public static Result<T> Fail<T>(FailureType failureType, string because = "")
    {
        return Result<T>.Fail(failureType, ResultLayer.Unknown, because);
    }
    
    public static Result<T> Fail<T>(ResultLayer failedLayer, string because = "")
    {
        return Result<T>.Fail(FailureType.Generic, failedLayer, because);
    }
    
    public static Result<T> Fail<T>(FailureType failureType, ResultLayer failedLayer, string because = "")
    {
        return Result<T>.Fail(failureType, failedLayer, because);
    }
}

public class Result<T> : TypedResult<T>
{
    private Result(T value, ResultLayer resultLayer = ResultLayer.Unknown) 
        : base(value, resultLayer)
    {
    }
    
    private Result(FailureType failureType, ResultLayer failedLayer, string because) : base(failureType, failedLayer, because)
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
    
    internal static Result<T> Fail(FailureType failureType, ResultLayer failedLayer, string because = "")
    {
        return new Result<T>(failureType, failedLayer, because);
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
    
    public static implicit operator Result<T>(NonTypedResult result)
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