using DDD.Core.Results;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.ValueObjects.Factories;


/// <summary>
/// Factory interface for Value Objects based on Guid identifiers
/// </summary>
/// <typeparam name="T">
/// The type of the Value Object to be created.
/// </typeparam>
public interface IGuidCreatables<T>
    where T : IIdentifier
{
    static abstract ValueObjectResult<T> Create();
    static abstract ValueObjectResult<T> Create(string value);
}

public interface IGuidFactory<T> : IGuidCreatables<T>, ISingleValueObjectFactory<Guid, T>
    where T : IIdentifier<Guid>;