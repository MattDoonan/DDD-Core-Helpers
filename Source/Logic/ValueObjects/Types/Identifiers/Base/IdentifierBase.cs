using ValueObjects.Types.Identifiers.Lists;
using ValueObjects.Types.Regular.Base;

namespace ValueObjects.Types.Identifiers.Base;

public abstract class IdentifierBase<TValue, T>(TValue value) : ValueObjectBase<TValue, T>(value), IIdentifierCommands<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>
{

    public bool IsInList(IIdentifierList<TValue, T> identifierList)
    {
        return identifierList.Get(Value).IsSuccessful;
    }
}