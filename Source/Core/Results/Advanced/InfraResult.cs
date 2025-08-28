using Core.Results.Base.Abstract;
using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;

namespace Core.Results.Advanced;

public class InfraResult : CoreResult<InfraResult>, IResultFactory<InfraResult>
{
    private InfraResult(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private InfraResult()
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
    
    public static InfraResult Copy(InfraResult result)
    {
        return Create(result);
    }
    
    public static InfraResult<T> Copy<T>(InfraResult<T> result)
    {
        return InfraResult<T>.Create(result);
    }
    
    public static implicit operator ServiceResult(InfraResult result)
    {
        return ServiceResult.Create(result);
    }
    
    public ServiceResult ToServiceResult()
    {
        return this;
    }
    
    public static implicit operator UseCaseResult(InfraResult result)
    {
        return UseCaseResult.Create(result);
    }
    
    public UseCaseResult ToUseCaseResult()
    {
        return this;
    }

    internal static InfraResult Create(IResultStatus result)
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

public class InfraResult<T>  : CoreResult<T, InfraResult>
{
    private InfraResult(T value) : base(value)
    {
    }
    
    private InfraResult(FailureType failureType, string because) : base(failureType, FailedLayer.Infrastructure, because)
    {
    }
    
    private InfraResult(ITypedResult<T> result) : base( result)
    {
    }
    
    internal static InfraResult<T> Pass(T value)
    {
        return new InfraResult<T>(value);
    }

    internal static InfraResult<T> Fail(FailureType failureType, string because = "")
    {
        return new InfraResult<T>(failureType, because);
    }
    
    internal static InfraResult<T> Create(ITypedResult<T> result)
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
    
    public static implicit operator ServiceResult<T>(InfraResult<T> result)
    {
        return ServiceResult<T>.Create(result);
    }
    
    public static implicit operator ServiceResult(InfraResult<T> result)
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
    
    public static implicit operator UseCaseResult<T>(InfraResult<T> result)
    {
        return UseCaseResult<T>.Create(result);
    }
    
    public static implicit operator UseCaseResult(InfraResult<T> result)
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