using System.Numerics;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Factories;

namespace DDD.Core.ValueObjects.SingleValueObjects.Types;

/// <summary>
/// Base class for number value objects
/// </summary>
/// <param name="Value">
/// The numeric value
/// </param>
/// <typeparam name="TValue">
/// The type of the numeric value
/// </typeparam>
/// <typeparam name="T">
/// The type of the Number Value Object
/// </typeparam>
public abstract record NumberValueObject<TValue, T>(TValue Value) : SingleValueObject<TValue>(Value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : NumberValueObject<TValue, T>, ISingleValueObjectFactory<TValue, T>
{
    /// <summary>
    /// Adds two <see cref="NumberValueObject{TValue, T}"/>
    /// </summary>
    /// <param name="a">
    /// The first <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <param name="b">
    /// The second <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <returns>
    /// The result of the addition wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public static ValueObjectResult<T> operator +(NumberValueObject<TValue, T> a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a.Value + b.Value);
    }
    
    /// <summary>
    /// Subtracts two <see cref="NumberValueObject{TValue, T}"/>
    /// </summary>
    /// <param name="a">
    /// The first <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <param name="b">
    /// The second <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <returns></returns>
    public static ValueObjectResult<T> operator -(NumberValueObject<TValue, T> a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a.Value - b.Value);
    }
    
    /// <summary>
    /// Adds a <see cref="TValue"/> to a <see cref="NumberValueObject{TValue, T}"/>
    /// </summary>
    /// <param name="a">
    /// The <see cref="TValue"/>
    /// </param>
    /// <param name="b">
    /// The <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <returns>
    /// The result of the addition wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public static ValueObjectResult<T> operator +(TValue a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a + b.Value);
    }
    
    /// <summary>
    /// Subtracts a <see cref="NumberValueObject{TValue, T}"/> from a <see cref="TValue"/>
    /// </summary>
    /// <param name="a">
    /// The <see cref="TValue"/>
    /// </param>
    /// <param name="b">
    /// The <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <returns>
    /// The result of the subtraction wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public static ValueObjectResult<T> operator -(TValue a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a - b.Value);
    }
    
    /// <summary>
    /// Adds a <see cref="TValue"/> to a <see cref="NumberValueObject{TValue, T}"/>
    /// </summary>
    /// <param name="a">
    /// The <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <param name="b">
    /// The <see cref="TValue"/>
    /// </param>
    /// <returns>
    /// The result of the addition wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public static ValueObjectResult<T> operator +(NumberValueObject<TValue, T> a, TValue b)
    {
        return T.Create(a.Value + b);
    }
    
    
    /// <summary>
    /// Subtracts a <see cref="TValue"/> from a <see cref="NumberValueObject{TValue, T}"/>
    /// </summary>
    /// <param name="a">
    /// The <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <param name="b">
    /// The <see cref="TValue"/>
    /// </param>
    /// <returns>
    /// The result of the subtraction wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public static ValueObjectResult<T> operator -(NumberValueObject<TValue, T> a, TValue b)
    {
        return T.Create(a.Value - b);
    }
    
    /// <summary>
    /// Multiplies two <see cref="NumberValueObject{TValue, T}"/>
    /// </summary>
    /// <param name="a">
    /// The first <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <param name="b">
    /// The second <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <returns>
    /// The result of the multiplication wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public static ValueObjectResult<T> operator *(NumberValueObject<TValue, T> a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a.Value * b.Value);
    }
    
    /// <summary>
    /// Divides two <see cref="NumberValueObject{TValue, T}"/>
    /// </summary>
    /// <param name="a">
    /// The first <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <param name="b">
    /// The second <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <returns>
    /// The result of the division wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public static ValueObjectResult<T> operator /(NumberValueObject<TValue, T> a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a.Value / b.Value);
    }
    
    /// <summary>
    /// Multiplies a <see cref="TValue"/> with a <see cref="NumberValueObject{TValue, T}"/>
    /// </summary>
    /// <param name="a">
    /// The <see cref="TValue"/>
    /// </param>
    /// <param name="b">
    /// The <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <returns>
    /// The result of the multiplication wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public static ValueObjectResult<T> operator *(TValue a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a * b.Value);
    }
    
    /// <summary>
    /// Divides a <see cref="TValue"/> by a <see cref="NumberValueObject{TValue, T}"/>
    /// </summary>
    /// <param name="a">
    /// The <see cref="TValue"/>
    /// </param>
    /// <param name="b">
    /// The <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <returns></returns>
    public static ValueObjectResult<T> operator /(TValue a, NumberValueObject<TValue, T> b)
    {
        return T.Create(a / b.Value);
    }
    
    /// <summary>
    /// Multiplies a <see cref="NumberValueObject{TValue, T}"/> with a <see cref="TValue"/>
    /// </summary>
    /// <param name="a">
    /// The <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <param name="b">
    /// The <see cref="TValue"/>
    /// </param>
    /// <returns>
    /// The result of the multiplication wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public static ValueObjectResult<T> operator *(NumberValueObject<TValue, T> a, TValue b)
    {
        return T.Create(a.Value * b);
    }
    
    /// <summary>
    /// Divides a <see cref="NumberValueObject{TValue, T}"/> by a <see cref="TValue"/>
    /// </summary>
    /// <param name="a">
    /// The <see cref="NumberValueObject{TValue, T}"/>
    /// </param>
    /// <param name="b">
    /// The <see cref="TValue"/>
    /// </param>
    /// <returns>
    /// The result of the division wrapped in a <see cref="ValueObjectResult{T}"/>
    /// </returns>
    public static ValueObjectResult<T> operator /(NumberValueObject<TValue, T> a, TValue b)
    {
        return T.Create(a.Value / b);
    }
}