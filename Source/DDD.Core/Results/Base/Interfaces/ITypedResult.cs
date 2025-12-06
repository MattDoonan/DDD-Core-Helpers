namespace DDD.Core.Results.Base.Interfaces;

public interface ITypedResult
{
    string GetOutputType();
}

public interface ITypedResult<out T> : IResultStatus, ITypedResult
{
    public T Output { get; }
}
