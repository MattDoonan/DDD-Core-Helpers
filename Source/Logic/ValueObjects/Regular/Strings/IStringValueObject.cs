using Outputs.ObjectTypes;
using Outputs.Results.Basic;
using ValueObjects.Regular.Base;

namespace ValueObjects.Regular.Strings;

public interface IStringValueObject<T> : IValueObject
    where T : class, IValueObject<string, T>
{
    ValueObjectResult<T> ToLower(StringValueObjectBase<T> value);
    ValueObjectResult<T> ToUpper(StringValueObjectBase<T> value);
}