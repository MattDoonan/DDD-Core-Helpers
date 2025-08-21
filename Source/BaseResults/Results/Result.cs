using Outputs.Results.Abstract;
using Outputs.Results.Interfaces;

namespace Outputs.Results;

public class Result : BasicResult<Result>, IAdvancedResult<Result>
{
    private Result(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private Result()
    {
    }
    
    private Result(FailureType failureType, string because) : base(failureType, because)
    {
    }

    public static Result Pass()
    {
        return new Result();
    }
    
    public static Result Fail(string because = "")
    {
        return new Result(FailureType.Generic, because);
    }
    
    public static Result Fail(FailureType failureType, string because)
    {
        return new Result(failureType, because);
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
    
    public static Result Create(IResultStatus result)
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
    
    public static Result<T> Create<T>(IContentResult<T> result)
    {
        return Result<T>.Create(result);
    }
}

public class Result<T> : AdvancedContentResultBase<T, Result>
{
    private Result(T value) : base(value)
    {
    }
    
    private Result(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    
    private Result(IResultStatus result) : base(result)
    {
    }
    
    private Result(IContentResult<T> result) : base(result)
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
    
    internal static Result<T> Create(IContentResult<T> result)
    {
        return new Result<T>(result);
    }
    
    public static implicit operator Result<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator Result<T>(Result result)
    {
        if (result.IsSuccessful)
        {
            throw new InvalidOperationException(
                "Cannot implicitly convert a successful Result to a typed Result<T>. Use an explicit constructor or method instead.");
        }
        return new Result<T>(result);
    }
}