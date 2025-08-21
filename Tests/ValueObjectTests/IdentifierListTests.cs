using ValueObjects.Results;
using ValueObjects.Types.Identifiers.Base;
using ValueObjects.Types.Identifiers.Lists;
using Xunit;

namespace ValueObjectTests;

public class IdentifierListTests
{
    private class TestValueObjectBase : IdentifierBase<string>, IIdentifier<string, TestValueObjectBase>
    {
        private TestValueObjectBase(string value) : base(value) { }
        public static ValueObjectResult<TestValueObjectBase> Create(string value)
        {
            return new TestValueObjectBase(value);
        }
    }
    
    [Fact]
    public void Add_NewIdentifier_ShouldPass()
    {
        var list = new IdentifierList<TestValueObjectBase>();
        var identifier = TestValueObjectBase.Create("A").Content;

        var result = list.Add(identifier);

        Assert.True(result.IsSuccessful);
        Assert.Contains(identifier, list.Values);
    }

    [Fact]
    public void Add_ExistingIdentifier_ShouldFail()
    {
        var list = new IdentifierList<TestValueObjectBase>();
        var identifier = TestValueObjectBase.Create("A").Content;
        list.Add(identifier);

        var result = list.Add(identifier);

        Assert.True(result.IsFailure);
        Assert.Contains("the identifier already exists in the list", result.GetErrorMessages());
    }

    [Fact]
    public void Remove_ExistingIdentifier_ShouldPass()
    {
        var list = new IdentifierList<TestValueObjectBase>();
        var identifier = TestValueObjectBase.Create("A").Content;
        list.Add(identifier);

        var result = list.Remove(identifier);

        Assert.True(result.IsSuccessful);
        Assert.DoesNotContain(identifier, list.Values);
    }

    [Fact]
    public void Remove_NonExistingIdentifier_ShouldFail()
    {
        var list = new IdentifierList<TestValueObjectBase>();
        var identifier = TestValueObjectBase.Create("A").Content;

        var result = list.Remove(identifier);

        Assert.False(result.IsSuccessful);
        Assert.Contains("the identifier does not exist in the list", result.GetErrorMessages());
    }

    [Fact]
    public void Get_ExistingIdentifier_ShouldPass()
    {
        var list = new IdentifierList<TestValueObjectBase>();
        var identifier = TestValueObjectBase.Create("A").Content;
        list.Add(identifier);

        var result = list.Get(identifier);

        Assert.True(result.IsSuccessful);
        Assert.Equal(identifier, result.Content);
    }

    [Fact]
    public void Get_NonExistingIdentifier_ShouldFail()
    {
        var list = new IdentifierList<TestValueObjectBase>();
        var identifier = TestValueObjectBase.Create("A").Content;

        var result = list.Get(identifier);

        Assert.False(result.IsSuccessful);
        Assert.Contains("the identifier does not exist in the list", result.GetErrorMessages());
    }

    [Fact]
    public void OrderAsc_UnorderedList_ShouldBeSortedAscending()
    {
        var c = TestValueObjectBase.Create("C").Content;
        var a = TestValueObjectBase.Create("A").Content;
        var b = TestValueObjectBase.Create("B").Content;
        var list = new IdentifierList<TestValueObjectBase>(c, b, a);

        list.OrderAsc();

        Assert.Equal(new[] { a, b, c }, list.Values);
    }

    [Fact]
    public void OrderDesc_UnorderedList_ShouldBeSortedDescending()
    {
        var a = TestValueObjectBase.Create("A").Content;
        var b = TestValueObjectBase.Create("B").Content;
        var c = TestValueObjectBase.Create("C").Content;
        var list = new IdentifierList<TestValueObjectBase>(a, b, c);

        list.OrderDesc();

        Assert.Equal(new[] { c, b, a }, list.Values);
    }
    
    
    
}