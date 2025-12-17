using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Convertibles.Interfaces;
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
    
    private RepoResult(FailureType failureType, string? because) : base(failureType, ResultLayer.Infrastructure, because)
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
        return new RepoResult(FailureType.Generic, because);
    }

    public static RepoResult Copy(RepoResult result)
    {
        return new RepoResult(result);
    }

    public static RepoResult NotFound(string? because = null)
    {
        return new RepoResult(FailureType.NotFound, because);
    }
    
    public static RepoResult AlreadyExists(string? because = null)
    {
        return new RepoResult(FailureType.AlreadyExists, because);
    }
    
    public static RepoResult InvalidRequest(string? because = null)
    {
        return new RepoResult(FailureType.InvalidRequest, because);
    }
    
    public static RepoResult ConcurrencyViolation(string? because = null)
    {
        return new RepoResult(FailureType.ConcurrencyViolation, because);
    }
    
    public static RepoResult OperationTimeout(string? because = null)
    {
        return new RepoResult(FailureType.OperationTimeout, because);
    }
    
    public static RepoResult OperationCancelled(string? because = null)
    {
        return new RepoResult(FailureType.OperationCancelled, because);
    }
    
    public static RepoResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<RepoResult>();
    }

    public static RepoResult From(IResultStatus result)
    {
        return new RepoResult(result);
    }
    
    public static RepoResult From(Exception exception, FailureType failureType = FailureType.Generic)
    {
        return new RepoResult(failureType, exception.ToResultMessage());
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
        return RepoResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static RepoResult<T> NotFound<T>(string? because = null)
    {
        return RepoResult<T>.Fail(FailureType.NotFound, because);
    }
    
    public static RepoResult<T> AlreadyExists<T>(string? because = null)
    {
        return RepoResult<T>.Fail(FailureType.AlreadyExists, because);
    }
    
    public static RepoResult<T> InvalidRequest<T>(string? because = null)
    {
        return RepoResult<T>.Fail(FailureType.InvalidRequest, because);
    }
    
    public static RepoResult<T> ConcurrencyViolation<T>(string? because = null)
    {
        return RepoResult<T>.Fail(FailureType.ConcurrencyViolation, because);
    }
    
    public static RepoResult<T> OperationTimeout<T>(string? because = null)
    {
        return RepoResult<T>.Fail(FailureType.OperationTimeout, because);
    }
    
    public static RepoResult<T> OperationCancelled<T>(string? because = null)
    {
        return RepoResult<T>.Fail(FailureType.OperationCancelled, because);
    }
    
    public static RepoResult<T> Copy<T>(RepoResult<T> result)
    {
        return RepoResult<T>.From(result);
    }
    
    public static RepoResult<T> From<T>(Exception exception, FailureType failureType = FailureType.Generic)
    {
        return RepoResult<T>.Fail(failureType, ResultErrorMessageExtension.ToResultMessage(exception));
    }
}

public class RepoResult<T> : ServiceConvertible<T>
{
    private RepoResult(T value) 
        : base(value, ResultLayer.Infrastructure)
    {
    }
    
    private RepoResult(FailureType failureType, string? because) 
        : base(failureType, ResultLayer.Infrastructure, because)
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

    internal static RepoResult<T> Fail(FailureType failureType, string? because = null)
    {
        return new RepoResult<T>(failureType, because);
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