using Core.Results.Base.Interfaces;

namespace Core.Results.Advanced.Interfaces;

public interface IResultConvertable : IResultStatus
{ 
    Result ToResult();
}

public interface IResultConvertable<T> : IResultConvertable, ITypedResult<T>
{
    Result<T> ToTypedResult();
    
}