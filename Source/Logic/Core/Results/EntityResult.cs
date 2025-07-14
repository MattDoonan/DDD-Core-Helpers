using Core.Entities.Interfaces;
using Outputs.Base;
using Outputs.Base.Interfaces;

namespace Core.Results;

public class EntityResult : ResultStatus, IResultStatusBase<EntityResult>
{
    private const string BaseErrorMessage = "Error during entity opperation";
    private EntityResult(string successLog) : base(successLog)
    {
    }

    private EntityResult(string baseMessage, string errorMessage) : base(baseMessage, errorMessage)
    {
        
    }

    public static EntityResult Pass(string successLog = "")
    {
        return new EntityResult(successLog);
    }

    public static EntityResult<T> Pass<T>(T value, string successLog = "")
        where T : class, IEntity
    {
        return EntityResult<T>.Pass(value, successLog);
    }
    
    public static EntityResult Fail(string because)
    {
        return new EntityResult(BaseErrorMessage, because);
    }
    
    public static EntityResult<T> Fail<T>(string because)
        where T : class, IEntity
    {
        return EntityResult<T>.Fail(because);
    }

    public static EntityResult AllPass(params EntityResult[] result)
    {
        var succeeded = AllSucceeded(result.Cast<IResultStatus>().ToArray());
        return succeeded 
            ? new EntityResult("All entity results were successful")
            : new EntityResult("Error while checking entity results", "not all entity results were successful");
    }

    public static EntityResult RemoveValue(IResultStatus status)
    {
        return status.Failed 
            ? new EntityResult(BaseErrorMessage, status.ErrorReason)
            : new EntityResult(status.SuccessLog);
    }
}

public class EntityResult<T> : ResultValue<T>, IResultValueBase<T, EntityResult<T>>
    where T : class, IEntity
{
    private EntityResult(T value, string successLog) : base(value, successLog)
    {
    }

    private EntityResult(string because) : base($"Error returning {typeof(T).Name} entity", because)
    {
    }

    public static EntityResult<T> Pass(T value, string successLog = "")
    {
       return new EntityResult<T>(value, successLog);
    }

    public static EntityResult<T> Fail(string because)
    {
        return new EntityResult<T>(because);
    }

    public EntityResult ToStatus()
    {
        return EntityResult.RemoveValue(this);
    }
}