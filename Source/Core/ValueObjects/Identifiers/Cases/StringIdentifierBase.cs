using Base.ValueObjects.Identifiers.Base;
using Base.ValueObjects.Identifiers.Lists;
using Base.ValueObjects.Regular.Strings;

namespace Base.ValueObjects.Identifiers.Cases;

public class StringIdentifierBase<T>(string value): StringValueObjectBase<T>(value), IIdentifier
    where T : class, IIdentifier<string, T>
{
    public bool IsInList(IIdentifierList<StringIdentifierBase<T>> identifierList)
    {
        return identifierList.Get(this).IsSuccessful;
    }
}