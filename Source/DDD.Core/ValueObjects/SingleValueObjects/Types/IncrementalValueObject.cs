using System.Numerics;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Factories;

namespace DDD.Core.ValueObjects.SingleValueObjects.Types;

public record IncrementalValueObject<TValue, T>(TValue Value) : NumberValueObject<TValue, T>(Value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : IncrementalValueObject<TValue, T>, ISingleValueObjectFactory<TValue, T>
{
    public ValueObjectResult<T> Next()
    { 
        return T.Create(Value + TValue.One);
    }

    public ValueObjectResult<T> Previous()
    {
        return T.Create(Value - TValue.One);
    }
    
    public static ValueObjectResult<T> Next(TValue value)
    {
        return T.Create(value + TValue.One);
    }

    public static ValueObjectResult<T> Previous(TValue value)
    {
        return T.Create(value - TValue.One);
    }
}