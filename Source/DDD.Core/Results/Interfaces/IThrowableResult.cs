using DDD.Core.Results.Exceptions;

namespace DDD.Core.Results.Interfaces;

public interface IThrowableResult
{
    void Throw(string message);

    void Throw();

    ResultException ToException(string message);

    ResultException ToException();
}