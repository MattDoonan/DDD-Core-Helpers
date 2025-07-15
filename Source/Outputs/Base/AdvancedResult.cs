using Outputs.Base.Interfaces;

namespace Outputs.Base;

public abstract class AdvancedResult<T, TResult> : ResultStatus, IAdvancedResult
    where TResult : IResultStatusBase<TResult>, IResultStatus
{
    public FailureType FailureType { get; }
    public string MainError => FailureType.ToMessage();

    protected AdvancedResult(string successLog) : base(successLog)
    {
    }

    protected AdvancedResult(FailureType failureType, string baseMessage, string because) : base(baseMessage, because)
    {
        FailureType = failureType;
    }
    
    
    
}