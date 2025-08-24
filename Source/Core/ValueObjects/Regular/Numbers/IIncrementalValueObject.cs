using System.Numerics;
using Base.Results.Basic;
using Base.ValueObjects.Regular.Base;

namespace Base.ValueObjects.Regular.Numbers;

public interface IIncrementalValueObject<in TValue, T> : IValueObject
    where TValue : INumber<TValue>
    where T : class, IValueObject<TValue, T>
{
    ValueObjectResult<T> Next();
    ValueObjectResult<T> Previous();
    static abstract ValueObjectResult<T> Next(TValue value);
    static abstract ValueObjectResult<T> Previous(TValue value);
}