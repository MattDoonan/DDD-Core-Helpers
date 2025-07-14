using ValueObjects.Types.Identifiers.Base;

namespace ValueObjects.Types.AggregateRootIdentifiers.Base;

public interface IAggregateRootId : IIdentifier;

public interface IAggregateRootId<TValue, T> : IAggregateRootId, IIdentifier<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IAggregateRootId<TValue, T>;