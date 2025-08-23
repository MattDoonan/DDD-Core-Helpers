using Base.ObjectTypes;
using Base.Results.Basic;
using Logic.ValueObjects.Regular.Base;

namespace Logic.ValueObjects.Regular.Strings;

public interface IStringValueObject<T> : IValueObject
    where T : class, IValueObject<string, T>
{
    ValueObjectResult<T> ToLower(StringValueObjectBase<T> value);
    ValueObjectResult<T> ToUpper(StringValueObjectBase<T> value);
}