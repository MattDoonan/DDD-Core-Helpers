using Base.Results.Basic;

namespace Base.ValueObjects.Regular.Base;

public interface IValueObjectFactory<TIn, TOut> 
    where TOut : class, IValueObject
{
    public TIn Value { get; }
    static abstract ValueObjectResult<TOut> Create(TIn value);
}