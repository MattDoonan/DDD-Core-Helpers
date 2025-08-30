using Core.Results.Base.Abstract;
using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;

namespace Core.Results.Advanced;

public class Result : CoreResult, IResultFactory<Result>
{
    private Result()
    {
    }
    
    private Result(ResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private Result(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
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
    
    internal static Result Create(ResultStatus result)
    {
        return new Result(result);
    }
    
    public static Result<T> Pass<T>(T value)
    {
        return Result<T>.Pass(value);
    }
    
    public static Result<T> Copy<T>(Result<T> result)
    {
        return Result<T>.Create(result);
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
    
    public Result RemoveType()
    {
        return Result.Create(this);
    }
    
    internal static Result<T> Pass(T value)
    {
        return new Result<T>(value);
    }
    
    internal static Result<T> Fail(FailureType failureType, FailedLayer failedLayer, string because = "")
    {
        return new Result<T>(failureType, failedLayer, because);
    }
    
    internal static Result<T> Create(ITypedResult<T> result)
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
    
    public static implicit operator Result<T>(CoreResult result)
    {
        return new Result<T>(result);
    }
}