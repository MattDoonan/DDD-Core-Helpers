using DDD.Core.Results.Convertibles.Interfaces;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertibles;

public abstract class ServiceConvertible : UseCaseConvertible, IServiceConvertible
{
    
    protected ServiceConvertible(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }

    protected ServiceConvertible(IResultStatus result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected ServiceConvertible(ResultLayer layer) 
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
    
    public static implicit operator ServiceResult(ServiceConvertible result)
    {
        return result.ToServiceResult();
    }
}

public abstract class ServiceConvertible<T> : UseCaseConvertible<T>, IServiceConvertible<T>
{
    protected ServiceConvertible(T value, ResultLayer resultLayer) 
        : base(value, resultLayer)
    {
    }

    protected ServiceConvertible(IResultStatus valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected ServiceConvertible(ITypedResult<T> valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected ServiceConvertible(FailureType failureType, ResultLayer failedLayer, string? because) : base(failureType, failedLayer, because)
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
        return ServiceResult.From((IUseCaseConvertible)this);
    }
    
    public static implicit operator ServiceResult<T>(ServiceConvertible<T> result)
    {
        return result.ToTypedServiceResult();
    }
    
    public static implicit operator ServiceResult(ServiceConvertible<T> result)
    {
        return result.ToServiceResult();
    }
}