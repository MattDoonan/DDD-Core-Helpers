using Outputs.ObjectTypes;
using Outputs.Results;
using Outputs.Results.Basic;

namespace ValueObjects.Regular.Base;

public interface IValueObjectFactory<TIn, TOut> 
    where TOut : class, IValueObject
{
    public TIn Value { get; }
    static abstract ValueObjectResult<TOut> Create(TIn value);
}