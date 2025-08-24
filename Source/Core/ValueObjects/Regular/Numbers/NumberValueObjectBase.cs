using System.Numerics;
using Core.Results.Basic;
using Core.ValueObjects.Regular.Base;

namespace Core.ValueObjects.Regular.Numbers;

public abstract class NumberValueObjectBase<TValue, T>(TValue value) : ValueObjectBase<TValue>(value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IValueObject<TValue, T>
{
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
}