namespace DDD.Core.Extensions;

public static class QueryableExtension
{
    public static IQueryable<T> ConfigureTracking<T>(this IQueryable<T> query, bool hasTracking)
        where T : class
    {
        return hasTracking ? query.AsTracking() :  query.AsNoTracking();
        
    }
}