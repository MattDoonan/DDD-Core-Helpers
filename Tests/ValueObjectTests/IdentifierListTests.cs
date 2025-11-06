using Core.Interfaces;
using Core.Results.Basic;
using Core.ValueObjects.Identifiers.Base;
using Core.ValueObjects.Identifiers.Lists;
using Core.ValueObjects.Regular.Base;
using Xunit;

namespace ValueObjectTests;

public class IdentifierListTests
{
    private record TestValueObjectBase : Identifier<string>, ISingleValueObjectFactory<string, TestValueObjectBase>
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
        var identifier = TestValueObjectBase.Create("A").Output;

        var result = list.Add(identifier);

        Assert.True(result.IsSuccessful);
        Assert.Contains(identifier, list.Values);
    }

    [Fact]
    public void Add_ExistingIdentifier_ShouldFail()
    {
        var list = new IdentifierList<TestValueObjectBase>();
        var identifier = TestValueObjectBase.Create("A").Output;
        list.Add(identifier);

        var result = list.Add(identifier);

        Assert.True(result.IsFailure);
        Assert.Contains("the identifier already exists in the list", result.GetErrorMessages());
    }

    [Fact]
    public void Remove_ExistingIdentifier_ShouldPass()
    {
        var list = new IdentifierList<TestValueObjectBase>();
        var identifier = TestValueObjectBase.Create("A").Output;
        list.Add(identifier);

        var result = list.Remove(identifier);

        Assert.True(result.IsSuccessful);
        Assert.DoesNotContain(identifier, list.Values);
    }

    [Fact]
    public void Remove_NonExistingIdentifier_ShouldFail()
    {
        var list = new IdentifierList<TestValueObjectBase>();
        var identifier = TestValueObjectBase.Create("A").Output;

        var result = list.Remove(identifier);

        Assert.False(result.IsSuccessful);
        Assert.Contains("the identifier does not exist in the list", result.GetErrorMessages());
    }
}