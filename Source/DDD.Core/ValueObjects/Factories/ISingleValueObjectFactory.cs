using DDD.Core.Interfaces.Factories;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Base.Interfaces;

namespace DDD.Core.ValueObjects.Factories;

public interface ISingleValueObjectFactory<in TIn, T>
    : ISimpleFactory<TIn, ValueObjectResult<T>, T>
    where T : IValueObject;