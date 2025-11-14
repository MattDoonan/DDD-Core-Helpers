using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Enums;

namespace DDD.Core.Results.Base;

public abstract class NonTypedResult : ResultStatus
{
    protected NonTypedResult(FailureType failureType, string because) : base(failureType, failureType.ToMessage(), because)
    {
    }
    
    protected NonTypedResult(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, failureType.ToMessage(), because)
    {
    }

    protected NonTypedResult()
    {
    }

    protected NonTypedResult(IResultStatus result) : base(result)
    {
    }
}