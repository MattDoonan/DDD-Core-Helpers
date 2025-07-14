using System.Numerics;
using Core.ValueObjects.Types.AggregateRootIdentifiers.Base;
using Core.ValueObjects.Types.Identifiers.Cases;

namespace Core.ValueObjects.Types.AggregateRootIdentifiers.Cases;

public class NumberAggregateRootIdBase<TValue, T>(TValue value) : NumberIdentifierBase<TValue, T>(value)
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IAggregateRootId<TValue, T>;