namespace Outputs.Results.Interfaces;

public interface IResultStatusBase<out TResult> 
    where TResult : IResultStatus
{
    static abstract TResult Pass();
    static abstract TResult Fail(string because = "");
    static abstract TResult Merge(params IResultStatus[] result);
    static abstract TResult RemoveValue(IResultStatus status);
}

