using Core.ValueObjects.Types.Identifiers.Lists;
using Core.ValueObjects.Types.Regular.Base;

namespace Core.ValueObjects.Types.Identifiers.Base;

public abstract class IdentifierBase<TValue, T>(TValue value) : ValueObjectBase<TValue>(value), IIdentifierCommands<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>
{

    public bool IsInList(IIdentifierList<TValue, T> identifierList)
    {
        return identifierList.Get(Value).Successful;
    }
}