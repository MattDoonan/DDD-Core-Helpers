using Core.Interfaces;
using Core.Results.Basic;

namespace Core.ValueObjects.Regular.Base;

public interface ISingleValueObjectFactory<in TIn, T>
    : ISimpleFactory<TIn, ValueObjectResult<T>, T>
    where T : ValueObject;