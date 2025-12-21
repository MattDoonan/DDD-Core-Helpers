using System.Linq.Expressions;
using DDD.Core.Interfaces.Values;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Core.Converters.SingleValueObjects;

/// <summary>
/// A base value converter for single value objects.
/// </summary>
/// <typeparam name="T">
/// The type of the single value object.
/// </typeparam>
/// <typeparam name="TValue">
/// The underlying value type.
/// </typeparam>
public abstract class SingleValueConverter<T, TValue> : ValueConverter<T, TValue>
    where T : ISingleValue<TValue>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    private static Expression<Func<T, TValue>> ConvertTo => x => x.Value;

    public SingleValueConverter(Expression<Func<TValue, T>> convertFrom, ConverterMappingHints? mappingHints = null) 
        : base(ConvertTo, convertFrom, mappingHints)
    {
    }
}