using DDD.Core.Results;
using DDD.Core.ValueObjects.Factories;

namespace DDD.Core.ValueObjects.SingleValueObjects.Types;

public abstract record StringValueObject<T>(string Value) : SingleValueObject<string>(Value)
    where T : StringValueObject<T>, ISingleValueObjectFactory<string, T>
{
    /// <summary>
    /// Converts the string value to lowercase.
    /// </summary>
    /// <param name="value">
    /// The <see cref="StringValueObject{T}"/> instance to convert.
    /// </param>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> containing the lowercase representation of the string value.
    /// </returns>
    public ValueObjectResult<T> ToLower(StringValueObject<T> value)
    {
        return T.Create(value.Value.ToLower());
    }
    
    /// <summary>
    /// Converts the string value to uppercase.
    /// </summary>
    /// <param name="value">
    /// The <see cref="StringValueObject{T}"/> instance to convert.
    /// </param>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> containing the uppercase representation of the string value.
    /// </returns>
    public ValueObjectResult<T> ToUpper(StringValueObject<T> value)
    {
        return T.Create(value.Value.ToUpper());
    }
    
    /// <summary>
    /// Concatenates two <see cref="StringValueObject{T}"/> instances.
    /// </summary>
    /// <param name="a">
    /// The first <see cref="StringValueObject{T}"/> instance.
    /// </param>
    /// <param name="b">
    /// The second <see cref="StringValueObject{T}"/> instance.
    /// </param>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> containing the concatenated string value.
    /// </returns>
    public static ValueObjectResult<T> operator +(StringValueObject<T> a, StringValueObject<T> b)
    {
        return T.Create(a.Value + b.Value);
    }
    
    /// <summary>
    /// Removes the value of the second <see cref="StringValueObject{T}"/> from the first.
    /// </summary>
    /// <param name="a">
    /// The first <see cref="StringValueObject{T}"/> instance.
    /// </param>
    /// <param name="b">
    /// The second <see cref="StringValueObject{T}"/> instance.
    /// </param>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> containing the modified string value.
    /// </returns>
    public static ValueObjectResult<T> operator -(StringValueObject<T> a, StringValueObject<T> b)
    {
        return T.Create(a.Value.Replace(b.Value, "").Trim());
    }
    
    /// <summary>
    /// Concatenates a <see cref="StringValueObject{T}"/> with a regular string.
    /// </summary>
    /// <param name="a">
    /// The <see cref="StringValueObject{T}"/> instance.
    /// </param>
    /// <param name="b">
    /// The regular string to concatenate.
    /// </param>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> containing the concatenated string value.
    /// </returns>
    public static ValueObjectResult<T> operator +(StringValueObject<T> a, string b)
    {
        return T.Create(a.Value + b);
    }
    
    /// <summary>
    /// Removes a regular string from a <see cref="StringValueObject{T}"/>.
    /// </summary>
    /// <param name="a">
    /// The <see cref="StringValueObject{T}"/> instance.
    /// </param>
    /// <param name="b">
    /// The regular string to remove.
    /// </param>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> containing the modified string value.
    /// </returns>
    public static ValueObjectResult<T> operator -(StringValueObject<T> a, string b)
    {
        return T.Create(a.Value.Replace(b, "").Trim());
    }
    
    /// <summary>
    /// Concatenates a regular string with a <see cref="StringValueObject{T}"/>.
    /// </summary>
    /// <param name="a">
    /// The regular string to concatenate.
    /// </param>
    /// <param name="b">
    /// The <see cref="StringValueObject{T}"/> instance.
    /// </param>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> containing the concatenated string value.
    /// </returns>
    public static ValueObjectResult<T> operator +(string a, StringValueObject<T> b)
    {
        return T.Create(a + b.Value);
    }
    
    /// <summary>
    /// Removes a <see cref="StringValueObject{T}"/> from a regular string.
    /// </summary>
    /// <param name="a">
    /// The regular string to modify.
    /// </param>
    /// <param name="b">
    /// The <see cref="StringValueObject{T}"/> instance to remove.
    /// </param>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> containing the modified string value.
    /// </returns>
    public static ValueObjectResult<T> operator -(string a, StringValueObject<T> b)
    {
        return T.Create(a.Replace(b.Value, "").Trim());
    }
}