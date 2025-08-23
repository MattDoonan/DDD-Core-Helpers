using Base.ObjectTypes;
using Base.Results.Basic;

namespace Logic.ValueObjects.Regular.Base;

public interface IValueObjectFactory<TIn, TOut> 
    where TOut : class, IValueObject
{
    public TIn Value { get; }
    static abstract ValueObjectResult<TOut> Create(TIn value);
}