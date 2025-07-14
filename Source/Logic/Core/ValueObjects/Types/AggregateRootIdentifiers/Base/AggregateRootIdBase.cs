using Core.ValueObjects.Types.Identifiers.Base;

namespace Core.ValueObjects.Types.AggregateRootIdentifiers.Base;

public class AggregateRootIdBase<TValue, T>(TValue value) : IdentifierBase<TValue, T>(value) 
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IAggregateRootId<TValue, T>;
