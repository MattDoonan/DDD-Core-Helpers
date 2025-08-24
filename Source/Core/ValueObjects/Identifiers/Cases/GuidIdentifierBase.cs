using Core.Results.Basic;
using Core.ValueObjects.Identifiers.Base;

namespace Core.ValueObjects.Identifiers.Cases;

public abstract class GuidIdentifierBase<T>(Guid value) : IdentifierBase<Guid>(value), IGuiIdentifier<T>
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