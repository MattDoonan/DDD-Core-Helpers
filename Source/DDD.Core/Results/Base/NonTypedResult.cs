using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Base;

public abstract class NonTypedResult : ResultStatus
{
    protected NonTypedResult(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
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