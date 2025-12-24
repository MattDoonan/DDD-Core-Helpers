using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.ValueObjects.Identifiers.Types;

/// <summary>
/// Base class for all Guid based Aggregate Root Identifiers
/// </summary>
/// <param name="Value">
/// The value of the identifier.
/// </param>
/// <typeparam name="T">
/// The type of the aggregate root identifier.
/// </typeparam>
public record GuidAggregateRootId<T>(Guid Value) : GuidIdentifier<T>(Value), IAggregateRootId<Guid>
    where T : GuidAggregateRootId<T>, IGuidFactory<T>
{
    
}