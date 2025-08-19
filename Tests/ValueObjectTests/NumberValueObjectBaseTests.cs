using ValueObjects.Results;
using ValueObjects.Types.Regular.Base;
using ValueObjects.Types.Regular.Numbers;
using Xunit;

namespace ValueObjectTests;

public class NumberValueObjectBaseTests
{
    private class TestNumberValueObjectBase : NumberValueObjectBase<long, TestNumberValueObjectBase>, IValueObject<long ,TestNumberValueObjectBase>
    {
        private TestNumberValueObjectBase (long value) : base(value) { }
        
        public static ValueObjectResult<TestNumberValueObjectBase> Create(long value)
        {
            return new TestNumberValueObjectBase(value);
        }
    }
    
    [Fact]
    public void Add_NumberObjects_Should_ReturnSum()
    {
        var a = TestNumberValueObjectBase.Create(5).Content;
        var b = TestNumberValueObjectBase.Create(3).Content;

        var result = a + b;

        Assert.Equal(8, result.Content.Value);
    }

    [Fact]
    public void Subtract_NumberObjects_Should_ReturnDifference()
    {
        var a = TestNumberValueObjectBase.Create(10).Content;
        var b = TestNumberValueObjectBase.Create(4).Content;

        var result = a - b;

        Assert.Equal(6, result.Content.Value);
    }

    [Fact]
    public void Add_ValueAndObject_Should_ReturnSum()
    {
        var b = TestNumberValueObjectBase.Create(7).Content;
        const long a = 3;

        var result1 = a + b;
        var result2 = b + a;

        Assert.Equal(10, result1.Content.Value);
        Assert.Equal(10, result2.Content.Value);
    }

    [Fact]
    public void Subtract_ValueAndObject_Should_ReturnDifference()
    {
        var b = TestNumberValueObjectBase.Create(2).Content;
        const long a = 9;

        var result1 = a - b;
        var result2 = b - a;

        Assert.Equal(7, result1.Content.Value);
        Assert.Equal(-7, result2.Content.Value);
    }

    [Fact]
    public void Multiply_NumberObjects_Should_ReturnProduct()
    {
        var a = TestNumberValueObjectBase.Create(4).Content;
        var b = TestNumberValueObjectBase.Create(6).Content;

        var result = a * b;

        Assert.Equal(24, result.Content.Value);
    }

    [Fact]
    public void Divide_NumberObjects_Should_ReturnQuotient()
    {
        var a = TestNumberValueObjectBase.Create(20).Content;
        var b = TestNumberValueObjectBase.Create(4).Content;

        var result = a / b;

        Assert.Equal(5, result.Content.Value);
    }

    [Fact]
    public void Multiply_ValueAndObject_Should_ReturnProduct()
    {
        var b = TestNumberValueObjectBase.Create(5).Content;
        const long a = 3;

        var result1 = a * b;
        var result2 = b * a;

        Assert.Equal(15, result1.Content.Value);
        Assert.Equal(15, result2.Content.Value);
    }

    [Fact]
    public void Divide_ValueAndObject_Should_ReturnQuotient()
    {
        var b = TestNumberValueObjectBase.Create(4).Content;
        const long a = 20;

        var result1 = a / b;
        var result2 = b / a;

        Assert.Equal(5, result1.Content.Value);
        Assert.Equal(0, result2.Content.Value);
    }
}