using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Abstract;

public abstract class UntypedResult : ResultStatus
{
    protected UntypedResult(ResultError error) 
        : base(error)
    {
    }

    protected UntypedResult(IResultStatus result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected UntypedResult(ResultLayer resultLayer) 
        : base(OperationStatus.Success(), resultLayer) 
    {
    }
    
}