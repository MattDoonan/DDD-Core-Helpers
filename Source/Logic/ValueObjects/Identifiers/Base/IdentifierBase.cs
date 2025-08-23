using Outputs.ObjectTypes;
using ValueObjects.Identifiers.Lists;
using ValueObjects.Regular.Base;

namespace ValueObjects.Identifiers.Base;

public abstract class IdentifierBase<TValue>(TValue value) : ValueObjectBase<TValue>(value), IIdentifier
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    public bool IsInList(IIdentifierList<IdentifierBase<TValue>> identifierList)
    {
        return identifierList.Get(this).IsSuccessful;
    }
}