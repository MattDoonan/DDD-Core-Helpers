using System.Numerics;
using Core.Interfaces;
using Core.ValueObjects.Identifiers.Cases;

namespace Core.ValueObjects.AggregateRootIdentifiers.Cases;

public record NumberAggregateRootIdBase<TValue, T>(TValue Value) : NumberIdentifierBase<TValue, T>(Value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : NumberAggregateRootIdBase<TValue, T>, ISimpleValueObjectFactory<TValue, T>;