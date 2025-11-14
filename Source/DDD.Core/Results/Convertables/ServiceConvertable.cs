using DDD.Core.Results.Convertables.Interfaces;
using DDD.Core.Results.Enums;

namespace DDD.Core.Results.Convertables;

public abstract class ServiceConvertable : UseCaseConvertable, IServiceConvertable
{
    protected ServiceConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    protected ServiceConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }

    protected ServiceConvertable(IServiceConvertable result) : base(result)
    {
    }
    
    protected ServiceConvertable()
    {
    }

    public ServiceResult ToServiceResult()
    {
        return ServiceResult.From(this);
    }
    
    public ServiceResult<T> ToTypedServiceResult<T>()
    {
        return ServiceResult<T>.From(this);
    }
    
    public static implicit operator ServiceResult(ServiceConvertable result)
    {
        return result.ToServiceResult();
    }
}

public abstract class ServiceConvertable<T> : UseCaseConvertable<T>, IServiceConvertable<T>
{
    protected ServiceConvertable(T value) : base(value)
    {
    }

    protected ServiceConvertable(IServiceConvertable valueResult) : base(valueResult)
    {
    }

    protected ServiceConvertable(IServiceConvertable<T> valueResult) : base(valueResult)
    {
    }

    protected ServiceConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }

    protected ServiceConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }
    
    public ServiceResult<T2> ToTypedServiceResult<T2>()
    {
        return ServiceResult<T2>.From(this);
    }

    public ServiceResult<T> ToTypedServiceResult()
    {
        return ServiceResult<T>.From(this);
    }

    public ServiceResult ToServiceResult()
    {
        return ServiceResult.From((IUseCaseConvertable)this);
    }
    
    public static implicit operator ServiceResult<T>(ServiceConvertable<T> result)
    {
        return result.ToTypedServiceResult();
    }
    
    public static implicit operator ServiceResult(ServiceConvertable<T> result)
    {
        return result.ToServiceResult();
    }
}