using Outputs.ObjectTypes;
using Outputs.Results.Base.Abstract;
using Outputs.Results.Base.Enums;
using Outputs.Results.Base.Interfaces;
using Outputs.Results.Basic;

namespace Outputs.Results.Advanced;

public class ServiceResult : CoreResult<ServiceResult>, IResultFactory<ServiceResult>
{
     private ServiceResult(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private ServiceResult()
    {
    }
    
    private ServiceResult(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    public static ServiceResult Pass()
    {
        return new ServiceResult();
    }

    public static ServiceResult Fail(string because = "")
    {
        return new ServiceResult(FailureType.Generic, because);
    }
    
    public static ServiceResult RemoveValue(IResultStatus status)
    {
        return new ServiceResult(status);
    }
}

public class ServiceResult<T> : CoreResult<T, ServiceResult>
{
    private ServiceResult(T value) : base(value)
    {
    }
    
    private ServiceResult(FailureType failureType, string because) : base(failureType, FailedLayer.Service, because)
    {
    }
    
    
    private ServiceResult(IResultStatus result) : base(result)
    {
    }
    
    private ServiceResult(ITypedResult<T> result) : base(result)
    {
    }
    
    internal static ServiceResult<T> Pass(T value)
    {
        return new ServiceResult<T>(value);
    }

    internal static ServiceResult<T> Fail(FailureType failureType, string because = "")
    {
        return new ServiceResult<T>(failureType, because);
    }
    
    internal static ServiceResult<T> Create(ITypedResult<T> result)
    {
        return new ServiceResult<T>(result);
    }
    
    public static implicit operator ServiceResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator ServiceResult<T>(MapperResult<T> result)
    {
        return Create(result);
    }
}

public static class ServiceResultExtensions
{
    public static ServiceResult<T> AsServiceResult<T>(this T value)
    {
        return value;
    }
    
    public static ServiceResult<T> ToServiceResult<T>(this EntityResult<T> result)
        where T : IEntity
    {
        return result;
    }
    
    public static ServiceResult<T> ToServiceResult<T>(this ValueObjectResult<T> result)
        where T : IValueObject
    {
        return result;
    }
    
    public static ServiceResult<T> ToServiceResult<T>(this MapperResult<T> result)
        where T : IValueObject
    {
        return result;
    }
    
    public static ServiceResult<T> ToServiceResult<T>(this RepoResult<T> result)
        where T : IAggregateRoot
    {
        return result;
    }
}