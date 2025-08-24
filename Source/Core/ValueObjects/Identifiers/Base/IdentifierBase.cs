using Core.ValueObjects.Identifiers.Lists;
using Core.ValueObjects.Regular.Base;

namespace Core.ValueObjects.Identifiers.Base;

public abstract class IdentifierBase<TValue>(TValue value) : ValueObjectBase<TValue>(value), IIdentifier
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    public bool IsInList(IIdentifierList<IdentifierBase<TValue>> identifierList)
    {
        return identifierList.Get(this).IsSuccessful;
    }
}