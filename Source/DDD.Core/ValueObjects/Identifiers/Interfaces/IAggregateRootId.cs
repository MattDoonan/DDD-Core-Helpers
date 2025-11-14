namespace DDD.Core.ValueObjects.Identifiers.Interfaces;

public interface IAggregateRootId : IIdentifier;

public interface IAggregateRootId<out TValue> : IAggregateRootId, IIdentifier<TValue>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;