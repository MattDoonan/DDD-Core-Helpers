using System.Numerics;
using Base.Results.Basic;
using Base.ValueObjects.Regular.Base;

namespace Base.ValueObjects.Regular.Numbers;

public class IncrementalValueObject<TValue, T>(TValue value) : NumberValueObjectBase<TValue, T>(value), IIncrementalValueObject<TValue, T>
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IValueObject<TValue, T>
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