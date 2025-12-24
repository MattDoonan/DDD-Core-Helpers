using System.Linq.Expressions;
using DDD.Core.Interfaces.Factories;
using DDD.Core.Interfaces.Values;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Core.Converters.SingleValueObjects;

/// <summary>
/// A value converter for single value objects that implement <see cref="IConvertibleFactory{TValue,T}"/>.
/// </summary>
/// <typeparam name="T">
/// The type of the single value object.
/// </typeparam>
/// <typeparam name="TValue">
/// The underlying value type.
/// </typeparam>
public class IConvertibleFactoryConverter<T, TValue> : SingleValueConverter<T, TValue>
    where T : ISingleValue<TValue>, IConvertibleFactory<TValue, T>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    private static Expression<Func<TValue, T>> ConvertFrom => x => From(x);
    
    public IConvertibleFactoryConverter(ConverterMappingHints? mappingHints = null) 
        : base(ConvertFrom, mappingHints)
    {
    }

    private static T From(TValue value)
    {
        return T.From(value);
    }
}