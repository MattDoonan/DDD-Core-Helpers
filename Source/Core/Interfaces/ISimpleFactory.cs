using Core.Entities.Regular;
using Core.Results.Base.Interfaces;
using Core.Results.Basic;
using Core.ValueObjects.Regular.Base;

namespace Core.Interfaces;

public interface ISimpleFactory<in TIn, out TResult, T>
    where TResult : class, ITypedResult<T>
{
    static abstract TResult Create(TIn value);
}

public interface ISimpleValueObjectFactory<in TIn, T>
    : ISimpleFactory<TIn, ValueObjectResult<T>, T>
    where T : ValueObject;
    
    
public interface ISimpleEntityFactory<in TIn, T> 
    : ISimpleFactory<TIn, EntityResult<T>, T>
    where T : Entity;