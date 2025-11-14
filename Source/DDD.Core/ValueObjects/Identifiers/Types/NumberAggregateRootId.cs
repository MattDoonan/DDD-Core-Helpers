using System.Numerics;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.ValueObjects.Identifiers.Types;

public record NumberAggregateRootId<TValue, T>(TValue Value) : NumberIdentifier<TValue, T>(Value), IAggregateRootId<TValue>
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : NumberAggregateRootId<TValue, T>, ISingleValueObjectFactory<TValue, T>;