using System.Linq.Expressions;
using DDD.Core.Interfaces.Factories;
using DDD.Core.Interfaces.Values;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Core.Converters.SingleValueObjects;

/// <summary>
/// A value converter for single value objects that implement <see cref="IConvertable{TValue,T}"/>.
/// </summary>
/// <typeparam name="T">
/// The type of the single value object.
/// </typeparam>
/// <typeparam name="TValue">
/// The underlying value type.
/// </typeparam>
public class ConvertableValueObjectConverter<T, TValue> : SingleValueConverter<T, TValue>
    where T : ISingleValue<TValue>, IConvertable<TValue, T>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    private static Expression<Func<TValue, T>> ConvertFrom => x => From(x);
    
    public ConvertableValueObjectConverter(ConverterMappingHints? mappingHints = null) 
        : base(ConvertFrom, mappingHints)
    {
    }

    private static T From(TValue value)
    {
        return T.From(value);
    }
}