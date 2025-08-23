using Outputs.ObjectTypes;
using Outputs.Results.Base.Abstract;
using Outputs.Results.Base.Enums;
using Outputs.Results.Base.Interfaces;
using Outputs.Results.Basic;

namespace Outputs.Results.Advanced;

public class RepoResult
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
        return RepoResult<T>.Fail(FailureType.OperationTimout, because);
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
    
    private RepoResult(ITypedResult<T> result) : base(result)
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
}

public static class RepoResultExtensions
{
    public static RepoResult<T> AsRepoResult<T>(this T value)
        where T : IAggregateRoot
    {
        return value;
    }
    
    public static RepoResult<T> ToRepoResult<T>(this EntityResult<T> result)
        where T : IAggregateRoot
    {
        return result;
    }
}