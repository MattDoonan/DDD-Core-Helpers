using Core.Entities.Regular;
using Core.Results.Base.Interfaces;
using Core.Results.Basic;

namespace Core.Interfaces;

public interface ISimpleFactory<in TIn, out TResult, T>
    where TResult : class, ITypedResult<T>
{
    static abstract TResult Create(TIn value);
}
    
public interface ISimpleEntityFactory<in TIn, T> 
    : ISimpleFactory<TIn, EntityResult<T>, T>
    where T : Entity;