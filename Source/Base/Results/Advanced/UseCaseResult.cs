using Base.Results.Base.Abstract;
using Base.Results.Base.Enums;
using Base.Results.Base.Interfaces;

namespace Base.Results.Advanced;

public class UseCaseResult : CoreResult<UseCaseResult>, IResultFactory<UseCaseResult>
{
    private UseCaseResult(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private UseCaseResult()
    {
    }
    
    private UseCaseResult(FailureType failureType, string because) : base(failureType, FailedLayer.UseCase, because)
    {
    }

    public static UseCaseResult Pass()
    {
        return new UseCaseResult();
    }

    public static UseCaseResult Fail(string because = "")
    {
        return new UseCaseResult(FailureType.Generic, because);
    }
    
    public static UseCaseResult<T> Pass<T>(T value)
    {
        return UseCaseResult<T>.Pass(value);
    }

    public static UseCaseResult<T> Fail<T>(string because = "")
    {
        return UseCaseResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static UseCaseResult Copy(UseCaseResult result)
    {
        return Create(result);
    }
    
    public static UseCaseResult<T> Copy<T>(UseCaseResult<T> result)
    {
        return UseCaseResult<T>.Create(result);
    }
    
    internal static UseCaseResult Create(IResultStatus result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.None })
        {
            return new UseCaseResult(result)
            {
                FailedLayer = FailedLayer.UseCase
            };   
        }
        return new UseCaseResult(result);
    }
}

public class UseCaseResult<T> : CoreResult<T, UseCaseResult>
{
    private UseCaseResult(T value) : base(value)
    {
    }

    private UseCaseResult(FailureType failureType, string because) : base(failureType, FailedLayer.UseCase, because)
    {
    }

    private UseCaseResult(ITypedResult<T> result) : base(result)
    {
    }
    
    internal static UseCaseResult<T> Pass(T value)
    {
        return new UseCaseResult<T>(value);
    }

    internal static UseCaseResult<T> Fail(FailureType failureType, string because = "")
    {
        return new UseCaseResult<T>(failureType, because);
    }
    
    internal static UseCaseResult<T> Create(ITypedResult<T> result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.None })
        {
            return new UseCaseResult<T>(result)
            {
                FailedLayer = FailedLayer.UseCase
            };   
        }
        return new UseCaseResult<T>(result);
    }
    
    public static implicit operator UseCaseResult<T>(T value)
    {
        return Pass(value);
    }
}

public static class UseCaseResultExtensions
{
    public static UseCaseResult<T> AsTypedUseCaseResult<T>(this T value)
    {
        return value;
    }
}