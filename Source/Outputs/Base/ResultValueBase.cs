namespace Outputs.Base;

public abstract class ResultValueBase<T> : ResultStatus, IResultValue<T>
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
    
    protected ResultValueBase(T value, string successLog) : base(successLog)
    {
        _value = value;
    }
    
    protected ResultValueBase(string baseMessage, string because) : base(baseMessage, because)
    {
        
    }
    
}