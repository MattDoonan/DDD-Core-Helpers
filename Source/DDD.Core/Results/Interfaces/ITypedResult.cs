namespace DDD.Core.Results.Interfaces;

public interface ITypedResult
{
    string GetOutputTypeName();
}

public interface ITypedResult<out T> : IResultStatus, ITypedResult
{
    public T Output { get; }
}
