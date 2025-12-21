using DDD.Core.Lists.Interfaces;
using DDD.Core.ValueObjects.Identifiers.Interfaces;
using DDD.Core.ValueObjects.SingleValueObjects;

namespace DDD.Core.ValueObjects.Identifiers;

/// <summary>
/// Base class for identifiers
/// </summary>
/// <param name="Value">
/// The value of the identifier
/// </param>
/// <typeparam name="TValue">
/// The type of the value
/// </typeparam>
public abstract record Identifier<TValue>(TValue Value) : SingleValueObject<TValue>(Value), IIdentifier<TValue>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    public bool IsInList(IIdentifierList<Identifier<TValue>> identifierList)
    {
        return identifierList.Values.Contains(this);
    }
}