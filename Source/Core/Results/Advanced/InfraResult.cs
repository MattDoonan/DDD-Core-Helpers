using Core.Results.Advanced.Abstract;
using Core.Results.Advanced.Interfaces;
using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;

namespace Core.Results.Advanced;

public class InfraResult : RepoConvertable, IResultFactory<InfraResult>
{
    private InfraResult()
    {
    }
    
    private InfraResult(IRepoConvertable resultStatus) : base(resultStatus)
    {
    }
    
    private InfraResult(FailureType failureType, string because) : base(failureType, FailedLayer.Infrastructure, because)
    {
    }
    
    public static InfraResult Pass()
    {
        return new InfraResult();
    }

    public static InfraResult Fail(string because = "")
    {
        return new InfraResult(FailureType.Generic, because);
    }
    
    public static InfraResult Copy(InfraResult result)
    {
        return Create(result);
    }
    
    public static InfraResult NotFound(string because = "")
    {
        return new InfraResult(FailureType.NotFound, because);
    }
    
    public static InfraResult AlreadyExists(string because = "")
    {
        return new InfraResult(FailureType.AlreadyExists, because);
    }
    
    public static InfraResult InvalidRequest(string because = "")
    {
        return new InfraResult(FailureType.InvalidRequest, because);
    }
    
    public static InfraResult OperationTimout(string because = "")
    {
        return new InfraResult(FailureType.OperationTimeout, because);
    }
    
    public static InfraResult<T> Pass<T>(T value)
    {
        return InfraResult<T>.Pass(value);
    }
    
    public static InfraResult<T> Fail<T>(string because = "")
    {
        return InfraResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static InfraResult<T> NotFound<T>(string because = "")
    {
        return InfraResult<T>.Fail(FailureType.NotFound, because);
    }
    
    public static InfraResult<T> AlreadyExists<T>(string because = "")
    {
        return InfraResult<T>.Fail(FailureType.AlreadyExists, because);
    }
    
    public static InfraResult<T> InvalidRequest<T>(string because = "")
    {
        return InfraResult<T>.Fail(FailureType.InvalidRequest, because);
    }
    
    public static InfraResult<T> OperationTimout<T>(string because = "")
    {
        return InfraResult<T>.Fail(FailureType.OperationTimeout, because);
    }
    
    public static InfraResult<T> Copy<T>(InfraResult<T> result)
    {
        return InfraResult<T>.Create(result);
    }

    internal static InfraResult Create(IRepoConvertable result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.Unknown })
        {
            return new InfraResult(result)
            {
                FailedLayer = FailedLayer.Infrastructure
            };   
        }
        return new InfraResult(result);
    }
}

public class InfraResult<T> : RepoConvertable<T>
{
    private InfraResult(T value) : base(value)
    {
    }
    
    private InfraResult(FailureType failureType, string because) : base(failureType, FailedLayer.Infrastructure, because)
    {
    }
    
    private InfraResult(IRepoConvertable<T> result) : base(result)
    {
    }
    
    private InfraResult(IRepoConvertable result) : base(result)
    {
    }
    
    public InfraResult RemoveType()
    {
        return InfraResult.Create(this);
    }
    
    internal static InfraResult<T> Pass(T value)
    {
        return new InfraResult<T>(value);
    }

    internal static InfraResult<T> Fail(FailureType failureType, string because = "")
    {
        return new InfraResult<T>(failureType, because);
    }
    
    internal static InfraResult<T> Create(IRepoConvertable<T> result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.Unknown })
        {
            return new InfraResult<T>(result)
            {
                FailedLayer = FailedLayer.Infrastructure
            };   
        }
        return new InfraResult<T>(result);
    }
    
    internal static InfraResult<T> Create(IRepoConvertable result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.Unknown })
        {
            return new InfraResult<T>(result)
            {
                FailedLayer = FailedLayer.Infrastructure
            };   
        }
        return new InfraResult<T>(result);
    }
    
    public static implicit operator InfraResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator InfraResult(InfraResult<T> result)
    {
        return result.RemoveType();
    }
    
    public static implicit operator InfraResult<T>(RepoConvertable result)
    {
        return new InfraResult<T>(result);
    }
}