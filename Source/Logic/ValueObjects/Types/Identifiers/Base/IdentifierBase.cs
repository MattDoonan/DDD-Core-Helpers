using ValueObjects.Types.Identifiers.Lists;
using ValueObjects.Types.Regular.Base;

namespace ValueObjects.Types.Identifiers.Base;

public abstract class IdentifierBase<TValue>(TValue value) : ValueObjectBase<TValue>(value), IIdentifier
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    public bool IsInList(IIdentifierList<IdentifierBase<TValue>> identifierList)
    {
        return identifierList.Get(this).IsSuccessful;
    }
}