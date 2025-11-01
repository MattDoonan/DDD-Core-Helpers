using Core.ValueObjects.Identifiers.Lists;
using Core.ValueObjects.Regular.Base;

namespace Core.ValueObjects.Identifiers.Base;

public abstract record Identifier<TValue>(TValue Value) : SingleValueObject<TValue>(Value), IIdentifier<TValue>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    public bool IsInList(IIdentifierList<Identifier<TValue>> identifierList)
    {
        return identifierList.Values.Contains(this);
    }
}