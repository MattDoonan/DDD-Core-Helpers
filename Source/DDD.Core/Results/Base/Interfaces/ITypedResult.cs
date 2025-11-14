namespace DDD.Core.Results.Base.Interfaces;

public interface ITypedResult<out T> : IResultStatus
{
    public T Output { get; }
}
