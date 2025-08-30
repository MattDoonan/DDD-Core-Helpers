using Core.Results.Advanced.Interfaces;
using Core.Results.Base.Enums;

namespace Core.Results.Advanced.Abstract;

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
        return ServiceResult.Create(this);
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

    public ServiceResult<T> ToTypedServiceResult()
    {
        return ServiceResult<T>.Create(this);
    }

    public ServiceResult ToServiceResult()
    {
        return ServiceResult.Create(this);
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