using DDD.Core.Interfaces.Values;
using DDD.Core.ValueObjects.Base;
using DDD.Core.ValueObjects.SingleValueObjects.Interfaces;

namespace DDD.Core.ValueObjects.SingleValueObjects;

/// <summary>
/// Base class for single value objects
/// Contains a single value of type <typeparamref name="TValue"/>
/// </summary>
/// <param name="Value">
/// The value of the value object
/// </param>
/// <typeparam name="TValue">
/// The type of the value
/// </typeparam>
public abstract record SingleValueObject<TValue>(TValue Value) 
    : ValueObject, ISingleValueObject<TValue>, IValue<SingleValueObject<TValue>>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    /// <summary>
    /// Returns the value as a string
    /// </summary>
    /// <returns>
    /// The value as a string
    /// </returns>
    public string ValueAsString()
    {
        return Value.ToString() ?? string.Empty;
    }

    /// <summary>
    /// Compares this value object to another object
    /// </summary>
    /// <param name="obj">
    /// The object to compare to
    /// </param>
    /// <returns>
    /// An integer that indicates the relative order of the objects being compared.
    /// The return value has these meanings:
    /// Less than zero: This object is less than the <paramref name="obj"/> parameter.
    /// Zero: This object is equal to <paramref name="obj"/>.
    /// Greater than zero: This object is greater than <paramref name="obj"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="obj"/> is not of type <see cref="SingleValueObject{TValue}"/>
    /// </exception>
    public virtual int CompareTo(object? obj)
    {
        if (obj is SingleValueObject<TValue> otherValue)
        {
            return CompareTo(otherValue);
        }
        throw new ArgumentException($"Object is not of the correct type {typeof(TValue)}");
    }

    /// <summary>
    /// Compares this value object to another value object
    /// </summary>
    /// <param name="other">
    /// The other value object to compare to
    /// </param>
    /// <returns>
    /// An integer that indicates the relative order of the objects being compared.
    /// The return value has these meanings:
    /// Less than zero: This object is less than the <paramref name="other"/> parameter.
    /// Zero: This object is equal to <paramref name="other"/>.
    /// Greater than zero: This object is greater than <paramref name="other"/>.
    /// </returns>
    public virtual int CompareTo(SingleValueObject<TValue>? other)
    {
        return other is null ? 1 : Value.CompareTo(other.Value);
    }
    
    /// <summary>
    /// Less than or equal to operator
    /// </summary>
    /// <param name="a">
    /// The first value object
    /// </param>
    /// <param name="b">
    /// The second value object
    /// </param>
    /// <returns>
    /// True if the first value object is less than or equal to the second value object, false otherwise
    /// </returns>
    public static bool operator <=(SingleValueObject<TValue> a, SingleValueObject<TValue> b)
    {
        return a.Value.CompareTo(b.Value) <= 0;
    }

    /// <summary>
    /// Greater than or equal to operator
    /// </summary>
    /// <param name="a">
    /// The first value object
    /// </param>
    /// <param name="b">
    /// The second value object
    /// </param>
    /// <returns>
    /// True if the first value object is greater than or equal to the second value object, false otherwise
    /// </returns>
    public static bool operator >=(SingleValueObject<TValue> a, SingleValueObject<TValue> b)
    {
        return a.Value.CompareTo(b.Value) >= 0;
    }
    
    /// <summary>
    /// Less than operator
    /// </summary>
    /// <param name="a">
    /// The first value object
    /// </param>
    /// <param name="b">
    /// The second value object
    /// </param>
    /// <returns>
    /// True if the first value object is less than the second value object, false otherwise
    /// </returns>
    public static bool operator <(SingleValueObject<TValue> a, SingleValueObject<TValue> b)
    {
        return a.Value.CompareTo(b.Value) < 0;
    }

    /// <summary>
    /// Greater than operator
    /// </summary>
    /// <param name="a">
    /// The first value object
    /// </param>
    /// <param name="b">
    /// The second value object
    /// </param>
    /// <returns>
    /// True if the first value object is greater than the second value object, false otherwise
    /// </returns>
    public static bool operator >(SingleValueObject<TValue> a, SingleValueObject<TValue> b)
    {
        return a.Value.CompareTo(b.Value) > 0;
    }

    /// <summary>
    /// Implicit conversion operator to convert the value object to its underlying value
    /// </summary>
    /// <param name="value">
    /// The value object to convert
    /// </param>
    /// <returns>
    /// The underlying value of the value object
    /// </returns>
    public static implicit operator TValue(SingleValueObject<TValue> value)
    {
        return value.Value;
    }
}