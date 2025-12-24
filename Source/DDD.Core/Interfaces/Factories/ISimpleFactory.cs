using DDD.Core.Entities;
using DDD.Core.Results;
using DDD.Core.Results.Interfaces;

namespace DDD.Core.Interfaces.Factories;

/// <summary>
/// A simple factory interface for creating typed results.
/// </summary>
/// <typeparam name="TIn">
/// The type of the input value used to create the result.
/// </typeparam>
/// <typeparam name="TResult">
/// The type of the typed result to be created.
/// </typeparam>
/// <typeparam name="T">
/// The type of the value contained in the typed result.
/// </typeparam>
public interface ISimpleFactory<in TIn, out TResult, T>
    where TResult : class, ITypedResult<T>
{
    static abstract TResult Create(TIn value);
}
    
public interface ISimpleEntityFactory<in TIn, T> 
    : ISimpleFactory<TIn, EntityResult<T>, T>
    where T : Entity;