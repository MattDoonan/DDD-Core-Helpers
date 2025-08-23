using Base.ObjectTypes;
using Logic.ValueObjects.Identifiers.Base;

namespace Logic.ValueObjects.AggregateRootIdentifiers.Base;

public interface IAggregateRootId<TValue, T> : IAggregateRootId, IIdentifier<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IAggregateRootId<TValue, T>;