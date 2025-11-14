using DDD.Core.Results.Base.Interfaces;

namespace DDD.Core.Results.Exceptions;

public class ResultException : Exception
{
    public ResultException() { }

    public ResultException(string message) : base(message) { }

    public ResultException(string message, Exception inner) : base(message, inner) { }

    public IResultStatus? ProblematicResult { get; init; }
}