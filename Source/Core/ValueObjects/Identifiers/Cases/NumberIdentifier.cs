using System.Numerics;
using Core.Interfaces;
using Core.ValueObjects.Identifiers.Base;
using Core.ValueObjects.Identifiers.Lists;
using Core.ValueObjects.Regular.Numbers;

namespace Core.ValueObjects.Identifiers.Cases;

public record NumberIdentifier<TValue, T>(TValue Value) : NumberValueObject<TValue, T>(Value), IIdentifier<TValue>
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : NumberIdentifier<TValue, T>, ISimpleValueObjectFactory<TValue, T>

{
    public bool IsInList(IIdentifierList<NumberIdentifier<TValue, T>> identifierList)
    {
        return identifierList.Values.Contains(this);
    }
}