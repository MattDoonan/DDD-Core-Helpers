using System.Numerics;
using Core.ValueObjects.Identifiers.Base;
using Core.ValueObjects.Identifiers.Lists;
using Core.ValueObjects.Regular.Numbers;

namespace Core.ValueObjects.Identifiers.Cases;

public class NumberIdentifierBase<TValue, T>(TValue value) : NumberValueObjectBase<TValue, T>(value), IIdentifier
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>

{
    public bool IsInList(IIdentifierList<NumberIdentifierBase<TValue, T>> identifierList)
    {
        return identifierList.Values.Contains(this);
    }
}