using DDD.Core.Lists.Interfaces;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Interfaces;
using DDD.Core.ValueObjects.SingleValueObjects.Types;

namespace DDD.Core.ValueObjects.Identifiers.Types;

/// <summary>
/// String based Identifier Value Object
/// </summary>
/// <param name="Value">
/// The string value of the identifier
/// </param>
/// <typeparam name="T">
/// The type of the Identifier
/// </typeparam>
public record StringIdentifier<T>(string Value): StringValueObject<T>(Value), IIdentifier<string>
    where T : StringIdentifier<T>, ISingleValueObjectFactory<string, T>
{
    public bool IsInList(IIdentifierList<StringIdentifier<T>> identifierList)
    {
        return identifierList.Values.Contains(this);
    }
}