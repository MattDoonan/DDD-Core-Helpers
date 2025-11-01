using System.Numerics;
using Core.Interfaces;
using Core.ValueObjects.Identifiers.Cases;

namespace Core.ValueObjects.AggregateRootIdentifiers.Cases;

public record NumberAggregateRootId<TValue, T>(TValue Value) : NumberIdentifier<TValue, T>(Value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : NumberAggregateRootId<TValue, T>, ISimpleValueObjectFactory<TValue, T>;