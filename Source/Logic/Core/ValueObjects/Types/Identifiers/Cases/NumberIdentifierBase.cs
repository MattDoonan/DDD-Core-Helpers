using System.Numerics;
using Core.ValueObjects.Types.Identifiers.Base;
using Core.ValueObjects.Types.Identifiers.Lists;
using Core.ValueObjects.Types.Regular.Cases;

namespace Core.ValueObjects.Types.Identifiers.Cases;

public class NumberIdentifierBase<TValue, T>(TValue value) : NumberValueObjectBase<TValue, T>(value), IIdentifierCommands<TValue, T>
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>

{
    public bool IsInList(IIdentifierList<TValue, T> identifierList)
    {
        return identifierList.Get(Value).Successful;
    }
}