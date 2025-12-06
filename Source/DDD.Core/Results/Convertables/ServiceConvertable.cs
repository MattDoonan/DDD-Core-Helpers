using DDD.Core.Results.Convertables.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertables;

public abstract class ServiceConvertable : UseCaseConvertable, IServiceConvertable
{
    
    protected ServiceConvertable(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }

    protected ServiceConvertable(IServiceConvertable result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected ServiceConvertable(ResultLayer layer) 
        : base(layer)
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
    protected ServiceConvertable(T value, ResultLayer resultLayer) 
        : base(value, resultLayer)
    {
    }

    protected ServiceConvertable(IServiceConvertable valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected ServiceConvertable(IServiceConvertable<T> valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected ServiceConvertable(FailureType failureType, ResultLayer failedLayer, string? because) : base(failureType, failedLayer, because)
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