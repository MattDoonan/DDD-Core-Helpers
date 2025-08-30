using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;

namespace Core.Results.Base.Abstract;

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