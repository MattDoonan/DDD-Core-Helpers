using System.Numerics;
using Core.Interfaces;
using Core.Results.Basic;
using Core.ValueObjects.Regular.Base;

namespace Core.ValueObjects.Regular.Numbers;

public abstract record NumberValueObject<TValue, T>(TValue Value) : SingleValueObject<TValue>(Value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : NumberValueObject<TValue, T>, ISimpleValueObjectFactory<TValue, T>
{
    public static ValueObjectResult<T> operator +(NumberValueObject<TValue, T> a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a.Value + b.Value);
    }
    
    public static ValueObjectResult<T> operator -(NumberValueObject<TValue, T> a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a.Value - b.Value);
    }
    
    public static ValueObjectResult<T> operator +(TValue a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a + b.Value);
    }
    
    public static ValueObjectResult<T> operator -(TValue a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a - b.Value);
    }
    
    public static ValueObjectResult<T> operator +(NumberValueObject<TValue, T> a, TValue b)
    {
        return T.Create(a.Value + b);
    }
    
    public static ValueObjectResult<T> operator -(NumberValueObject<TValue, T> a, TValue b)
    {
        return T.Create(a.Value - b);
    }
    
    public static ValueObjectResult<T> operator *(NumberValueObject<TValue, T> a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a.Value * b.Value);
    }
    
    public static ValueObjectResult<T> operator /(NumberValueObject<TValue, T> a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a.Value / b.Value);
    }
    
    public static ValueObjectResult<T> operator *(TValue a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a * b.Value);
    }
    
    public static ValueObjectResult<T> operator /(TValue a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a / b.Value);
    }
    
    public static ValueObjectResult<T> operator *(NumberValueObject<TValue, T> a, TValue b)
    {
        return T.Create(a.Value * b);
    }
    
    public static ValueObjectResult<T> operator /(NumberValueObject<TValue, T> a, TValue b)
    {
        return T.Create(a.Value / b);
    }
}