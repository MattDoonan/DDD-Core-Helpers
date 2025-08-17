using Outputs.ObjectTypes;
using Outputs.Results;
using ValueObjects.Results;

namespace ValueObjects.Types.Regular.Base;

public interface IValueObjectFactory<TIn, TOut> 
    where TOut : class, IValueObject
{
    public TIn Value { get; }
    static abstract ValueObjectResult<TOut> Create(TIn value);
}