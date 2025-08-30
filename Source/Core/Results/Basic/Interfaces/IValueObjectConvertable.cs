namespace Core.Results.Basic.Interfaces;

public interface IValueObjectConvertable<T> : IEntityConvertable<T>
{
    public ValueObjectResult<T> ToTypedValueObject();
}