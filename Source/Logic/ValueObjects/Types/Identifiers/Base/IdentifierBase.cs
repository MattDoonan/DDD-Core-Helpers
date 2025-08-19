using ValueObjects.Types.Identifiers.Lists;
using ValueObjects.Types.Regular.Base;

namespace ValueObjects.Types.Identifiers.Base;

public abstract class IdentifierBase<TValue, T>(TValue value) : ValueObjectBase<TValue>(value), IIdentifier
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>
{
    public bool IsInList(IIdentifierList<IdentifierBase<TValue, T>> identifierList)
    {
        return identifierList.Get(this).IsSuccessful;
    }
}