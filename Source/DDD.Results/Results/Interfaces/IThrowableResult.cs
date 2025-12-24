using DDD.Core.Results.Exceptions;

namespace DDD.Core.Results.Interfaces;

public interface IThrowableResult
{
    void ThrowIfFailure();
}