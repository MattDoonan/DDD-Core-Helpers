using Core.Results.Advanced.Interfaces;
using Core.Results.Base.Enums;

namespace Core.Results.Advanced.Abstract;

public abstract class UseCaseConvertable : ResultConvertable, IUseCaseConvertable
{
    protected UseCaseConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    protected UseCaseConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }

    protected UseCaseConvertable(IUseCaseConvertable result) : base(result)
    {
    }
    
    protected UseCaseConvertable()
    {
    }
    
    public UseCaseResult ToUseCaseResult()
    {
        return UseCaseResult.Create(this);
    }
    
    public static implicit operator UseCaseResult(UseCaseConvertable result)
    {
        return result.ToUseCaseResult();
    }
}

public abstract class UseCaseConvertable<T> : ResultConvertable<T>, IUseCaseConvertable<T>
{
    protected UseCaseConvertable(T value) : base(value)
    {
    }

    protected UseCaseConvertable(IUseCaseConvertable valueResult) : base(valueResult)
    {
    }

    protected UseCaseConvertable(IUseCaseConvertable<T> valueResult) : base(valueResult)
    {
    }

    protected UseCaseConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }

    protected UseCaseConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }

    public UseCaseResult<T> ToTypedUseCaseResult()
    {
        return UseCaseResult<T>.Create(this);
    }

    public UseCaseResult ToUseCaseResult()
    {
        return UseCaseResult.Create(this);
    }
    
    public static implicit operator UseCaseResult<T>(UseCaseConvertable<T> result)
    {
        return result.ToTypedUseCaseResult();
    }
    
    public static implicit operator UseCaseResult(UseCaseConvertable<T> result)
    {
        return result.ToUseCaseResult();
    }
}