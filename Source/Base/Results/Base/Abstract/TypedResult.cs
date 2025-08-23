using Outputs.Results.Base.Enums;
using Outputs.Results.Base.Interfaces;

namespace Outputs.Results.Base.Abstract;

public abstract class TypedResult<T> : ResultStatus, ITypedResult<T>
{
    public T Output
    {
        get
        {
            if (_output is not null && _hasGotOutput)
            {
                return _output;
            }
            var type = typeof(T);
            if (type.IsValueType && Nullable.GetUnderlyingType(type) is null || !_hasGotOutput)
            {
                throw new InvalidOperationException($"Cannot access the output of type {type.Name} when the result is a failure");
            }
            return default!;
        }
    }
    
    private readonly T? _output;
    private readonly bool _hasGotOutput;
    
    protected TypedResult(T value)
    {
        _output = value;
        _hasGotOutput  = true;
    }
    
    protected TypedResult(ITypedResult<T> valueResult) : base(valueResult)
    {
        if (valueResult.IsSuccessful)
        {
            _output = valueResult.Output;
            _hasGotOutput = true;
        }
        else
        {
            _hasGotOutput = false;
        }
    }
    
    protected TypedResult(FailureType failureType, string because) : base(failureType, failureType.ToMessage<T>(), because)
    {
        _hasGotOutput = false;
    }
    
    protected TypedResult(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, failureType.ToMessage<T>(), because)
    {
        _hasGotOutput = false;
    }

    public string GetOutputType()
    {
        return typeof(T).Name;
    }

    public bool TryGetOutput(out T output)
    {
        output = _output ?? default!;
        return _hasGotOutput;
    }
    
    public T? Unwrap()
    {
        return _output;
    }
    
    public static implicit operator T(TypedResult<T> result)
    {
        return result.Output;
    }
}