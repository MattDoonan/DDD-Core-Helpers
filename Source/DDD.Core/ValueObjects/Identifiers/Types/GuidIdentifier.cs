using DDD.Core.Results;
using DDD.Core.ValueObjects.Factories;

namespace DDD.Core.ValueObjects.Identifiers.Types;

public abstract record GuidIdentifier<T>(Guid Value) : Identifier<Guid>(Value), IGuidCreatables<T>
    where T : GuidIdentifier<T>, IGuidFactory<T>
{
    
    public static ValueObjectResult<T> Create()
    {
        return T.Create(Guid.NewGuid());
    }
    
    public static ValueObjectResult<T> Create(string value)
    {
        return Guid.TryParse(value, out var guid) 
            ? T.Create(guid) 
            : ValueObjectResult.Fail<T>("string failed to parse to Guid");
    }
}