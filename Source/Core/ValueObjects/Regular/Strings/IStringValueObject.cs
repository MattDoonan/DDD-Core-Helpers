using Core.Results.Basic;
using Core.ValueObjects.Regular.Base;

namespace Core.ValueObjects.Regular.Strings;

public interface IStringValueObject<T> : IValueObject
    where T : class, IValueObject<string, T>
{
    ValueObjectResult<T> ToLower(StringValueObjectBase<T> value);
    ValueObjectResult<T> ToUpper(StringValueObjectBase<T> value);
}