using System.Numerics;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.ValueObjects.Identifiers.Types;

/// <summary>
/// Number based Aggregate Root Identifier
/// </summary>
/// <param name="Value">
/// The identifier value
/// </param>
/// <typeparam name="TValue">
/// The underlying number type
/// </typeparam>
/// <typeparam name="T">
/// The type of the Aggregate Root Identifier
/// </typeparam>
public record NumberAggregateRootId<TValue, T>(TValue Value) : NumberIdentifier<TValue, T>(Value), IAggregateRootId<TValue>
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : NumberAggregateRootId<TValue, T>, ISingleValueObjectFactory<TValue, T>;