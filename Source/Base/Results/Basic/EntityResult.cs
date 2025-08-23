using Outputs.ObjectTypes;
using Outputs.Results.Advanced;
using Outputs.Results.Base.Abstract;
using Outputs.Results.Base.Enums;
using Outputs.Results.Base.Interfaces;

namespace Outputs.Results.Basic;

public class EntityResult : CoreResult<EntityResult>, IResultFactory<EntityResult>
{
    private EntityResult(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private EntityResult(FailureType failureType, string because) : base(failureType, because)
    {
    }

    private EntityResult()
    {
    }
    
    public static EntityResult Pass()
    {
        return new EntityResult();
    }
    
    public static EntityResult<T> Pass<T>(T value)
        where T : IEntity
    {
        return EntityResult<T>.Pass(value);
    }

    public static EntityResult Fail(string because = "")
    {
        return new EntityResult(FailureType.Entity, because);
    }
    
    public static EntityResult<T> Fail<T>(string because = "")
        where T : IEntity
    {
        return EntityResult<T>.Fail(because);
    }
    
    public static EntityResult<T> Copy<T>(EntityResult<T> result)
        where T : IEntity
    {
        return EntityResult<T>.Create(result);
    }

    public static EntityResult RemoveValue(IResultStatus status)
    {
        return new EntityResult(status);
    }
    
    public static implicit operator MapperResult(EntityResult result)
    {
        return MapperResult.Create(result);
    }
    
    public MapperResult AsMapperResult()
    {
        return this;
    }
    
    public static implicit operator ServiceResult(EntityResult result)
    {
        return ServiceResult.Create(result);
    }
    
    public ServiceResult ToServiceResult()
    {
        return this;
    }
    
    public static implicit operator UseCaseResult(EntityResult result)
    {
        return UseCaseResult.Create(result);
    }
    
    public UseCaseResult ToUseCaseResult()
    {
        return this;
    }
}

public class EntityResult<T> : CoreResult<T, EntityResult>
    where T : IEntity
{
    private EntityResult(T value) : base(value)
    {
    }

    private EntityResult(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    private EntityResult(ITypedResult<T> result) : base(result)
    {
    }
    
    internal static EntityResult<T> Pass(T value)
    {
        return new EntityResult<T>(value);
    }

    internal static EntityResult<T> Fail(string because = "")
    {
        return new EntityResult<T>(FailureType.Entity, because);
    }
    
    internal static EntityResult<T> Create(ITypedResult<T> result)
    {
        return new EntityResult<T>(result);
    }
    
    public static implicit operator EntityResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator MapperResult<T>(EntityResult<T> result)
    {
        return MapperResult<T>.Create(result);
    }
    
    public static implicit operator MapperResult(EntityResult<T> result)
    {
        return MapperResult.Create(result);
    }
    
    public MapperResult<T> AsTypedMapperResult()
    {
        return this;
    }
    
    public MapperResult AsMapperResult()
    {
        return this;
    }
    
    public static implicit operator ServiceResult<T>(EntityResult<T> result)
    {
        return ServiceResult<T>.Create(result);
    }
    
    public static implicit operator ServiceResult(EntityResult<T> result)
    {
        return ServiceResult.Create(result);
    }
    
    public ServiceResult<T> ToServiceTypedResult()
    {
        return this;
    }
    
    public ServiceResult ToServiceResult()
    {
        return this;
    }
    
    public static implicit operator UseCaseResult<T>(EntityResult<T> result)
    {
        return UseCaseResult<T>.Create(result);
    }
    
    public static implicit operator UseCaseResult(EntityResult<T> result)
    {
        return UseCaseResult.Create(result);
    }
    
    public UseCaseResult<T> ToUseCaseTypedResult()
    {
        return this;
    }
    
    public UseCaseResult ToUseCaseResult()
    {
        return this;
    }
}

public static class EntityResultExtensions
{
    public static EntityResult<T> AsTypedEntityResult<T>(this T value)
        where T : IAggregateRoot
    {
        return value;
    }
}