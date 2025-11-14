using DDD.Core.Results.Base.Interfaces;

namespace DDD.Core.Results.Convertables.Interfaces;

public interface IResultConvertable : IResultStatus
{ 
    Result ToResult();
}

public interface IResultConvertable<T> : IResultConvertable, ITypedResult<T>
{
    Result<T> ToTypedResult();
    
}