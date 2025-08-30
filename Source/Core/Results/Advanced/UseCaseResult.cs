using Core.Results.Advanced.Abstract;
using Core.Results.Advanced.Interfaces;
using Core.Results.Base.Abstract;
using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;
using Core.Results.Helpers;

namespace Core.Results.Advanced;

public class UseCaseResult : ResultConvertable, IResultFactory<UseCaseResult>
{
    private UseCaseResult()
    {
    }
    private UseCaseResult(IResultConvertable resultStatus) : base(resultStatus)
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
    
    public static UseCaseResult Merge(params ResultConvertable[] results)
    {
        return ResultCreationHelper.Merge<UseCaseResult, ResultConvertable>(results);
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
    
    internal static UseCaseResult Create(IResultConvertable result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.Unknown })
        {
            return new UseCaseResult(result)
            {
                FailedLayer = FailedLayer.UseCase
            };   
        }
        return new UseCaseResult(result);
    }
}

public class UseCaseResult<T> : ResultConvertable<T>
{
    private UseCaseResult(T value) : base(value)
    {
    }

    private UseCaseResult(FailureType failureType, string because) : base(failureType, FailedLayer.UseCase, because)
    {
    }

    private UseCaseResult(IResultConvertable<T> result) : base(result)
    {
    }
    
    private UseCaseResult(IResultConvertable result) : base(result)
    {
    }
    
    public UseCaseResult RemoveType()
    {
        return UseCaseResult.Create(this);
    }
    
    internal static UseCaseResult<T> Pass(T value)
    {
        return new UseCaseResult<T>(value);
    }

    internal static UseCaseResult<T> Fail(FailureType failureType, string because = "")
    {
        return new UseCaseResult<T>(failureType, because);
    }
    
    internal static UseCaseResult<T> Create(IResultConvertable<T> result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.Unknown })
        {
            return new UseCaseResult<T>(result)
            {
                FailedLayer = FailedLayer.UseCase
            };   
        }
        return new UseCaseResult<T>(result);
    }
    
    internal static UseCaseResult<T> Create(IResultConvertable result)
    {
        if (result is { IsFailure: true, FailedLayer: FailedLayer.Unknown })
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
    
    public static implicit operator UseCaseResult(UseCaseResult<T> result)
    {
        return result.RemoveType();
    }
    
    public static implicit operator UseCaseResult<T>(ResultConvertable result)
    {
        return new UseCaseResult<T>(result);
    }
}