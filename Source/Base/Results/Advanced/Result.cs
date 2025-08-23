using Outputs.Results.Base.Abstract;
using Outputs.Results.Base.Enums;
using Outputs.Results.Base.Interfaces;

namespace Outputs.Results.Advanced;

public class Result : CoreResult<Result>, IResultFactory<Result>
{
    private Result(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private Result()
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
    
    public static Result Timeout(string because = "")
    {
        return Fail(FailureType.OperationTimout, because);
    }
    
    public static Result InvalidRequest(string because = "")
    {
        return Fail(FailureType.InvalidRequest, because);
    }

    public static Result RemoveValue(IResultStatus status)
    {
        return new Result(status);
    }
    
    public static Result Copy(Result result)
    {
        return new Result(result);
    }
    
    internal static Result Create(IResultStatus result)
    {
        return new Result(result);
    }
    
    public static Result<T> Pass<T>(T value)
    {
        return Result<T>.Pass(value);
    }
    
    public static Result<T> Fail<T>(FailureType failureType, string because = "")
    {
        return Result<T>.Fail(failureType, because);
    }
    
    public static Result<T> Copy<T>(Result<T> result)
    {
        return Result<T>.Create(result);
    }
    
    public static Result<T> NotFound<T>(string because = "")
    {
        return Result<T>.Fail(FailureType.NotFound, because);
    }
    
    public static Result<T> AlreadyExists<T>(string because = "")
    {
        return Result<T>.Fail(FailureType.AlreadyExists, because);
    }
    
    public static Result<T> Timeout<T>(string because = "")
    {
        return Result<T>.Fail(FailureType.OperationTimout, because);
    }
    
    public static Result<T> InvalidRequest<T>(string because = "")
    {
        return Result<T>.Fail(FailureType.InvalidRequest, because);
    }
    
    public static Result<T> Fail<T>(string because = "")
    {
        return Result<T>.Fail(FailureType.Generic, because);
    }

    public static Result RemoveValue<T>(Result<T> status)
    {
        return new Result(status);
    }
    
    internal static Result<T> Create<T>(ITypedResult<T> result)
    {
        return Result<T>.Create(result);
    }
}

public class Result<T> : CoreResult<T, Result>
{
    private Result(T value) : base(value)
    {
    }
    
    private Result(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    private Result(ITypedResult<T> result) : base(result)
    {
    }
    
    internal static Result<T> Pass(T value)
    {
        return new Result<T>(value);
    }

    internal static Result<T> Fail(FailureType failureType, string because = "")
    {
        return new Result<T>(failureType, because);
    }
    
    internal static Result<T> Create(ITypedResult<T> result)
    {
        return new Result<T>(result);
    }
    
    public static implicit operator Result(Result<T> result)
    {
        return result.RemoveValue();
    }
    
    public static implicit operator Result<T>(T value)
    {
        return Pass(value);
    }
}

public static class ResultExtensions
{
    public static Result<T> AsResult<T>(this T value)
    {
        return value;
    }
    
    public static Result<T> ToTypedResult<T>(this ITypedResult<T> result)
    {
        return Result<T>.Create(result);
    }
    
    public static Result ToResult(this IResultStatus result)
    {
        return Result.Create(result);
    }

}