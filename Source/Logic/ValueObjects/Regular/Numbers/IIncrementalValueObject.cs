using System.Numerics;
using Base.ObjectTypes;
using Base.Results.Basic;
using Logic.ValueObjects.Regular.Base;

namespace Logic.ValueObjects.Regular.Numbers;

public interface IIncrementalValueObject<in TValue, T> : IValueObject
    where TValue : INumber<TValue>
    where T : class, IValueObject<TValue, T>
{
    ValueObjectResult<T> Next();
    ValueObjectResult<T> Previous();
    static abstract ValueObjectResult<T> Next(TValue value);
    static abstract ValueObjectResult<T> Previous(TValue value);
}