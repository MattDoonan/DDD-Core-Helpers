using System.Diagnostics.CodeAnalysis;
using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Enums;
using DDD.Core.Results.Exceptions;

namespace DDD.Core.Results.Base;

public abstract class TypedResult<T> : ResultStatus, ITypedResult<T>
{
    public T Output
    {
        get
        {
            if (_output is not null && _hasOutput)
            {
                return _output;
            }
            var type = typeof(T);
            if (type.IsValueType && Nullable.GetUnderlyingType(type) is null || !_hasOutput)
            {
                throw new ResultException($"Cannot access the output of type {type.Name} when the result is a failure.");
            }
            return default!;
        }
    }
    
    private readonly T? _output;
    private readonly bool _hasOutput;
    
    protected TypedResult(T value)
    {
        _output = value;
        _hasOutput  = true;
    }
    
    protected TypedResult(IResultStatus result) : base(result)
    {
        if (result.IsSuccessful)
        {
            throw new ResultException("Cannot convert a successful non-typed Result to a typed Result.");
        }
        _hasOutput = false;
    }

    
    protected TypedResult(ITypedResult<T> valueResult) : base(valueResult)
    {
        if (valueResult.IsSuccessful)
        {
            _output = valueResult.Output;
            _hasOutput = true;
        }
        else
        {
            _hasOutput = false;
        }
    }
    
    protected TypedResult(FailureType failureType, string because) : base(failureType, failureType.ToMessage<T>(), because)
    {
        _hasOutput = false;
    }
    
    protected TypedResult(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, failureType.ToMessage<T>(), because)
    {
        _hasOutput = false;
    }

    public string GetOutputType()
    {
        return typeof(T).Name;
    }

    public bool TryGetOutput([NotNullWhen(true)] out T? output)
    {
        output = _output;
        return _hasOutput;
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