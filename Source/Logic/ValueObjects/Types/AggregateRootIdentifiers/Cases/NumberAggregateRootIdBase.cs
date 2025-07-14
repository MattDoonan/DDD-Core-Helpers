using System.Numerics;
using ValueObjects.Types.AggregateRootIdentifiers.Base;
using ValueObjects.Types.Identifiers.Cases;

namespace ValueObjects.Types.AggregateRootIdentifiers.Cases;

public class NumberAggregateRootIdBase<TValue, T>(TValue value) : NumberIdentifierBase<TValue, T>(value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IAggregateRootId<TValue, T>;