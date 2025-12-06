using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Convertables;
using DDD.Core.Results.Convertables.Interfaces;
using DDD.Core.Results.Helpers;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

public class UseCaseResult : ResultConvertable, IResultFactory<UseCaseResult>
{
    private UseCaseResult() 
        : base(ResultLayer.UseCase)
    {
    }
    private UseCaseResult(IResultConvertable resultStatus) 
        : base(resultStatus, ResultLayer.UseCase)
    {
    }
    
    private UseCaseResult(FailureType failureType, string because) 
        : base(failureType, ResultLayer.UseCase, because)
    {
    }
    
    public UseCaseResult<T> ToTypedUseCaseResult<T>()
    {
        return UseCaseResult<T>.From(this);
    }

    public static UseCaseResult Pass()
    {
        return new UseCaseResult();
    }

    public static UseCaseResult Fail(string because = "")
    {
        return new UseCaseResult(FailureType.Generic, because);
    }
    
    public static UseCaseResult InvariantViolation(string because = "")
    {
        return new UseCaseResult(FailureType.InvariantViolation, because);
    }
    
    public static UseCaseResult Merge(params IResultConvertable[] results)
    {
        return ResultCreationHelper.Merge<UseCaseResult, IResultConvertable>(results);
    }
    
    public static UseCaseResult From(IResultConvertable result)
    {
        return new UseCaseResult(result);
    }
    
    public static UseCaseResult<T> From<T>(IResultConvertable<T> result)
    {
        return UseCaseResult<T>.From(result);
    }
    
    public static UseCaseResult<T> From<T>(IResultConvertable result)
    {
        return UseCaseResult<T>.From(result);
    }
    
    public static UseCaseResult<T> Pass<T>(T value)
    {
        return UseCaseResult<T>.Pass(value);
    }

    public static UseCaseResult<T> Fail<T>(string because = "")
    {
        return UseCaseResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static UseCaseResult<T> InvariantViolation<T>(string because = "")
    {
        return UseCaseResult<T>.Fail(FailureType.InvariantViolation, because);
    }
    
    public static UseCaseResult Copy(UseCaseResult result)
    {
        return From(result);
    }
    
    public static UseCaseResult<T> Copy<T>(UseCaseResult<T> result)
    {
        return UseCaseResult<T>.From(result);
    }
}

public class UseCaseResult<T> : ResultConvertable<T>
{
    private UseCaseResult(T value) 
        : base(value, ResultLayer.UseCase)
    {
    }

    private UseCaseResult(FailureType failureType, string because) 
        : base(failureType, ResultLayer.UseCase, because)
    {
    }

    private UseCaseResult(IResultConvertable<T> result) 
        : base(result, ResultLayer.UseCase)
    {
    }
    
    private UseCaseResult(IResultConvertable result) 
        : base(result, ResultLayer.UseCase)
    {
    }
    
    public UseCaseResult RemoveType()
    {
        return UseCaseResult.From((IResultConvertable)this);
    }
    
    public UseCaseResult<T2> ToTypedUseCaseResult<T2>()
    {
        return UseCaseResult<T2>.From(this);
    }
    
    internal static UseCaseResult<T> Pass(T value)
    {
        return new UseCaseResult<T>(value);
    }

    internal static UseCaseResult<T> Fail(FailureType failureType, string because = "")
    {
        return new UseCaseResult<T>(failureType, because);
    }
    
    internal static UseCaseResult<T> From(IResultConvertable<T> result)
    {
        return new UseCaseResult<T>(result);
    }
    
    internal static UseCaseResult<T> From(IResultConvertable result)
    {
        return new UseCaseResult<T>(result);
    }
    
    public static implicit operator UseCaseResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator UseCaseResult(UseCaseResult<T> result)
    {
        return result.RemoveType();
    }
    
    public static implicit operator UseCaseResult<T>(ResultConvertable result)
    {
        return new UseCaseResult<T>(result);
    }
}