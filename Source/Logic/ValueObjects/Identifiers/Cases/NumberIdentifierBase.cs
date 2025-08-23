using System.Numerics;
using Outputs.ObjectTypes;
using ValueObjects.Identifiers.Base;
using ValueObjects.Identifiers.Lists;
using ValueObjects.Regular.Numbers;

namespace ValueObjects.Identifiers.Cases;

public class NumberIdentifierBase<TValue, T>(TValue value) : NumberValueObjectBase<TValue, T>(value), IIdentifier
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>

{
    public bool IsInList(IIdentifierList<NumberIdentifierBase<TValue, T>> identifierList)
    {
        return identifierList.Get(this).IsSuccessful;
    }
}