using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.ValueObjects.Identifiers;

/// <summary>
/// Identifier for Aggregate Roots
/// </summary>
/// <param name="Value">
/// The value of the identifier.
/// </param>
/// <typeparam name="TValue">
/// The type of the identifier value.
/// </typeparam>
public record AggregateRootId<TValue>(TValue Value) : Identifier<TValue>(Value), IAggregateRootId<TValue>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;
