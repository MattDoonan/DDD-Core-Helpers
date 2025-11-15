using DDD.Core.Lists.Interfaces;
using DDD.Core.ValueObjects.Identifiers.Interfaces;
using DDD.Core.ValueObjects.SingleValueObjects;

namespace DDD.Core.ValueObjects.Identifiers;

public abstract record Identifier<TValue>(TValue Value) : SingleValueObject<TValue>(Value), IIdentifier<TValue>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    public bool IsInList(IIdentifierList<Identifier<TValue>> identifierList)
    {
        return identifierList.Values.Contains(this);
    }
}