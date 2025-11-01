using Core.ValueObjects.Identifiers.Base;

namespace Core.ValueObjects.AggregateRootIdentifiers.Base;

public record AggregateRootId<TValue>(TValue Value) : Identifier<TValue>(Value), IAggregateRootId<TValue>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;
