using System.Numerics;
using ValueObjects.Results;
using ValueObjects.Types.Regular.Base;

namespace ValueObjects.Types.Regular.Numbers;

public abstract class NumberValueObjectBase<TValue, T>(TValue value) : ValueObjectBase<TValue, T>(value)
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
    
    public static ValueObjectResult<T> operator +(NumberValueObjectBase<TValue, T> a, NumberValueObjectBase<TValue, T> b)
    {
        return T.Create(a.Value + b.Value);
    }
    
    public static ValueObjectResult<T> operator -(NumberValueObjectBase<TValue, T> a, NumberValueObjectBase<TValue, T> b)
    {
        return T.Create(a.Value - b.Value);
    }
    
    public static ValueObjectResult<T> operator +(TValue a, NumberValueObjectBase<TValue, T> b)
    {
        return T.Create(a + b.Value);
    }
    
    public static ValueObjectResult<T> operator -(TValue a, NumberValueObjectBase<TValue, T> b)
    {
        return T.Create(a - b.Value);
    }
    
    public static ValueObjectResult<T> operator +(NumberValueObjectBase<TValue, T> a, TValue b)
    {
        return T.Create(a.Value + b);
    }
    
    public static ValueObjectResult<T> operator -(NumberValueObjectBase<TValue, T> a, TValue b)
    {
        return T.Create(a.Value - b);
    }
    
    public static ValueObjectResult<T> operator *(NumberValueObjectBase<TValue, T> a, NumberValueObjectBase<TValue, T> b)
    {
        return T.Create(a.Value * b.Value);
    }
    
    public static ValueObjectResult<T> operator /(NumberValueObjectBase<TValue, T> a, NumberValueObjectBase<TValue, T> b)
    {
        return T.Create(a.Value / b.Value);
    }
    
    public static ValueObjectResult<T> operator *(TValue a, NumberValueObjectBase<TValue, T> b)
    {
        return T.Create(a * b.Value);
    }
    
    public static ValueObjectResult<T> operator /(TValue a, NumberValueObjectBase<TValue, T> b)
    {
        return T.Create(a / b.Value);
    }
    
    public static ValueObjectResult<T> operator *(NumberValueObjectBase<TValue, T> a, TValue b)
    {
        return T.Create(a.Value * b);
    }
    
    public static ValueObjectResult<T> operator /(NumberValueObjectBase<TValue, T> a, TValue b)
    {
        return T.Create(a.Value / b);
    }
    
    public static bool operator <=(NumberValueObjectBase<TValue, T> a, NumberValueObjectBase<TValue, T> b)
    {
        return a.Value <= b.Value;
    }

    public static bool operator >=(NumberValueObjectBase<TValue, T> a, NumberValueObjectBase<TValue, T> b)
    {
        return a.Value >= b.Value;
    }
    
    public static bool operator <(NumberValueObjectBase<TValue, T> a, NumberValueObjectBase<TValue, T> b)
    {
        return a.Value <= b.Value;
    }

    public static bool operator >(NumberValueObjectBase<TValue, T> a, NumberValueObjectBase<TValue, T> b)
    {
        return a.Value >= b.Value;
    }
}