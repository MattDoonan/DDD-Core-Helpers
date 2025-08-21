using Outputs.Results.Interfaces;

namespace Outputs.Results.Abstract;

public abstract class ContentResult<T> : ResultStatus, IContentResult<T>
{
    public T Content
    {
        get
        {
            if (_content == null || !_hasContent)
            {
                throw new InvalidOperationException($"Cannot access {typeof(T).Name} Value when the result is an error" );
            }
            return _content;
        }
    }
    
    private readonly T? _content;
    private readonly bool _hasContent;
    
    protected ContentResult(T value)
    {
        _content = value;
        _hasContent  = true;
    }
    
    protected ContentResult(IContentResult<T> valueResult) : base(valueResult)
    {
        if (valueResult.IsSuccessful)
        {
            _content = valueResult.Content;
            _hasContent = true;
        }
        else
        {
            _hasContent = false;
        }
    }
    
    protected ContentResult(IResultStatus valueResult) : base(valueResult)
    {
        if (valueResult.IsSuccessful)
        {
            throw new  InvalidOperationException($"Cannot access {typeof(T).Name} Value when the result is a failure");
        }
        _hasContent = false;
    }
    
    protected ContentResult(FailureType failureType, string because) : base(failureType, failureType.ToMessage<T>(), because)
    {
        _hasContent = false;
    }

    public string GetTypeOf()
    {
        return typeof(T).Name;
    }

    public bool TryGetContent(out T content)
    {
        content = _content ?? default!;
        return _hasContent;
    }
    
    public T? Unwrap()
    {
        return _content;
    }
    
    public static implicit operator T(ContentResult<T> result)
    {
        return result.Content;
    }
}