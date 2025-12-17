using DDD.Core.Results.Interfaces;

namespace DDD.Core.Results.Convertibles.Interfaces;

public interface IResultConvertible : IResultStatus
{ 
    Result ToResult();
    Result<T> ToTypedResult<T>();
}

public interface IResultConvertible<T> : IResultConvertible, ITypedResult<T>
{
    Result<T> ToTypedResult();
    
}