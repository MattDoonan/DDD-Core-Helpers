using Core.Interfaces;
using Core.Results.Basic;
using Core.ValueObjects.Regular.Base;
using Core.ValueObjects.Regular.Numbers;
using Xunit;

namespace ValueObjectTests;

public class NumberValueObjectBaseTests
{
    private record TestNumberValueObjectBase 
        : NumberValueObject<long, TestNumberValueObjectBase>, ISimpleValueObjectFactory<long, TestNumberValueObjectBase>
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
        var a = TestNumberValueObjectBase.Create(5).Output;
        var b = TestNumberValueObjectBase.Create(3).Output;

        var result = a + b;

        Assert.Equal(8, result.Output.Value);
    }

    [Fact]
    public void Subtract_NumberObjects_Should_ReturnDifference()
    {
        var a = TestNumberValueObjectBase.Create(10).Output;
        var b = TestNumberValueObjectBase.Create(4).Output;

        var result = a - b;

        Assert.Equal(6, result.Output.Value);
    }

    [Fact]
    public void Add_ValueAndObject_Should_ReturnSum()
    {
        var b = TestNumberValueObjectBase.Create(7).Output;
        const long a = 3;

        var result1 = a + b;
        var result2 = b + a;

        Assert.Equal(10, result1.Output.Value);
        Assert.Equal(10, result2.Output.Value);
    }

    [Fact]
    public void Subtract_ValueAndObject_Should_ReturnDifference()
    {
        var b = TestNumberValueObjectBase.Create(2).Output;
        const long a = 9;

        var result1 = a - b;
        var result2 = b - a;

        Assert.Equal(7, result1.Output.Value);
        Assert.Equal(-7, result2.Output.Value);
    }

    [Fact]
    public void Multiply_NumberObjects_Should_ReturnProduct()
    {
        var a = TestNumberValueObjectBase.Create(4).Output;
        var b = TestNumberValueObjectBase.Create(6).Output;

        var result = a * b;

        Assert.Equal(24, result.Output.Value);
    }

    [Fact]
    public void Divide_NumberObjects_Should_ReturnQuotient()
    {
        var a = TestNumberValueObjectBase.Create(20).Output;
        var b = TestNumberValueObjectBase.Create(4).Output;

        var result = a / b;

        Assert.Equal(5, result.Output.Value);
    }

    [Fact]
    public void Multiply_ValueAndObject_Should_ReturnProduct()
    {
        var b = TestNumberValueObjectBase.Create(5).Output;
        const long a = 3;

        var result1 = a * b;
        var result2 = b * a;

        Assert.Equal(15, result1.Output.Value);
        Assert.Equal(15, result2.Output.Value);
    }

    [Fact]
    public void Divide_ValueAndObject_Should_ReturnQuotient()
    {
        var b = TestNumberValueObjectBase.Create(4).Output;
        const long a = 20;

        var result1 = a / b;
        var result2 = b / a;

        Assert.Equal(5, result1.Output.Value);
        Assert.Equal(0, result2.Output.Value);
    }
}