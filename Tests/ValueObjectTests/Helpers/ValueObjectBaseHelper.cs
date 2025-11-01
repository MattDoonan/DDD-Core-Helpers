using Core.Interfaces;
using Core.Results.Basic;
using Core.ValueObjects.Regular.Base;
using Xunit;

namespace ValueObjectTests.Helpers;

public abstract class ValueObjectBaseHelper<T>(T baseValue, T largerValue, T smallerValue)
    where T : IComparable, IComparable<T>, IEquatable<T>
{
    private record TestValueObjectBase : SingleValueObject<T>, ISimpleValueObjectFactory<T, TestValueObjectBase>
    {
        private TestValueObjectBase(T value) : base(value) { }
        public static ValueObjectResult<TestValueObjectBase> Create(T value)
        {
            return new TestValueObjectBase(value);
        }
    }


    [Fact]
    public void ToString_Should_ReturnValueAsString()
    {
        var valueObject = TestValueObjectBase.Create(baseValue);
        Assert.True(valueObject.IsSuccessful);
        var toString = valueObject.Output.ValueAsString();
        Assert.Equal($"{valueObject.Output.Value}", toString);
    }
    
    public static IEnumerable<object[]> NotEqualObjects =>
        new List<object[]>
        {
            new object[] { 1 },
            new object[] { 10L },
            new object[] { "Test" },
            new object[] { new () },
            new object[] { true },
            new object[] { false },
            new object[] { null! }
        };

    [Theory, MemberData(nameof(NotEqualObjects))]
    public void Equals_WithDifferentObjects_Should_ReturnFalse(object input)
    {
        var valueObject = TestValueObjectBase.Create(baseValue);
        Assert.True(valueObject.IsSuccessful);
        var isEqual = valueObject.Output.Equals(input);
        Assert.False(isEqual);
    }
    
    [Fact]
    public void Equals_WithSameClass_ButDifferentValue_Should_ReturnFalse()
    {
        var valueObject = TestValueObjectBase.Create(baseValue);
        Assert.True(valueObject.IsSuccessful);
        var isEqual1 = valueObject.Output.Equals(TestValueObjectBase.Create(smallerValue).Output);
        Assert.False(isEqual1);
        var isEqual2 = valueObject.Output.Equals(TestValueObjectBase.Create(largerValue).Output);
        Assert.False(isEqual2);
    }

    [Fact]
    public void Equals_WithSameClass_Should_ReturnTrue()
    {
        var valueObject = TestValueObjectBase.Create(baseValue);
        Assert.True(valueObject.IsSuccessful);
        var isEqual = valueObject.Output.Equals(valueObject);
        Assert.True(isEqual);
    }
    
    [Fact]
    public void Equals_WithSameObjects_Should_ReturnTrue()
    {
        var valueObject = TestValueObjectBase.Create(baseValue);
        Assert.True(valueObject.IsSuccessful);
        var isEqual = valueObject.Output.Equals((object) valueObject.Output);
        Assert.True(isEqual);
    }
    
    [Fact]
    public void Equals_NullObject_Should_ReturnFalse()
    {
        var valueObject = TestValueObjectBase.Create(baseValue);
        Assert.True(valueObject.IsSuccessful);
        TestValueObjectBase? nullObject = null; 
        var isEqual = valueObject.Output.Equals(nullObject);
        Assert.False(isEqual);
    }
    
    [Fact]
    public void CompareTo_Object_WithSameType_ShouldReturnCorrectComparison()
    {
        var obj1 = TestValueObjectBase.Create(smallerValue).Output;
        var obj2 = TestValueObjectBase.Create(baseValue).Output;
        var result = obj1.CompareTo((object)obj2);
        Assert.True(result < 0); 
    }

    [Fact]
    public void CompareTo_Object_WithNull_ShouldThrowArgumentException()
    {
        var obj1 = TestValueObjectBase.Create(baseValue).Output;
        var ex = Assert.Throws<ArgumentException>(() => obj1.CompareTo("not a value object"));
        Assert.Contains("Object is not of the correct type", ex.Message);
    }

    [Fact]
    public void CompareTo_Typed_ShouldReturnZeroForEqualValues()
    {
        var obj1 = TestValueObjectBase.Create(baseValue).Output;
        var obj2 = TestValueObjectBase.Create(baseValue).Output;
        var result = obj1.CompareTo(obj2);
        Assert.Equal(0, result);
    }

    [Fact]
    public void CompareTo_Typed_ShouldReturnPositiveWhenOtherIsNull()
    {
        var obj1 = TestValueObjectBase.Create(baseValue).Output;
        var result = obj1.CompareTo(null);
        Assert.True(result > 0);
    }

    [Fact]
    public void CompareTo_Typed_ShouldReturnNegativeWhenValueIsLess()
    {
        var obj1 = TestValueObjectBase.Create(smallerValue).Output;
        var obj2 = TestValueObjectBase.Create(baseValue).Output;
        var result = obj1.CompareTo(obj2);
        Assert.True(result < 0);
    }

    [Fact]
    public void CompareTo_Typed_ShouldReturnPositiveWhenValueIsGreater()
    {
        var obj1 = TestValueObjectBase.Create(largerValue).Output;
        var obj2 = TestValueObjectBase.Create(baseValue).Output;
        var result = obj1.CompareTo(obj2);
        Assert.True(result > 0);
    }
    
    
    [Fact]
    public void EqualsEqualityOperator_ShouldReturnTrueForSameValues()
    {
        var obj1 = TestValueObjectBase.Create(baseValue).Output;
        var obj2 = TestValueObjectBase.Create(baseValue).Output;
        Assert.True(obj1 == obj2);
    }
    
    [Fact]
    public void EqualsEqualityOperator_ShouldReturnFalseForDifferentValues()
    {
        var obj1 = TestValueObjectBase.Create(baseValue).Output;
        var obj2 = TestValueObjectBase.Create(smallerValue).Output;
        Assert.False(obj1 == obj2);
    }
    
    [Fact]
    public void NotEqualsEqualityOperator_ShouldReturnTrueForDifferentValues()
    {
        var obj1 = TestValueObjectBase.Create(baseValue).Output;
        var obj2 = TestValueObjectBase.Create(smallerValue).Output;
        Assert.True(obj1 != obj2);
    }
    
    [Fact]
    public void NotEqualsEqualityOperator_ShouldReturnFalseForSameValues()
    {
        var obj1 = TestValueObjectBase.Create(baseValue).Output;
        var obj2 = TestValueObjectBase.Create(baseValue).Output;
        Assert.False(obj1 != obj2);
    }
    
    [Fact]
    public void EqualsEqualityOperator_ShouldReturnFalseForNullValue()
    {
        var obj1 = TestValueObjectBase.Create(baseValue).Output;
        Assert.False(obj1 == null!);
    }
    
    [Fact]
    public void NotEqualsEqualityOperator_ShouldReturnTrueForNullValue()
    {
        var obj1 = TestValueObjectBase.Create(baseValue).Output;
        Assert.True(obj1 != null!);
    }
    
    public static IEnumerable<object[]> EqualityDifferentType =>
        new List<object[]>
        {
            new object[] { 1 },
            new object[] { 10L },
            new object[] { "Test" },
            new object[] { new () },
            new object[] { true },
            new object[] { false },
        };
    
    [Theory, MemberData(nameof(EqualityDifferentType))]
    public void EqualsEqualityOperator_ShouldReturnFalseForDifferentTypeValue(object compare)
    {
        var obj1 = TestValueObjectBase.Create(baseValue).Output;
        Assert.False(obj1 == compare);
    }
    
    [Theory, MemberData(nameof(EqualityDifferentType))]
    public void NotEqualsEqualityOperator_ShouldReturnTrueForDifferentTypeValue(object compare)
    {
        var obj1 = TestValueObjectBase.Create(baseValue).Output;
        Assert.True(obj1 != compare);
    }
    
    [Fact]
    public void GetHasCode_OfSameObject_Should_Equal()
    {
        var obj1 = TestValueObjectBase.Create(baseValue).Output;
        var obj2 = TestValueObjectBase.Create(baseValue).Output;
        Assert.Equal(obj1.GetHashCode(), obj1.GetHashCode());
        Assert.Equal(obj1.GetHashCode(), obj2.GetHashCode());
    }
    
    [Fact]
    public void LessThanOrEqual_Should_ReturnCorrectBoolean()
    {
        var a = TestValueObjectBase.Create(smallerValue).Output;
        var b = TestValueObjectBase.Create(baseValue).Output;
        var c = TestValueObjectBase.Create(smallerValue).Output;

        Assert.True(a <= b);
        Assert.True(a <= c);
        Assert.False(b <= a);
    }

    [Fact]
    public void GreaterThanOrEqual_Should_ReturnCorrectBoolean()
    {
        var a = TestValueObjectBase.Create(largerValue).Output;
        var b = TestValueObjectBase.Create(baseValue).Output;
        var c = TestValueObjectBase.Create(largerValue).Output;

        Assert.True(a >= b);
        Assert.True(a >= c);
        Assert.False(b >= a);
    }

    [Fact]
    public void LessThan_Should_ReturnCorrectBoolean()
    {
        var a = TestValueObjectBase.Create(baseValue).Output;
        var b = TestValueObjectBase.Create(largerValue).Output;

        Assert.True(a < b);
        Assert.False(b < a);
    }

    [Fact]
    public void GreaterThan_Should_ReturnCorrectBoolean()
    {
        var a = TestValueObjectBase.Create(baseValue).Output;
        var b = TestValueObjectBase.Create(smallerValue).Output;

        Assert.True(a > b);
        Assert.False(b > a);
    }
}