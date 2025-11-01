using Core.Interfaces;
using Core.Results.Basic;
using Core.ValueObjects.Regular.Base;

namespace Core.ValueObjects.Identifiers.Base;


public interface IGuidCreatables<T>
    where T : class, IIdentifier
{
    static abstract ValueObjectResult<T> Create();
    static abstract ValueObjectResult<T> Create(string value);
}

public interface IGuidFactory<T> : IGuidCreatables<T>, ISimpleValueObjectFactory<Guid, T>
    where T : Identifier<Guid>;