using System.Numerics;
using Core.Interfaces;
using Core.ValueObjects.Identifiers.Base;
using Core.ValueObjects.Identifiers.Lists;
using Core.ValueObjects.Regular.Numbers;

namespace Core.ValueObjects.Identifiers.Cases;

public record NumberIdentifierBase<TValue, T>(TValue Value) : NumberValueObject<TValue, T>(Value), IIdentifier<TValue>
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : NumberIdentifierBase<TValue, T>, ISimpleValueObjectFactory<TValue, T>

{
    public bool IsInList(IIdentifierList<NumberIdentifierBase<TValue, T>> identifierList)
    {
        return identifierList.Values.Contains(this);
    }
}