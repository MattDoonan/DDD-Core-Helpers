using Core.Enitities.AggregateRoot;
using Core.Results.Base.Abstract;
using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;
using Core.Results.Basic;

namespace Core.Results.Advanced;

public static class RepoResult
{
    public static RepoResult<T> Pass<T>(T value)
        where T : IAggregateRoot
    {
        return RepoResult<T>.Pass(value);
    }
    
    public static RepoResult<T> Fail<T>(string because = "")
        where T : IAggregateRoot
    {
        return RepoResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static RepoResult<T> NotFound<T>(string because = "")
        where T : IAggregateRoot
    {
        return RepoResult<T>.Fail(FailureType.NotFound, because);
    }
    
    public static RepoResult<T> AlreadyExists<T>(string because = "")
        where T : IAggregateRoot
    {
        return RepoResult<T>.Fail(FailureType.AlreadyExists, because);
    }
    
    public static RepoResult<T> InvalidRequest<T>(string because = "")
        where T : IAggregateRoot
    {
        return RepoResult<T>.Fail(FailureType.InvalidRequest, because);
    }
    
    public static RepoResult<T> OperationTimout<T>(string because = "")
        where T : IAggregateRoot
    {
        return RepoResult<T>.Fail(FailureType.OperationTimeout, because);
    }
    
    public static RepoResult<T> Copy<T>(RepoResult<T> result)
        where T : IAggregateRoot
    {
        return RepoResult<T>.Create(result);
    }
}

public class RepoResult<T> : TypedResult<T>
    where T : IAggregateRoot
{
    private RepoResult(T value) : base(value)
    {
    }
    
    private RepoResult(FailureType failureType, string because) : base(failureType, FailedLayer.Infrastructure, because)
    {
    }
    
    private RepoResult(ITypedResult<T> result) : base( result)
    {
    }
    
    internal static RepoResult<T> Pass(T value)
    {
        return new RepoResult<T>(value);
    }

    internal static RepoResult<T> Fail(FailureType failureType, string because = "")
    {
        return new RepoResult<T>(failureType, because);
    }
    
    internal static RepoResult<T> Create(ITypedResult<T> result)
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
    
    public static implicit operator RepoResult<T>(EntityResult<T> result)
    {
        return Create(result);
    }
    
    public static implicit operator ServiceResult<T>(RepoResult<T> result)
    {
        return ServiceResult<T>.Create(result);
    }
    
    public static implicit operator ServiceResult(RepoResult<T> result)
    {
        return ServiceResult.Create(result);
    }
    
    public ServiceResult<T> ToTypedServiceResult()
    {
        return this;
    }
    
    public ServiceResult ToServiceResult()
    {
        return this;
    }
    
    public static implicit operator UseCaseResult<T>(RepoResult<T> result)
    {
        return UseCaseResult<T>.Create(result);
    }
    
    public static implicit operator UseCaseResult(RepoResult<T> result)
    {
        return UseCaseResult.Create(result);
    }
    
    public UseCaseResult<T> ToTypedUseCaseResult()
    {
        return this;
    }
    
    public UseCaseResult ToUseCaseResult()
    {
        return this;
    }

}

public static class RepoResultExtensions
{
    public static RepoResult<T> AsTypedRepoResult<T>(this T value)
        where T : IAggregateRoot
    {
        return value;
    }
    
    public static RepoResult<T> ToTypedRepoResult<T>(this EntityResult<T> result)
        where T : IAggregateRoot
    {
        return result;
    }
}