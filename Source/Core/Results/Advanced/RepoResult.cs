using Core.Entities.AggregateRoot;
using Core.Results.Advanced.Abstract;
using Core.Results.Advanced.Interfaces;
using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;
using Core.Results.Helpers;

namespace Core.Results.Advanced;

public class RepoResult : ServiceConvertable, IResultFactory<RepoResult>
{
    
    private RepoResult()
    {
    }
    private RepoResult(IServiceConvertable resultStatus) : base(resultStatus)
    {
    }
    
    private RepoResult(FailureType failureType, string because) : base(failureType, FailedLayer.Infrastructure, because)
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

    public static RepoResult Fail(string because = "")
    {
        return new RepoResult(FailureType.Generic, because);
    }

    public static RepoResult Copy(RepoResult result)
    {
        return new RepoResult(result);
    }

    public static RepoResult NotFound(string because = "")
    {
        return new RepoResult(FailureType.NotFound, because);
    }
    
    public static RepoResult AlreadyExists(string because = "")
    {
        return new RepoResult(FailureType.AlreadyExists, because);
    }
    
    public static RepoResult InvalidRequest(string because = "")
    {
        return new RepoResult(FailureType.InvalidRequest, because);
    }
    
    public static RepoResult ConcurrencyViolation(string because = "")
    {
        return new RepoResult(FailureType.ConcurrencyViolation, because);
    }
    
    public static RepoResult OperationTimeout(string because = "")
    {
        return new RepoResult(FailureType.OperationTimeout, because);
    }
    
    public static RepoResult Merge(params IServiceConvertable[] results)
    {
        return ResultCreationHelper.Merge<RepoResult, IServiceConvertable>(results);
    }

    public static RepoResult From(IServiceConvertable result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.Unknown })
        {
            return new RepoResult(result)
            {
                FailedLayer = FailedLayer.Infrastructure
            };   
        }
        return new RepoResult(result);
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
        where T : IAggregateRoot
    {
        return RepoResult<T>.Pass(value);
    }
    
    public static RepoResult<IEnumerable<T>> Pass<T>(IEnumerable<T> value)
        where T : IAggregateRoot
    {
        return RepoResult<IEnumerable<T>>.Pass(value);
    }
    
    public static RepoResult<T> Fail<T>(string because = "")
    {
        return RepoResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static RepoResult<T> NotFound<T>(string because = "")
    {
        return RepoResult<T>.Fail(FailureType.NotFound, because);
    }
    
    public static RepoResult<T> AlreadyExists<T>(string because = "")
    {
        return RepoResult<T>.Fail(FailureType.AlreadyExists, because);
    }
    
    public static RepoResult<T> InvalidRequest<T>(string because = "")
    {
        return RepoResult<T>.Fail(FailureType.InvalidRequest, because);
    }
    
    public static RepoResult<T> ConcurrencyViolation<T>(string because = "")
    {
        return RepoResult<T>.Fail(FailureType.ConcurrencyViolation, because);
    }
    
    public static RepoResult<T> OperationTimeout<T>(string because = "")
    {
        return RepoResult<T>.Fail(FailureType.OperationTimeout, because);
    }
    
    public static RepoResult<T> Copy<T>(RepoResult<T> result)
    {
        return RepoResult<T>.From(result);
    }
}

public class RepoResult<T> : ServiceConvertable<T>
{
    private RepoResult(T value) : base(value)
    {
    }
    
    private RepoResult(FailureType failureType, string because) : base(failureType, FailedLayer.Infrastructure, because)
    {
    }
    
    private RepoResult(IServiceConvertable<T> result) : base(result)
    {
    }
    
    private RepoResult(IServiceConvertable result) : base(result)
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

    internal static RepoResult<T> Fail(FailureType failureType, string because = "")
    {
        return new RepoResult<T>(failureType, because);
    }
    
    internal static RepoResult<T> From(IServiceConvertable<T> result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.Unknown })
        {
            return new RepoResult<T>(result)
            {
                FailedLayer = FailedLayer.Infrastructure
            };   
        }
        return new RepoResult<T>(result);
    }
    
    internal static RepoResult<T> From(IServiceConvertable result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.Unknown })
        {
            return new RepoResult<T>(result)
            {
                FailedLayer = FailedLayer.Infrastructure
            };   
        }
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