using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Abstract;

public abstract class UntypedResult : ResultStatus
{
    protected UntypedResult(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(new ResultError(failureType, failedLayer, because))
    {
    }

    protected UntypedResult(IResultStatus result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected UntypedResult(ResultLayer resultLayer) 
        : base(resultLayer) 
    {
    }
    
}