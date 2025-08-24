using System.Numerics;
using Base.ValueObjects.AggregateRootIdentifiers.Base;
using Base.ValueObjects.Identifiers.Cases;

namespace Base.ValueObjects.AggregateRootIdentifiers.Cases;

public class NumberAggregateRootIdBase<TValue, T>(TValue value) : NumberIdentifierBase<TValue, T>(value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IAggregateRootId<TValue, T>;