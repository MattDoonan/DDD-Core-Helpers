using Core.Results;
using Core.ValueObjects.Types.Regular.Base;

namespace Core.ValueObjects.Types.Regular.Cases;

public interface IStringValueObject<T> : IValueObject
    where T : class, IValueObject<string, T>
{
    ValueObjectResult<T> ToLower(StringValueObjectBase<T> value);
    ValueObjectResult<T> ToUpper(StringValueObjectBase<T> value);
}