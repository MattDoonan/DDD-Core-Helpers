using Core.ValueObjects.Identifiers.Base;

namespace Core.ValueObjects.AggregateRootIdentifiers.Base;

public interface IAggregateRootId : IIdentifier;

public interface IAggregateRootId<out TValue> : IAggregateRootId, IIdentifier<TValue>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;