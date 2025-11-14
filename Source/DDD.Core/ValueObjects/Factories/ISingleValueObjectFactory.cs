using DDD.Core.Interfaces.Factories;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Base;

namespace DDD.Core.ValueObjects.Factories;

public interface ISingleValueObjectFactory<in TIn, T>
    : ISimpleFactory<TIn, ValueObjectResult<T>, T>
    where T : ValueObject;