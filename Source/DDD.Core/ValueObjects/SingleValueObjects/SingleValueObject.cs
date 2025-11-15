using DDD.Core.Interfaces.Values;
using DDD.Core.ValueObjects.Base;
using DDD.Core.ValueObjects.SingleValueObjects.Interfaces;

namespace DDD.Core.ValueObjects.SingleValueObjects;

public abstract record SingleValueObject<TValue>(TValue Value) 
    : ValueObject, ISingleValueObject<TValue>, IValue<SingleValueObject<TValue>>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    public string ValueAsString()
    {
        return Value.ToString() ?? string.Empty;
    }

    public virtual int CompareTo(object? obj)
    {
        if (obj is SingleValueObject<TValue> otherValue)
        {
            return CompareTo(otherValue);
        }
        throw new ArgumentException($"Object is not of the correct type {typeof(TValue)}");
    }

    public virtual int CompareTo(SingleValueObject<TValue>? other)
    {
        return other is null ? 1 : Value.CompareTo(other.Value);
    }
    
    public static bool operator <=(SingleValueObject<TValue> a, SingleValueObject<TValue> b)
    {
        return a.Value.CompareTo(b.Value) <= 0;
    }

    public static bool operator >=(SingleValueObject<TValue> a, SingleValueObject<TValue> b)
    {
        return a.Value.CompareTo(b.Value) >= 0;
    }
    
    public static bool operator <(SingleValueObject<TValue> a, SingleValueObject<TValue> b)
    {
        return a.Value.CompareTo(b.Value) < 0;
    }

    public static bool operator >(SingleValueObject<TValue> a, SingleValueObject<TValue> b)
    {
        return a.Value.CompareTo(b.Value) > 0;
    }

    public static implicit operator TValue(SingleValueObject<TValue> value)
    {
        return value.Value;
    }
}