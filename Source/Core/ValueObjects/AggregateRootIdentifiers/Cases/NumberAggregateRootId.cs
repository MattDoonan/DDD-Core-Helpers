using System.Numerics;
using Core.Interfaces;
using Core.ValueObjects.AggregateRootIdentifiers.Base;
using Core.ValueObjects.Identifiers.Cases;
using Core.ValueObjects.Regular.Base;

namespace Core.ValueObjects.AggregateRootIdentifiers.Cases;

public record NumberAggregateRootId<TValue, T>(TValue Value) : NumberIdentifier<TValue, T>(Value), IAggregateRootId<TValue>
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : NumberAggregateRootId<TValue, T>, ISingleValueObjectFactory<TValue, T>;