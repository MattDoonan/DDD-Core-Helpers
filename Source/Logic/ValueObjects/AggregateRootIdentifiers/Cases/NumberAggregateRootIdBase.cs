using System.Numerics;
using Logic.ValueObjects.AggregateRootIdentifiers.Base;
using Logic.ValueObjects.Identifiers.Cases;

namespace Logic.ValueObjects.AggregateRootIdentifiers.Cases;

public class NumberAggregateRootIdBase<TValue, T>(TValue value) : NumberIdentifierBase<TValue, T>(value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IAggregateRootId<TValue, T>;