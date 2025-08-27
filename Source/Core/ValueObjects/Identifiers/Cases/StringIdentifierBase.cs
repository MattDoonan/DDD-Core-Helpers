using Core.ValueObjects.Identifiers.Base;
using Core.ValueObjects.Identifiers.Lists;
using Core.ValueObjects.Regular.Strings;

namespace Core.ValueObjects.Identifiers.Cases;

public class StringIdentifierBase<T>(string value): StringValueObjectBase<T>(value), IIdentifier
    where T : class, IIdentifier<string, T>
{
    public bool IsInList(IIdentifierList<StringIdentifierBase<T>> identifierList)
    {
        return identifierList.Values.Contains(this);
    }
}