using Base.ObjectTypes;
using Logic.ValueObjects.Identifiers.Lists;
using Logic.ValueObjects.Regular.Base;

namespace Logic.ValueObjects.Identifiers.Base;

public abstract class IdentifierBase<TValue>(TValue value) : ValueObjectBase<TValue>(value), IIdentifier
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    public bool IsInList(IIdentifierList<IdentifierBase<TValue>> identifierList)
    {
        return identifierList.Get(this).IsSuccessful;
    }
}