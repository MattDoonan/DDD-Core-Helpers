using DDD.Core.Interfaces.Values;
using DDD.Core.ValueObjects.Base.Interfaces;

namespace DDD.Core.ValueObjects.SingleValueObjects.Interfaces;

/// <summary>
/// Interface for single value objects
/// Contains a single value of type <typeparamref name="TValue"/>
/// </summary>
/// <typeparam name="TValue">
/// The type of the value
/// </typeparam>
public interface ISingleValueObject<out TValue> : ISingleValue<TValue>, IValueObject
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;
