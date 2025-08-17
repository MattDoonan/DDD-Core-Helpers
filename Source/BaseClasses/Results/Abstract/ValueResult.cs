using Outputs.Results.Interfaces;

namespace Outputs.Results.Abstract;

public abstract class ValueResult<T> : ResultStatus, IValueResult<T>
{
    public T Value
    {
        get
        {
            if (_value == null || !_hasValue)
            {
                throw new InvalidOperationException($"Cannot access {typeof(T).Name} Value when the result is an error" );
            }
            return _value;
        }
    }
    
    private readonly T? _value;
    private readonly bool _hasValue;
    
    protected ValueResult(T value)
    {
        _value = value;
        _hasValue  = true;
    }
    
    protected ValueResult(IValueResult<T> valueResult) : base(valueResult)
    {
        if (valueResult.IsSuccessful)
        {
            _value = valueResult.Value;
            _hasValue = true;
        }
        else
        {
            _hasValue = false;
        }
    }
    
    protected ValueResult(IResultStatus valueResult) : base(valueResult)
    {
        if (valueResult.IsSuccessful)
        {
            throw new  InvalidOperationException($"Cannot access {typeof(T).Name} Value when the result is a failure");
        }
        _hasValue = false;
    }
    
    protected ValueResult(FailureType failureType, string because) : base(failureType, failureType.ToMessage<T>(), because)
    {
        _hasValue = false;
    }

    public string GetTypeOf()
    {
        return typeof(T).Name;
    }
    
}