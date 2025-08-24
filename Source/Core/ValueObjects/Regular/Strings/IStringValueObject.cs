using Base.Results.Basic;
using Base.ValueObjects.Regular.Base;

namespace Base.ValueObjects.Regular.Strings;

public interface IStringValueObject<T> : IValueObject
    where T : class, IValueObject<string, T>
{
    ValueObjectResult<T> ToLower(StringValueObjectBase<T> value);
    ValueObjectResult<T> ToUpper(StringValueObjectBase<T> value);
}