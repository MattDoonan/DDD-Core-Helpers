namespace Outputs.Base.Interfaces;

public interface IResultStatusBase<TResult> 
    where TResult : IResultStatus
{
    static abstract TResult Pass(string successLog = "");
    static abstract TResult Fail(string because = "");
    static abstract TResult AllPass(params TResult[] result);
    static abstract TResult RemoveValue(IResultStatus status);

}

public interface IResultValueBase<in T, out TResultValue>
    where TResultValue : IResultValue<T>
{
    static abstract TResultValue Pass(T value, string successLog = "");
    static abstract TResultValue Fail(string because = "");
}

