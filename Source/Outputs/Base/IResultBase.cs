namespace Outputs.Base;

public interface IResultStatusBase<TResult> 
    where TResult : IResultStatus
{
    static abstract TResult Pass(string successLog = "");
    static abstract TResult Fail(string because = "");
    static abstract TResult AllPass(params ResultStatus[] result);
}

public interface IResultValueBase<in T, out TResultValue>
    where TResultValue : IResultValue<T>
{
    static abstract TResultValue Pass(T value, string successLog = "");
    static abstract TResultValue Fail(string because = "");
}

