using System.Numerics;
using ValueObjects.AggregateRootIdentifiers.Base;
using ValueObjects.Identifiers.Cases;

namespace ValueObjects.AggregateRootIdentifiers.Cases;

public class NumberAggregateRootIdBase<TValue, T>(TValue value) : NumberIdentifierBase<TValue, T>(value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IAggregateRootId<TValue, T>;