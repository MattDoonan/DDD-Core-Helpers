using Outputs.Results.Base.Abstract;
using Outputs.Results.Base.Enums;
using Outputs.Results.Base.Interfaces;

namespace Outputs.Results.Advanced;

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
    
    internal static UseCaseResult Create(IResultStatus result)
    {
        if (result.FailedLayer == FailedLayer.None)
        {
            return new UseCaseResult(result)
            {
                FailedLayer = FailedLayer.UseCase
            };   
        }
        return new UseCaseResult(result);
    }
}

public class UseCaseResult<T> : CoreResult<T, ServiceResult>
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
        if (result.FailedLayer == FailedLayer.None)
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