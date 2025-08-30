using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;

namespace Core.Results.Base.Abstract;

public abstract class CoreResult : ResultStatus
{
    protected CoreResult(FailureType failureType, string because) : base(failureType, failureType.ToMessage(), because)
    {
    }
    
    protected CoreResult(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, failureType.ToMessage(), because)
    {
    }

    protected CoreResult()
    {
    }

    protected CoreResult(IResultStatus result) : base(result)
    {
    }
}