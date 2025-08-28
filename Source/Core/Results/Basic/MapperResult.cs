using Core.Results.Advanced;
using Core.Results.Base.Abstract;
using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;

namespace Core.Results.Basic;

public class MapperResult : CoreResult<MapperResult>, IResultFactory<MapperResult>
{
    private MapperResult(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private MapperResult(FailureType failureType, string because) : base(failureType, because)
    {
    }

    private MapperResult()
    {
    }

    public static MapperResult Pass()
    {
        return new MapperResult();
    }
    
    public static MapperResult<T> Pass<T>(T value)
    {
        return MapperResult<T>.Pass(value);
    }

    public static MapperResult Fail(string because = "")
    {
        return new MapperResult(FailureType.Generic, because);
    }
    
    public static MapperResult<T> Fail<T>(string because = "")
    {
        return MapperResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static MapperResult DomainViolation(string because = "")
    {
        return new MapperResult(FailureType.DomainViolation, because);
    }
    
    public static MapperResult<T> DomainViolation<T>(string because = "")
    {
        return MapperResult<T>.Fail(FailureType.DomainViolation, because);
    }
    
    public static MapperResult InvalidInput(string because = "")
    {
        return new MapperResult(FailureType.InvalidInput, because);
    }
    
    public static MapperResult<T> InvalidInput<T>(string because = "")
    {
        return MapperResult<T>.Fail(FailureType.InvalidInput, because);
    }
    
    public static MapperResult<T> Copy<T>(MapperResult<T> result)
    {
        return MapperResult<T>.Create(result);
    }
    
    public static MapperResult Copy(MapperResult result)
    {
        return Create(result);
    }

    internal static MapperResult Create(IResultStatus status)
    {
        return new MapperResult(status);
    }
    
    public static implicit operator InfraResult(MapperResult result)
    {
        return InfraResult.Create(result);
    }
    
    public InfraResult ToInfraResult()
    {
        return this;
    }
    
    public static implicit operator ServiceResult(MapperResult result)
    {
        return ServiceResult.Create(result);
    }
    
    public ServiceResult ToServiceResult()
    {
        return this;
    }
    
    public static implicit operator UseCaseResult(MapperResult result)
    {
        return UseCaseResult.Create(result);
    }
    
    public UseCaseResult ToUseCaseResult()
    {
        return this;
    }
}

public class MapperResult<T> : CoreResult<T, MapperResult>
{
    private MapperResult(T value) : base(value)
    {
    }

    private MapperResult(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    private MapperResult(ITypedResult<T> result) : base(result)
    {
    }

    internal static MapperResult<T> Pass(T value)
    {
        return new MapperResult<T>(value);
    }

    internal static MapperResult<T> Fail(FailureType failureType, string because = "")
    {
        return new MapperResult<T>(failureType, because);
    }
    
    internal static MapperResult<T> Create(ITypedResult<T> result)
    {
        return new MapperResult<T>(result);
    }
    
    public static implicit operator MapperResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator InfraResult<T>(MapperResult<T> result)
    {
        return InfraResult<T>.Create(result);
    }
    
    public static implicit operator InfraResult(MapperResult<T> result)
    {
        return InfraResult.Create(result);
    }
    
    public InfraResult<T> ToTypedInfraResult()
    {
        return this;
    }
    
    public InfraResult ToInfraResult()
    {
        return this;
    }
    
    public static implicit operator ServiceResult(MapperResult<T> result)
    {
        return ServiceResult.Create(result);
    }
    
    public static implicit operator ServiceResult<T>(MapperResult<T> result)
    {
        return ServiceResult<T>.Create(result);
    }
    
    public ServiceResult<T> ToTypedServiceResult()
    {
        return this;
    }
    
    public ServiceResult ToServiceResult()
    {
        return this;
    }
    
    public static implicit operator UseCaseResult<T>(MapperResult<T> result)
    {
        return UseCaseResult<T>.Create(result);
    }
    
    public static implicit operator UseCaseResult(MapperResult<T> result)
    {
        return UseCaseResult.Create(result);
    }
    
    public UseCaseResult<T> ToTypedUseCaseResult()
    {
        return this;
    }
    
    public UseCaseResult ToUseCaseResult()
    {
        return this;
    }
}

public static class MapperResultExtensions
{
    public static MapperResult<T> AsTypedMapperResult<T>(this T value)
    {
        return value;
    }
}