using Outputs.ObjectTypes;
using ValueObjects.Identifiers.Base;

namespace ValueObjects.AggregateRootIdentifiers.Base;

public interface IAggregateRootId<TValue, T> : IAggregateRootId, IIdentifier<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IAggregateRootId<TValue, T>;