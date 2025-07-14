using ValueObjects.Results;
using ValueObjects.Types.Identifiers.Base;

namespace ValueObjects.Types.Identifiers.Cases;

public abstract class GuidIdentifierBase<T>(Guid value) : IdentifierBase<Guid, T>(value), IGuiIdentifier<T>
    where T : class, IIdentifier<Guid, T>
{
    
    public ValueObjectResult<T> Create()
    {
        return T.Create(Guid.NewGuid());
    }
    
    public ValueObjectResult<T> Create(string value)
    {
        return Guid.TryParse(value, out var guid) 
            ? T.Create(guid) 
            : ValueObjectResult.Fail<T>("string failed to parse to Guid");
    }
}