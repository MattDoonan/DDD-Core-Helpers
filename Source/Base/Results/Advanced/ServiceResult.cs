using Outputs.ObjectTypes;
using Outputs.Results.Base.Abstract;
using Outputs.Results.Base.Enums;
using Outputs.Results.Base.Interfaces;
using Outputs.Results.Basic;

namespace Outputs.Results.Advanced;

public class ServiceResult : CoreResult<ServiceResult>, IResultFactory<ServiceResult>
{
     private ServiceResult(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private ServiceResult()
    {
    }
    
    private ServiceResult(FailureType failureType, string because) : base(failureType, FailedLayer.Service, because)
    {
    }
    
    public static ServiceResult Pass()
    {
        return new ServiceResult();
    }

    public static ServiceResult Fail(string because = "")
    {
        return new ServiceResult(FailureType.Generic, because);
    }
    
    public static ServiceResult DomainViolation(string because = "")
    {
        return new ServiceResult(FailureType.DomainViolation, because);
    }
    
    public static ServiceResult NotAllowed(string because = "")
    {
        return new ServiceResult(FailureType.NotAllowed, because);
    }
    
    public static ServiceResult<T> Pass<T>(T value)
    {
        return ServiceResult<T>.Pass(value);
    }

    public static ServiceResult<T> Fail<T>(string because = "")
    {
        return ServiceResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static ServiceResult<T> DomainViolation<T>(string because = "")
    {
        return ServiceResult<T>.Fail(FailureType.DomainViolation, because);
    }
    
    public static ServiceResult<T> NotAllowed<T>(string because = "")
    {
        return ServiceResult<T>.Fail(FailureType.NotAllowed, because);
    }
    
    public static ServiceResult RemoveValue<T>(ServiceResult<T> status)
    {
        return new ServiceResult(status);
    }
    
    internal static ServiceResult Create(IResultStatus result)
    {
        if (result.FailedLayer == FailedLayer.None)
        {
            return new ServiceResult(result)
            {
                FailedLayer = FailedLayer.Service
            };   
        }
        return new ServiceResult(result);
    }
    
    public static implicit operator UseCaseResult(ServiceResult result)
    {
        return UseCaseResult.Create(result);
    }
    
    public UseCaseResult ToUseCaseResult()
    {
        return this;
    }
}

public class ServiceResult<T> : CoreResult<T, ServiceResult>
{
    private ServiceResult(T value) : base(value)
    {
    }
    
    private ServiceResult(FailureType failureType, string because) : base(failureType, FailedLayer.Service, because)
    {
    }
    
    private ServiceResult(ITypedResult<T> result) : base(result)
    {
    }
    
    internal static ServiceResult<T> Pass(T value)
    {
        return new ServiceResult<T>(value);
    }

    internal static ServiceResult<T> Fail(FailureType failureType, string because = "")
    {
        return new ServiceResult<T>(failureType, because);
    }
    
    internal static ServiceResult<T> Create(ITypedResult<T> result)
    {
        if (result.FailedLayer == FailedLayer.None)
        {
            return new ServiceResult<T>(result)
            {
                FailedLayer = FailedLayer.Service
            };   
        }
        return new ServiceResult<T>(result);
    }
    
    public static implicit operator ServiceResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator UseCaseResult<T>(ServiceResult<T> result)
    {
        return UseCaseResult<T>.Create(result);
    }
    
    public static implicit operator UseCaseResult(ServiceResult<T> result)
    {
        return UseCaseResult.Create(result);
    }
    
    public UseCaseResult<T> ToUseCaseTypedResult()
    {
        return this;
    }
    
    public UseCaseResult ToUseCaseResult()
    {
        return this;
    }
}

public static class ServiceResultExtensions
{
    public static ServiceResult<T> AsTypedServiceResult<T>(this T value)
    {
        return value;
    }
}