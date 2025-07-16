namespace Outputs.Base.Interfaces;

public interface IResultStatusBase<out TResult> 
    where TResult : IResultStatus
{
    static abstract TResult Pass(string successLog = "");
    static abstract TResult Fail(string because = "");
    static abstract TResult Merge(params IResultStatus[] result);
    static abstract TResult RemoveValue(IResultStatus status);
}

