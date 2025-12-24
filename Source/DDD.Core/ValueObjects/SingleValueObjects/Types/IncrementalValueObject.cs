using System.Numerics;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Factories;

namespace DDD.Core.ValueObjects.SingleValueObjects.Types;

/// <summary>
/// Base class for incremental number value objects
/// </summary>
/// <param name="Value">
/// The numeric value
/// </param>
/// <typeparam name="TValue">
/// The type of the numeric value
/// </typeparam>
/// <typeparam name="T">
/// The type of the Incremental Value Object
/// </typeparam>
public record IncrementalValueObject<TValue, T>(TValue Value) : NumberValueObject<TValue, T>(Value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : IncrementalValueObject<TValue, T>, ISingleValueObjectFactory<TValue, T>
{
    /// <summary>
    /// Gets the next incremental value
    /// </summary>
    /// <returns>
    /// The next incremental value object wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public ValueObjectResult<T> Next()
    { 
        return T.Create(Value + TValue.One);
    }

    /// <summary>
    /// Gets the previous incremental value
    /// </summary>
    /// <returns>
    /// The previous incremental value object wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public ValueObjectResult<T> Previous()
    {
        return T.Create(Value - TValue.One);
    }
    
    /// <summary>
    /// Gets the next incremental value for a given value
    /// </summary>
    /// <param name="value">
    /// The current value
    /// </param>
    /// <returns>
    /// The next incremental value object wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public static ValueObjectResult<T> Next(TValue value)
    {
        return T.Create(value + TValue.One);
    }

    /// <summary>
    /// Gets the previous incremental value for a given value
    /// </summary>
    /// <param name="value">
    /// The current value
    /// </param>
    /// <returns>
    /// The previous incremental value object wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public static ValueObjectResult<T> Previous(TValue value)
    {
        return T.Create(value - TValue.One);
    }
}