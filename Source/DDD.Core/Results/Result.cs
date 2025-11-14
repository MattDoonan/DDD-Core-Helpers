using DDD.Core.Constants;
using DDD.Core.Results.Base;
using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Enums;
using DDD.Core.Results.Helpers;

namespace DDD.Core.Results;

public class Result : NonTypedResult, IResultFactory<Result>
{
    private Result()
    {
    }
    
    private Result(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private Result(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
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
    
    public static Result Fail(string because = "")
    {
        return new Result(FailureType.Generic, FailedLayer.Unknown, because);
    }

    public static Result Fail(FailureType failureType, string because)
    {
        return new Result(failureType, FailedLayer.Unknown, because);
    }
    
    public static Result Fail(FailedLayer failedLayer, string because)
    {
        return new Result(FailureType.Generic, failedLayer, because);
    }
    
    public static Result Fail(FailureType failureType, FailedLayer failedLayer, string because)
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
    
    public static Result<T> Copy<T>(Result<T> result)
    {
        return Result<T>.From(result);
    }
    
    public static Result<T> Fail<T>(string because = "")
    {
        return Result<T>.Fail(FailureType.Generic, FailedLayer.Unknown, because);
    }
    
    public static Result<T> Fail<T>(FailureType failureType, string because = "")
    {
        return Result<T>.Fail(failureType, FailedLayer.Unknown, because);
    }
    
    public static Result<T> Fail<T>(FailedLayer failedLayer, string because = "")
    {
        return Result<T>.Fail(FailureType.Generic, failedLayer, because);
    }
    
    public static Result<T> Fail<T>(FailureType failureType, FailedLayer failedLayer, string because = "")
    {
        return Result<T>.Fail(failureType, failedLayer, because);
    }
}

public class Result<T> : TypedResult<T>
{
    private Result(T value) : base(value)
    {
    }
    
    private Result(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
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
    
    internal static Result<T> Pass(T value)
    {
        return new Result<T>(value);
    }
    
    internal static Result<T> Fail(FailureType failureType, FailedLayer failedLayer, string because = "")
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