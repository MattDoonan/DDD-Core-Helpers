using System.Linq.Expressions;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.SingleValueObjects.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Core.Converters.SingleValueObjects;

internal class SingleValueObjectFactoryConverter<T, TValue> : SingleValueConverter<T, TValue>
    where T : ISingleValueObject<TValue>, ISingleValueObjectFactory<TValue, T>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    private static Expression<Func<TValue, T>> ConvertFrom => x => From(x);
    
    public SingleValueObjectFactoryConverter(ConverterMappingHints? mappingHints = null) 
        : base(ConvertFrom, mappingHints)
    {
    }

    private static T From(TValue value)
    {
        return T.Create(value).Output;
    }
}