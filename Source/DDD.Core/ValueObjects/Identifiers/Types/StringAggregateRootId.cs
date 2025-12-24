using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.ValueObjects.Identifiers.Types;

/// <summary>
/// String based Aggregate Root Identifier
/// </summary>
/// <param name="Value">
/// The identifier value
/// </param>
/// <typeparam name="T">
/// The type of the Aggregate Root Identifier
/// </typeparam>
public record StringAggregateRootId<T>(string Value): StringIdentifier<T>(Value), IAggregateRootId<string>
    where T : StringAggregateRootId<T>, ISingleValueObjectFactory<string, T>;