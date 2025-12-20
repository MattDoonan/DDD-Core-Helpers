using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

public class RepoResult : ServiceConvertible, IResultFactory<RepoResult>
{
    
    private RepoResult() 
        : base(ResultLayer.Infrastructure)
    {
    }
    private RepoResult(IResultStatus resultStatus) 
        : base(resultStatus, ResultLayer.Infrastructure)
    {
    }
    
    private RepoResult(FailedOperationStatus failedOperationStatus, string? because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Infrastructure, because))
    {
    }
    
    public RepoResult<T> ToTypedRepoResult<T>()
    {
        return RepoResult<T>.From(this);
    }
    
    public static RepoResult Pass()
    {
        return new RepoResult();
    }

    public static RepoResult Fail(string? because = null)
    {
        return new RepoResult(OperationStatus.Failure(), because);
    }

    public static RepoResult Copy(RepoResult result)
    {
        return new RepoResult(result);
    }

    public static RepoResult NotFound(string? because = null)
    {
        return new RepoResult(OperationStatus.NotFound(), because);
    }
    
    public static RepoResult AlreadyExists(string? because = null)
    {
        return new RepoResult(OperationStatus.AlreadyExists(), because);
    }
    
    public static RepoResult InvalidRequest(string? because = null)
    {
        return new RepoResult(OperationStatus.InvalidRequest(), because);
    }
    
    public static RepoResult ConcurrencyViolation(string? because = null)
    {
        return new RepoResult(OperationStatus.ConcurrencyViolation(), because);
    }
    
    public static RepoResult OperationTimeout(string? because = null)
    {
        return new RepoResult(OperationStatus.TimedOut(), because);
    }
    
    public static RepoResult OperationCancelled(string? because = null)
    {
        return new RepoResult(OperationStatus.Cancelled(), because);
    }
    
    public static RepoResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<RepoResult>();
    }

    public static RepoResult From(IResultStatus result)
    {
        return new RepoResult(result);
    }
    
    public static RepoResult From(Exception exception, FailedOperationStatus? operationStatus = null)
    {
        return new RepoResult(operationStatus ?? OperationStatus.Failure(), exception.ToResultMessage());
    }
    
    public static RepoResult<T> From<T>(ITypedResult<T> result)
    {
        return RepoResult<T>.From(result);
    }
    
    public static RepoResult<T> From<T>(IResultStatus result)
    {
        return RepoResult<T>.From(result);
    }
    
    public static RepoResult<T> Pass<T>(T value)
    {
        return RepoResult<T>.Pass(value);
    }
    
    public static RepoResult<T> Fail<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.Failure<T>(), because);
    }
    
    public static RepoResult<T> NotFound<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.NotFound<T>(), because);
    }
    
    public static RepoResult<T> AlreadyExists<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.AlreadyExists<T>(), because);
    }
    
    public static RepoResult<T> InvalidRequest<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.InvalidRequest<T>(), because);
    }
    
    public static RepoResult<T> ConcurrencyViolation<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.ConcurrencyViolation<T>(), because);
    }
    
    public static RepoResult<T> OperationTimeout<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.TimedOut<T>(), because);
    }
    
    public static RepoResult<T> OperationCancelled<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.Cancelled<T>(), because);
    }
    
    public static RepoResult<T> Copy<T>(RepoResult<T> result)
    {
        return RepoResult<T>.From(result);
    }
    
    public static RepoResult<T> From<T>(Exception exception, FailedOperationStatus? operationStatus = null)
    {
        return RepoResult<T>.Fail(operationStatus ?? OperationStatus.Failure<T>(), exception.ToResultMessage());
    }
}

public class RepoResult<T> : ServiceConvertible<T>
{
    private RepoResult(T value) 
        : base(value, ResultLayer.Infrastructure)
    {
    }
    
    private RepoResult(FailedOperationStatus failedOperationStatus, string? because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Infrastructure, because))
    {
    }
    
    private RepoResult(ITypedResult<T> result) 
        : base(result, ResultLayer.Infrastructure)
    {
    }
    
    private RepoResult(IResultStatus result) 
        : base(result, ResultLayer.Infrastructure)
    {
    }
    
    public RepoResult RemoveType()
    {
        return RepoResult.From((IResultStatus)this);
    }
    
    public RepoResult<T2> ToTypedRepoResult<T2>()
    {
        return RepoResult<T2>.From(this);
    }
    
    internal static RepoResult<T> Pass(T value)
    {
        return new RepoResult<T>(value);
    }

    internal static RepoResult<T> Fail(FailedOperationStatus failedOperationStatus, string? because = null)
    {
        return new RepoResult<T>(failedOperationStatus, because);
    }
    
    internal static RepoResult<T> From(ITypedResult<T> result)
    {
        return new RepoResult<T>(result);
    }
    
    internal static RepoResult<T> From(IResultStatus result)
    {
        return new RepoResult<T>(result);
    }
    
    public static implicit operator RepoResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator RepoResult(RepoResult<T> result)
    {
        return result.RemoveType();
    }
    
    public static implicit operator RepoResult<T>(ServiceConvertible result)
    {
        return new RepoResult<T>(result);
    }
}