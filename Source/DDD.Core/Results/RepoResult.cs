using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Convertables;
using DDD.Core.Results.Convertables.Interfaces;
using DDD.Core.Results.Helpers;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

public class RepoResult : ServiceConvertable, IResultFactory<RepoResult>
{
    
    private RepoResult() 
        : base(ResultLayer.Infrastructure)
    {
    }
    private RepoResult(IServiceConvertable resultStatus) 
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
    
    public static RepoResult Merge(params IServiceConvertable[] results)
    {
        return ResultCreationHelper.Merge<RepoResult, IServiceConvertable>(results);
    }

    public static RepoResult From(IServiceConvertable result)
    {
        return new RepoResult(result);
    }
    
    public static RepoResult From(Exception exception)
    {
        return new RepoResult(FailureType.Generic, ResultErrorMessage.ExceptionToMessage(exception));
    }
    
    public static RepoResult<T> From<T>(IServiceConvertable<T> result)
    {
        return RepoResult<T>.From(result);
    }
    
    public static RepoResult<T> From<T>(IServiceConvertable result)
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
    
    public static RepoResult<T> Copy<T>(RepoResult<T> result)
    {
        return RepoResult<T>.From(result);
    }
    
    public static RepoResult<T> From<T>(Exception exception)
    {
        return RepoResult<T>.Fail(FailureType.Generic, ResultErrorMessage.ExceptionToMessage(exception));
    }
}

public class RepoResult<T> : ServiceConvertable<T>
{
    private RepoResult(T value) 
        : base(value, ResultLayer.Infrastructure)
    {
    }
    
    private RepoResult(FailureType failureType, string? because) 
        : base(failureType, ResultLayer.Infrastructure, because)
    {
    }
    
    private RepoResult(IServiceConvertable<T> result) 
        : base(result, ResultLayer.Infrastructure)
    {
    }
    
    private RepoResult(IServiceConvertable result) 
        : base(result, ResultLayer.Infrastructure)
    {
    }
    
    public RepoResult RemoveType()
    {
        return RepoResult.From((IServiceConvertable)this);
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
    
    internal static RepoResult<T> From(IServiceConvertable<T> result)
    {
        return new RepoResult<T>(result);
    }
    
    internal static RepoResult<T> From(IServiceConvertable result)
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
    
    public static implicit operator RepoResult<T>(ServiceConvertable result)
    {
        return new RepoResult<T>(result);
    }
}