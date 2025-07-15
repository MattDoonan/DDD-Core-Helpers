using Outputs.Base.Interfaces;

namespace Outputs.Base;

public abstract class ResultValue<T> : ResultStatus, IResultValue<T>
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
    
    protected ResultValue(T value, string successLog) : base(successLog)
    {
        _value = value;
    }
    
    protected ResultValue(string baseMessage, string because) : base(baseMessage, because)
    {
        
    }

    public string GetTypeOf()
    {
        return typeof(T).Name;
    }
    
}