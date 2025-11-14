using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.ValueObjects.Identifiers;

public record AggregateRootId<TValue>(TValue Value) : Identifier<TValue>(Value), IAggregateRootId<TValue>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;
