using DDD.Core.Entities;
using DDD.Core.Results;
using DDD.Core.Results.Base.Interfaces;

namespace DDD.Core.Interfaces.Factories;

public interface ISimpleFactory<in TIn, out TResult, T>
    where TResult : class, ITypedResult<T>
{
    static abstract TResult Create(TIn value);
}
    
public interface ISimpleEntityFactory<in TIn, T> 
    : ISimpleFactory<TIn, EntityResult<T>, T>
    where T : Entity;