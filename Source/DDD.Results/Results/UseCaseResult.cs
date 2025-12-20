using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

public class UseCaseResult : ResultConvertible, IResultFactory<UseCaseResult>
{
    private UseCaseResult() 
        : base(ResultLayer.UseCase)
    {
    }
    private UseCaseResult(IResultStatus resultStatus) 
        : base(resultStatus, ResultLayer.UseCase)
    {
    }
    
    private UseCaseResult(FailedOperationStatus failedOperationStatus, string because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.UseCase, because))
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
        return new UseCaseResult(OperationStatus.Failure(), because);
    }
    
    public static UseCaseResult InvariantViolation(string because = "")
    {
        return new UseCaseResult(OperationStatus.InvariantViolation(), because);
    }
    
    public static UseCaseResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<UseCaseResult>();
    }
    
    public static UseCaseResult From(IResultStatus result)
    {
        return new UseCaseResult(result);
    }
    
    public static UseCaseResult<T> From<T>(ITypedResult<T> result)
    {
        return UseCaseResult<T>.From(result);
    }
    
    public static UseCaseResult<T> From<T>(IResultStatus result)
    {
        return UseCaseResult<T>.From(result);
    }
    
    public static UseCaseResult<T> Pass<T>(T value)
    {
        return UseCaseResult<T>.Pass(value);
    }

    public static UseCaseResult<T> Fail<T>(string because = "")
    {
        return UseCaseResult<T>.Fail(OperationStatus.Failure<T>(), because);
    }
    
    public static UseCaseResult<T> InvariantViolation<T>(string because = "")
    {
        return UseCaseResult<T>.Fail(OperationStatus.InvariantViolation<T>(), because);
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

public class UseCaseResult<T> : ResultConvertible<T>
{
    private UseCaseResult(T value) 
        : base(value, ResultLayer.UseCase)
    {
    }

    private UseCaseResult(FailedOperationStatus failedOperationStatus, string because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.UseCase, because))
    {
    }

    private UseCaseResult(ITypedResult<T> result) 
        : base(result, ResultLayer.UseCase)
    {
    }
    
    private UseCaseResult(IResultStatus result) 
        : base(result, ResultLayer.UseCase)
    {
    }
    
    public UseCaseResult RemoveType()
    {
        return UseCaseResult.From((IResultStatus)this);
    }
    
    public UseCaseResult<T2> ToTypedUseCaseResult<T2>()
    {
        return UseCaseResult<T2>.From(this);
    }
    
    internal static UseCaseResult<T> Pass(T value)
    {
        return new UseCaseResult<T>(value);
    }

    internal static UseCaseResult<T> Fail(FailedOperationStatus failedOperationStatus, string because = "")
    {
        return new UseCaseResult<T>(failedOperationStatus, because);
    }
    
    internal static UseCaseResult<T> From(ITypedResult<T> result)
    {
        return new UseCaseResult<T>(result);
    }
    
    internal static UseCaseResult<T> From(IResultStatus result)
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
    
    public static implicit operator UseCaseResult<T>(ResultConvertible result)
    {
        return new UseCaseResult<T>(result);
    }
}