using System.Numerics;
using ValueObjects.Results;
using ValueObjects.Types.Regular.Base;

namespace ValueObjects.Types.Regular.Numbers;

public interface IIncrementalValueObject<in TValue, T> : IValueObject
    where TValue : INumber<TValue>
    where T : class, IValueObject<TValue, T>
{
    ValueObjectResult<T> Next();
    ValueObjectResult<T> Previous();
    static abstract ValueObjectResult<T> Next(TValue value);
    static abstract ValueObjectResult<T> Previous(TValue value);
}