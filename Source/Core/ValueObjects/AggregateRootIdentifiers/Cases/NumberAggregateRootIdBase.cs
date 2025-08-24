using System.Numerics;
using Core.ValueObjects.AggregateRootIdentifiers.Base;
using Core.ValueObjects.Identifiers.Cases;

namespace Core.ValueObjects.AggregateRootIdentifiers.Cases;

public class NumberAggregateRootIdBase<TValue, T>(TValue value) : NumberIdentifierBase<TValue, T>(value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IAggregateRootId<TValue, T>;