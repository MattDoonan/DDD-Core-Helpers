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

    public static EntityResult RemoveValue(IResultStatus status)
    {
        return new EntityResult(status);
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
    
    internal static EntityResult<T> Pass(T value)
    {
        return new EntityResult<T>(value);
    }

    internal static EntityResult<T> Fail(string because = "")
    {
        return new EntityResult<T>(FailureType.Entity, because);
    }
    
    public static implicit operator EntityResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator ServiceResult<T>(EntityResult<T> result)
    {
        return ServiceResult<T>.Create(result);
    }
    
    public static implicit operator MapperResult<T>(EntityResult<T> result)
    {
        return MapperResult<T>.Create(result);
    }
}

public static class EntityResultExtensions
{
    public static EntityResult<T> AsEntityResult<T>(this T value)
        where T : IAggregateRoot
    {
        return value;
    }
}