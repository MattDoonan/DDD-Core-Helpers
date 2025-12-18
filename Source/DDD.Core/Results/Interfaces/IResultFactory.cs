namespace DDD.Core.Results.Interfaces;

public interface IResultFactory<TResult> 
    where TResult : IResultStatus
{
    static abstract TResult Pass();
    static abstract TResult Fail(string because = "");
    static abstract TResult Copy(TResult result);
    static abstract TResult From(IResultStatus result);
    static abstract TResult Merge(params IResultStatus[] results);

}