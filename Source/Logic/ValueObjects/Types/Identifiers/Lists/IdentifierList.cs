using Outputs.Results;
using ValueObjects.Types.Identifiers.Base;

namespace ValueObjects.Types.Identifiers.Lists;

public class IdentifierList<T>(List<T> values) : IIdentifierList<T>
    where T : class, IIdentifier
{
    public List<T> Values { get; } = values;

    public IdentifierList() : this([])
    {
    }

    public Result Add(T identifier)
    {
        var contains = Get(identifier);
        if (contains.IsSuccessful)
        {
            return Result.Fail("the identifier already exists in the list");
        }
        Values.Add(identifier);
        return Result.Pass();
    }

    public Result Remove(T identifier)
    {
       var removed = Values.Remove(identifier); 
       return removed 
           ? Result.Pass() 
           : Result.Fail("the identifier does not exist in the list");
    }

    public Result<T> Get(T identifier)
    {
        var existingItem = Values.FirstOrDefault(id => id.Equals(identifier));
        return existingItem == null
            ? Result.Fail<T>("the identifier does not exist in the list")
            : Result.Pass(existingItem);
    }

    public void OrderAsc()
    {
        Values.Sort();
    }

    public void OrderDesc()
    {
        Values.Reverse();
    }
}