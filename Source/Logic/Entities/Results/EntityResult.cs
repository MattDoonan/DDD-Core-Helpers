using Entities.Types.Regular;
using Outputs.Results.Abstract;
using Outputs.Results.Interfaces;

namespace Entities.Results;

public class EntityResult : BasicResult<EntityResult>, IResultStatusBase<EntityResult>
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

public class EntityResult<T> :  BasicContentResultBase<T, EntityResult>
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
}