using Outputs.Results.Interfaces;

namespace Outputs.Results.Abstract;

public abstract class ValueResult<T> : ResultStatus, IValueResult<T>
{
    public T Value
    {
        get
        {
            if (_value == null)
            {
                throw new InvalidOperationException($"Cannot access {typeof(T).Name} Value when the result is an error" );
            }
            return _value;
        }
    }
    
    private readonly T? _value;
    
    protected ValueResult(T value)
    {
        _value = value;
    }
    
    protected ValueResult(IValueResult<T> valueResult) : base(valueResult)
    {
        if (valueResult.IsSuccessful)
        {
            _value = valueResult.Value;
        }
    }
    
    protected ValueResult(IResultStatus valueResult) : base(valueResult)
    {
        if (valueResult.IsSuccessful)
        {
            throw new  InvalidOperationException($"Cannot access {typeof(T).Name} Value when the result is a failure");
        }
    }
    
    protected ValueResult(FailureType failureType, string because) : base(failureType, failureType.ToMessage<T>(), because)
    {
        
    }

    public string GetTypeOf()
    {
        return typeof(T).Name;
    }
    
}