using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Convertibles.Interfaces;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

public class InfraResult : RepoConvertible, IResultFactory<InfraResult>
{
    private InfraResult() 
        : base(ResultLayer.Infrastructure)
    {
    }
    
    private InfraResult(IResultStatus resultStatus) 
        : base(resultStatus, ResultLayer.Infrastructure)
    {
    }
    
    private InfraResult(FailureType failureType, string? because) 
        : base(failureType, ResultLayer.Infrastructure, because)
    {
    }
    
    public InfraResult<T> ToTypedInfraResult<T>()
    {
        return InfraResult<T>.From(this);
    }
    
    public static InfraResult Pass()
    {
        return new InfraResult();
    }

    public static InfraResult Fail(string? because = null)
    {
        return new InfraResult(FailureType.Generic, because);
    }
    
    public static InfraResult Copy(InfraResult result)
    {
        return From(result);
    }
    
    public static InfraResult NotFound(string? because = null)
    {
        return new InfraResult(FailureType.NotFound, because);
    }
    
    public static InfraResult AlreadyExists(string? because = null)
    {
        return new InfraResult(FailureType.AlreadyExists, because);
    }
    
    public static InfraResult InvalidRequest(string? because = null)
    {
        return new InfraResult(FailureType.InvalidRequest, because);
    }
    
    public static InfraResult OperationTimeout(string? because = null)
    {
        return new InfraResult(FailureType.OperationTimeout, because);
    }
    
    public static InfraResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<InfraResult>();
    }
    
    public static InfraResult From(IResultStatus result)
    {
        return new InfraResult(result);
    }
    
    public static InfraResult<T> From<T>(ITypedResult<T> result)
    {
        return InfraResult<T>.From(result);
    }
    
    public static InfraResult<T> From<T>(IResultStatus result)
    {
        return InfraResult<T>.From(result);
    }
    
    public static InfraResult<T> Pass<T>(T value)
    {
        return InfraResult<T>.Pass(value);
    }
    
    public static InfraResult<T> Fail<T>(string? because = null)
    {
        return InfraResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static InfraResult<T> NotFound<T>(string? because = null)
    {
        return InfraResult<T>.Fail(FailureType.NotFound, because);
    }
    
    public static InfraResult<T> AlreadyExists<T>(string? because = null)
    {
        return InfraResult<T>.Fail(FailureType.AlreadyExists, because);
    }
    
    public static InfraResult<T> InvalidRequest<T>(string? because = null)
    {
        return InfraResult<T>.Fail(FailureType.InvalidRequest, because);
    }
    
    public static InfraResult<T> OperationTimeout<T>(string? because = null)
    {
        return InfraResult<T>.Fail(FailureType.OperationTimeout, because);
    }
    
    public static InfraResult<T> Copy<T>(InfraResult<T> result)
    {
        return InfraResult<T>.From(result);
    }
}

public class InfraResult<T> : RepoConvertible<T>
{
    private InfraResult(T value) 
        : base(value, ResultLayer.Infrastructure)
    {
    }
    
    private InfraResult(FailureType failureType, string? because) 
        : base(failureType, ResultLayer.Infrastructure, because)
    {
    }
    
    private InfraResult(ITypedResult<T> result) 
        : base(result, ResultLayer.Infrastructure)
    {
    }
    
    private InfraResult(IResultStatus result) 
        : base(result, ResultLayer.Infrastructure)
    {
    }
    
    public InfraResult RemoveType()
    {
        return InfraResult.From((IResultStatus)this);
    }
    
    public InfraResult<T2> ToTypedInfraResult<T2>()
    {
        return InfraResult<T2>.From(this);
    }
    
    internal static InfraResult<T> Pass(T value)
    {
        return new InfraResult<T>(value);
    }

    internal static InfraResult<T> Fail(FailureType failureType, string? because = null)
    {
        return new InfraResult<T>(failureType, because);
    }
    
    internal static InfraResult<T> From(ITypedResult<T> result)
    {
        return new InfraResult<T>(result);
    }
    
    internal static InfraResult<T> From(IResultStatus result)
    {
        return new InfraResult<T>(result);
    }
    
    public static implicit operator InfraResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator InfraResult(InfraResult<T> result)
    {
        return result.RemoveType();
    }
    
    public static implicit operator InfraResult<T>(RepoConvertible result)
    {
        return new InfraResult<T>(result);
    }
}