using System.Numerics;
using DDD.Core.Lists.Interfaces;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Interfaces;
using DDD.Core.ValueObjects.SingleValueObjects.Types;

namespace DDD.Core.ValueObjects.Identifiers.Types;

/// <summary>
/// Number based Identifier
/// </summary>
/// <param name="Value">
/// The identifier value
/// </param>
/// <typeparam name="TValue">
/// The underlying number type
/// </typeparam>
/// <typeparam name="T">
/// The type of the Identifier
/// </typeparam>
public record NumberIdentifier<TValue, T>(TValue Value) : NumberValueObject<TValue, T>(Value), IIdentifier<TValue>
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : NumberIdentifier<TValue, T>, ISingleValueObjectFactory<TValue, T>

{
    public bool IsInList(IIdentifierList<NumberIdentifier<TValue, T>> identifierList)
    {
        return identifierList.Values.Contains(this);
    }
}