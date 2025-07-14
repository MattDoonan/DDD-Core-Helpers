using System.Numerics;
using Core.Results;
using Core.ValueObjects.Types.Regular.Base;

namespace Core.ValueObjects.Types.Regular.Cases;

public interface INumberValueObject<in TValue, T> : IValueObject
    where TValue : INumber<TValue>
    where T : class, IValueObject<TValue, T>
{
    ValueObjectResult<T> Next();
    ValueObjectResult<T> Previous();
    static abstract ValueObjectResult<T> Next(TValue value);
    static abstract ValueObjectResult<T> Previous(TValue value);
}