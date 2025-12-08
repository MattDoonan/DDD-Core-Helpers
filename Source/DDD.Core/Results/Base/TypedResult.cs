using System.Diagnostics.CodeAnalysis;
using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Exceptions;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Base;

public abstract class TypedResult<T> : ResultStatus, ITypedResult<T>
{
    public T Output
    {
        get
        {
            if (_output is not null && HasOutput)
            {
                return _output;
            }
            var type = typeof(T);
            if (type.IsValueType && Nullable.GetUnderlyingType(type) is null || !HasOutput)
            {
                throw new ResultException(this, $"Cannot access the output of type {type.Name} when the result is a failure.");
            }
            return default!;
        }
    }
    public bool HasOutput { get; }

    private readonly T? _output;

    protected TypedResult(T value, ResultLayer layer) : base(layer)
    {
        _output = value;
        HasOutput  = true;
    }
    
    protected TypedResult(IResultStatus result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
        if (result.IsSuccessful)
        {
            throw new ResultException(this, "Cannot convert a successful non-typed Result to a typed Result.");
        }
        HasOutput = false;
    }

    
    protected TypedResult(ITypedResult<T> valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
        if (valueResult.IsSuccessful)
        {
            _output = valueResult.Output;
            HasOutput = true;
        }
        else
        {
            HasOutput = false;
        }
    }
    
    protected TypedResult(FailureType failureType, ResultLayer failedLayer, string? because)
        : base(new ResultError(failureType, failedLayer, because, typeof(T)))
    {
        HasOutput = false;
    }
    
    public T GetOrDefault(T defaultValue)
    {
        return _output ?? defaultValue;
    }

    public string GetOutputType()
    {
        return typeof(T).Name;
    }

    public bool TryGetOutput([NotNullWhen(true)] out T? output)
    {
        output = _output;
        return HasOutput;
    }
    
    public T? Unwrap()
    {
        return _output;
    }
    
    public IEnumerable<ResultError> GetErrorsOfType()
    {
        return GetErrorsBy(e => e.IsOfType<T>());
    }
    
    public static implicit operator T(TypedResult<T> result)
    {
        return result.Output;
    }
}