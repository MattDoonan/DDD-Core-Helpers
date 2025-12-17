using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Abstract;

public abstract class NonTypedResult : ResultStatus
{
    protected NonTypedResult(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(new ResultError(failureType, failedLayer, because))
    {
    }

    protected NonTypedResult(IResultStatus result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected NonTypedResult(ResultLayer resultLayer) 
        : base(resultLayer) 
    {
    }
    
}