using DDD.Core.Results;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.SingleValueObjects.Types;
using Xunit;

namespace ValueObjectTests;

public class IncrementalValueObjectTests
{
    private record TestIncrementalValueObject 
        : IncrementalValueObject<float, TestIncrementalValueObject>, ISingleValueObjectFactory<float, TestIncrementalValueObject>
    {
        private TestIncrementalValueObject (float value) : base(value) { }
        public static ValueObjectResult<TestIncrementalValueObject> Create(float value)
        {
            return new TestIncrementalValueObject(value);
        }
    }
    
    [Theory]
    [InlineData(0f, 1f)]
    [InlineData(1.5f, 2.5f)]
    [InlineData(-1f, 0f)]
    public void Next_InstanceMethodInput_ShouldReturnNextValue(float input, float expected)
    {
        var obj = TestIncrementalValueObject.Create(input).Output;
        var result = obj.Next().Output;
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData(0f, -1f)]
    [InlineData(1.5f, 0.5f)]
    [InlineData(-1f, -2f)]
    public void Previous_InstanceMethodInput_ShouldReturnPreviousValue(float input, float expected)
    {
        var obj = TestIncrementalValueObject.Create(input).Output;
        var result = obj.Previous().Output;
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData(0f, 1f)]
    [InlineData(2f, 3f)]
    [InlineData(-3f, -2f)]
    public void Next_StaticMethodInput_ShouldReturnNextValue(float input, float expected)
    {
        var result = TestIncrementalValueObject.Next(input).Output;
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData(0f, -1f)]
    [InlineData(2f, 1f)]
    [InlineData(-3f, -4f)]
    public void Previous_StaticMethodInput_ShouldReturnPreviousValue(float input, float expected)
    {
        var result = TestIncrementalValueObject.Previous(input).Output;
        Assert.Equal(expected, result.Value);
    }
}