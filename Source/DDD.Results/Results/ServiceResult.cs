using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

public class ServiceResult : UseCaseConvertible, IResultFactory<ServiceResult>
{
    private ServiceResult() 
        : base(ResultLayer.Service)
    {
    }
    
     private ServiceResult(IResultStatus resultStatus) 
         : base(resultStatus, ResultLayer.Service)
    {
    }
    
    private ServiceResult(FailedOperationStatus failedOperationStatus, string? because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Service, because))
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
        return new ServiceResult(OperationStatus.Failure(), because);
    }
    
    public static ServiceResult DomainViolation(string? because = null)
    {
        return new ServiceResult(OperationStatus.DomainViolation(), because);
    }
    
    public static ServiceResult NotAllowed(string? because = null)
    {
        return new ServiceResult(OperationStatus.NotAllowed(), because);
    }
    
    public static ServiceResult NotFound(string? because = null)
    {
        return new ServiceResult(OperationStatus.NotFound(), because);
    }
    
    public static ServiceResult InvariantViolation(string? because = null)
    {
        return new ServiceResult(OperationStatus.InvariantViolation(), because);
    }
    
    public static ServiceResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<ServiceResult>();
    }
    
    public static ServiceResult From(IResultStatus result)
    {
        return new ServiceResult(result);
    }
    
    public static ServiceResult<T> From<T>(ITypedResult<T> result)
    {
        return ServiceResult<T>.From(result);
    }
    
    public static ServiceResult<T> From<T>(IResultStatus result)
    {
        return ServiceResult<T>.From(result);
    }
    
    public static ServiceResult<T> Pass<T>(T value)
    {
        return ServiceResult<T>.Pass(value);
    }

    public static ServiceResult<T> Fail<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(OperationStatus.Failure<T>(), because);
    }
    
    public static ServiceResult<T> DomainViolation<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(OperationStatus.DomainViolation<T>(), because);
    }
    
    public static ServiceResult<T> NotAllowed<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(OperationStatus.NotAllowed<T>(), because);
    }
    
    public static ServiceResult<T> NotFound<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(OperationStatus.NotFound<T>(), because);
    }
    
    public static ServiceResult<T> InvariantViolation<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(OperationStatus.InvariantViolation<T>(), because);
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

public class ServiceResult<T> : UseCaseConvertible<T>
{
    private ServiceResult(T value) 
        : base(value, ResultLayer.Service)
    {
    }
    
    private ServiceResult(FailedOperationStatus failedOperationStatus, string? because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Service, because))
    {
    }
    
    private ServiceResult(ITypedResult<T> result) 
        : base(result, ResultLayer.Service)
    {
    }
    
    private ServiceResult(IResultStatus result)
        : base(result, ResultLayer.Service)
    {
    }
    
    public ServiceResult RemoveType()
    {
        return ServiceResult.From((IResultStatus)this);
    }
    
    public ServiceResult<T2> ToTypedServiceResult<T2>()
    {
        return ServiceResult<T2>.From(this);
    }
    
    internal static ServiceResult<T> Pass(T value)
    {
        return new ServiceResult<T>(value);
    }

    internal static ServiceResult<T> Fail(FailedOperationStatus failedOperationStatus, string? because = null)
    {
        return new ServiceResult<T>(failedOperationStatus, because);
    }
    
    internal static ServiceResult<T> From(ITypedResult<T> result)
    {
        return new ServiceResult<T>(result);
    }
    
    internal static ServiceResult<T> From(IResultStatus result)
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
    
    public static implicit operator ServiceResult<T>(UseCaseConvertible result)
    {
        return new ServiceResult<T>(result);
    }
}