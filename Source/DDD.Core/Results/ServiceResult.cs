using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Convertables;
using DDD.Core.Results.Convertables.Interfaces;
using DDD.Core.Results.Helpers;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

public class ServiceResult : UseCaseConvertable, IResultFactory<ServiceResult>
{
    private ServiceResult() 
        : base(ResultLayer.Service)
    {
    }
    
     private ServiceResult(IUseCaseConvertable resultStatus) 
         : base(resultStatus, ResultLayer.Service)
    {
    }
    
    private ServiceResult(FailureType failureType, string? because) 
        : base(failureType, ResultLayer.Service, because)
    {
    }
    
    public ServiceResult<T> ToTypedServiceResult<T>()
    {
        return ServiceResult<T>.From(this);
    }
    
    public static ServiceResult Pass()
    {
        return new ServiceResult();
    }

    public static ServiceResult Fail(string? because = null)
    {
        return new ServiceResult(FailureType.Generic, because);
    }
    
    public static ServiceResult DomainViolation(string? because = null)
    {
        return new ServiceResult(FailureType.DomainViolation, because);
    }
    
    public static ServiceResult NotAllowed(string? because = null)
    {
        return new ServiceResult(FailureType.NotAllowed, because);
    }
    
    public static ServiceResult NotFound(string? because = null)
    {
        return new ServiceResult(FailureType.NotFound, because);
    }
    
    public static ServiceResult InvariantViolation(string? because = null)
    {
        return new ServiceResult(FailureType.InvariantViolation, because);
    }
    
    public static ServiceResult Merge(params IUseCaseConvertable[] results)
    {
        return ResultCreationHelper.Merge<ServiceResult, IUseCaseConvertable>(results);
    }
    
    public static ServiceResult From(IUseCaseConvertable result)
    {
        return new ServiceResult(result);
    }
    
    public static ServiceResult<T> From<T>(IUseCaseConvertable<T> result)
    {
        return ServiceResult<T>.From(result);
    }
    
    public static ServiceResult<T> From<T>(IUseCaseConvertable result)
    {
        return ServiceResult<T>.From(result);
    }
    
    public static ServiceResult<T> Pass<T>(T value)
    {
        return ServiceResult<T>.Pass(value);
    }

    public static ServiceResult<T> Fail<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static ServiceResult<T> DomainViolation<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(FailureType.DomainViolation, because);
    }
    
    public static ServiceResult<T> NotAllowed<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(FailureType.NotAllowed, because);
    }
    
    public static ServiceResult<T> NotFound<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(FailureType.NotFound, because);
    }
    
    public static ServiceResult<T> InvariantViolation<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(FailureType.InvariantViolation, because);
    }
    
    public static ServiceResult Copy(ServiceResult result)
    {
        return From(result);
    }
    
    public static ServiceResult<T> Copy<T>(ServiceResult<T> result)
    {
        return ServiceResult<T>.From(result);
    }
}

public class ServiceResult<T> : UseCaseConvertable<T>
{
    private ServiceResult(T value) 
        : base(value, ResultLayer.Service)
    {
    }
    
    private ServiceResult(FailureType failureType, string? because) 
        : base(failureType, ResultLayer.Service, because)
    {
    }
    
    private ServiceResult(IUseCaseConvertable<T> result) 
        : base(result, ResultLayer.Service)
    {
    }
    
    private ServiceResult(IUseCaseConvertable result)
        : base(result, ResultLayer.Service)
    {
    }
    
    public ServiceResult RemoveType()
    {
        return ServiceResult.From((IUseCaseConvertable)this);
    }
    
    public ServiceResult<T2> ToTypedServiceResult<T2>()
    {
        return ServiceResult<T2>.From(this);
    }
    
    internal static ServiceResult<T> Pass(T value)
    {
        return new ServiceResult<T>(value);
    }

    internal static ServiceResult<T> Fail(FailureType failureType, string? because = null)
    {
        return new ServiceResult<T>(failureType, because);
    }
    
    internal static ServiceResult<T> From(IUseCaseConvertable<T> result)
    {
        return new ServiceResult<T>(result);
    }
    
    internal static ServiceResult<T> From(IUseCaseConvertable result)
    {
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