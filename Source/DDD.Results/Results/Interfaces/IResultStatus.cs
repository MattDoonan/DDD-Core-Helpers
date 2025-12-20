namespace DDD.Core.Results.Interfaces;

public interface IResultStatus : IResultFailure, ILayeredResult, IResultErrorInfo, IThrowableResult
{
    public bool IsSuccessful => !IsFailure;
    
}