using DDD.Core.Lists;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Interfaces;
using DDD.Core.ValueObjects.SingleValueObjects.Types;

namespace DDD.Core.ValueObjects.Identifiers.Types;

public record StringIdentifier<T>(string Value): StringValueObject<T>(Value), IIdentifier<string>
    where T : StringIdentifier<T>, ISingleValueObjectFactory<string, T>
{
    public bool IsInList(IIdentifierList<StringIdentifier<T>> identifierList)
    {
        return identifierList.Values.Contains(this);
    }
}