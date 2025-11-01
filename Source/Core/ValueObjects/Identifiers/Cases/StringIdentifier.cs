using Core.Interfaces;
using Core.ValueObjects.Identifiers.Base;
using Core.ValueObjects.Identifiers.Lists;
using Core.ValueObjects.Regular.Strings;

namespace Core.ValueObjects.Identifiers.Cases;

public record StringIdentifier<T>(string Value): StringValueObject<T>(Value), IIdentifier<string>
    where T : StringIdentifier<T>, ISimpleValueObjectFactory<string, T>
{
    public bool IsInList(IIdentifierList<StringIdentifier<T>> identifierList)
    {
        return identifierList.Values.Contains(this);
    }
}