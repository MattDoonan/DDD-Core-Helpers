namespace Outputs.Results.Interfaces;

public interface IContentResult<out T> : IResultStatus
{
    public T Content { get; }
}

