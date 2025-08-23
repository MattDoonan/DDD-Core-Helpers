using System.Numerics;
using Outputs.ObjectTypes;
using Outputs.Results;
using Outputs.Results.Basic;
using ValueObjects.Regular.Base;

namespace ValueObjects.Regular.Numbers;

public interface IIncrementalValueObject<in TValue, T> : IValueObject
    where TValue : INumber<TValue>
    where T : class, IValueObject<TValue, T>
{
    ValueObjectResult<T> Next();
    ValueObjectResult<T> Previous();
    static abstract ValueObjectResult<T> Next(TValue value);
    static abstract ValueObjectResult<T> Previous(TValue value);
}