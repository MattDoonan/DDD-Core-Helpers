using Core.Results.Advanced.Abstract;
using Core.Results.Advanced.Interfaces;
using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;

namespace Core.Results.Advanced;

public class ServiceResult : UseCaseConvertable, IResultFactory<ServiceResult>
{
    private ServiceResult()
    {
    }
    
     private ServiceResult(IUseCaseConvertable resultStatus) : base(resultStatus)
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
    
    public static ServiceResult Copy(ServiceResult result)
    {
        return Create(result);
    }
    
    public static ServiceResult<T> Copy<T>(ServiceResult<T> result)
    {
        return ServiceResult<T>.Create(result);
    }
    
    internal static ServiceResult Create(IUseCaseConvertable result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.Unknown })
        {
            return new ServiceResult(result)
            {
                FailedLayer = FailedLayer.Service
            };   
        }
        return new ServiceResult(result);
    }
}

public class ServiceResult<T> : UseCaseConvertable<T>
{
    private ServiceResult(T value) : base(value)
    {
    }
    
    private ServiceResult(FailureType failureType, string because) : base(failureType, FailedLayer.Service, because)
    {
    }
    
    private ServiceResult(IUseCaseConvertable<T> result) : base(result)
    {
    }
    
    private ServiceResult(IUseCaseConvertable result) : base(result)
    {
    }
    
    public ServiceResult RemoveType()
    {
        return ServiceResult.Create(this);
    }
    
    internal static ServiceResult<T> Pass(T value)
    {
        return new ServiceResult<T>(value);
    }

    internal static ServiceResult<T> Fail(FailureType failureType, string because = "")
    {
        return new ServiceResult<T>(failureType, because);
    }
    
    internal static ServiceResult<T> Create(IUseCaseConvertable<T> result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.Unknown })
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
    
    public static implicit operator ServiceResult(ServiceResult<T> result)
    {
        return result.RemoveType();
    }
    
    public static implicit operator ServiceResult<T>(UseCaseConvertable result)
    {
        return new ServiceResult<T>(result);
    }
}