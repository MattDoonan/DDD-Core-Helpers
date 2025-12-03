using System.Linq.Expressions;
using DDD.Core.Interfaces.Factories;
using DDD.Core.Interfaces.Values;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Core.Converters.SingleValueObjects;

internal class ConvertableValueObjectConverter<T, TValue> : SingleValueConverter<T, TValue>
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