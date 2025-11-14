namespace DDD.Core.Results.Convertables.Interfaces;

public interface IValueObjectConvertable<T> : IEntityConvertable<T>
{
    public ValueObjectResult<T> ToTypedValueObject();
}