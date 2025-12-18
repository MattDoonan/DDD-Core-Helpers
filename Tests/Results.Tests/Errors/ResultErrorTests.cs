using DDD.Core.Results.Extensions;
using DDD.Core.Results.ValueObjects;
using Xunit;

namespace Results.Tests.Errors;

public class ResultErrorTests
{
    [Fact]
    public void Create_WithNoneFailureType_Should_ThrowError()
    {
        Assert.Throws<ArgumentException>(() => new ResultError(FailureType.None, ResultLayer.Infrastructure));
    }
    
    [Fact]
    public void Create_WithValidFailureType_Should_CreateSuccessfully()
    {
        var expectedFailureType = FailureType.None;
        var expectedLayer = ResultLayer.Infrastructure;
        var resultLayer = new ResultError(expectedFailureType, expectedLayer);
        Assert.Equal(expectedFailureType, resultLayer.FailureType);
        Assert.Equal(expectedLayer, resultLayer.ResultLayer);
    }
    
    [Fact]
    public void IsOfType_ReturnsTrue_WhenItContainsType()
    {
        var error = new ResultError(FailureType.AlreadyExists, ResultLayer.Infrastructure, typeof(string));
        Assert.True(error.IsOfType<string>());
        Assert.True(error.IsOfType(typeof(string)));
    }

    [Fact]
    public void IsOfType_ReturnsFalse_WhenItDoesNotContainType()
    {
        var error = new ResultError(FailureType.None, ResultLayer.Infrastructure, typeof(string));
        Assert.False(error.IsOfType<int>());
        Assert.False(error.IsOfType(typeof(int)));
    }
    
    [Theory, MemberData(nameof(AllFailureTypes))]
    public void IsFailureType_ReturnsTrue_WhenItIsInputtedType(FailureType failureType)
    {
        var error = new ResultError(failureType, ResultLayer.Infrastructure);
        Assert.True(error.IsFailureType(failureType));
    }

    [Fact]
    public void IsFailureType_ReturnsFalse_WhenItIsNotTheInputtedType()
    {
        var error = new ResultError(FailureType.Generic, ResultLayer.Infrastructure);
        Assert.False(error.IsFailureType(FailureType.None));
    }
    
    [Theory, MemberData(nameof(AllLayers))]
    public void IsLayer_ReturnsTrue_WhenItIsTheInputtedLayer(ResultLayer layer)
    {
        var error = new ResultError(FailureType.Generic, layer);
        Assert.True(error.IsLayer(layer));
    }
    
    [Fact]
    public void IsLayer_ReturnsFalse_WhenItIsNotTheInputtedLayer()
    {
        var error = new ResultError(FailureType.Generic, ResultLayer.Unknown);
        Assert.False(error.IsLayer(ResultLayer.Infrastructure));
    }

    [Fact]
    public void ToErrorMessage_WithNoBecause_CreatesFormattedErrorMessage()
    {
        var error = new ResultError(FailureType.Generic, ResultLayer.Infrastructure);
        Assert.Equal($"{FailureType.Generic.ToMessage()} on the {ResultLayer.Infrastructure.ToMessage()}", error.ToErrorMessage());
    }
    
    [Fact]
    public void ToErrorMessage_WithType_AndWithNoBecause_CreatesFormattedErrorMessage()
    {
        var error = new ResultError(FailureType.Generic, ResultLayer.Infrastructure, typeof(string));
        Assert.Equal($"{FailureType.Generic.ToMessage<string>()} on the {ResultLayer.Infrastructure.ToMessage()}", error.ToErrorMessage());
    }
    
    [Fact]
    public void ToErrorMessage_AndWithBecause_CreatesFormattedErrorMessage()
    {
        var error = new ResultError(FailureType.Generic, ResultLayer.Infrastructure, "of a test");
        Assert.Equal($"{FailureType.Generic.ToMessage()} on the {ResultLayer.Infrastructure.ToMessage()} because of a test", error.ToErrorMessage());
    }
    
    [Fact]
    public void ToErrorMessage_WithType_AndWithBecause_CreatesFormattedErrorMessage()
    {
        var error = new ResultError(FailureType.Generic, ResultLayer.Infrastructure, "of a test", typeof(int));
        Assert.Equal($"{FailureType.Generic.ToMessage<int>()} on the {ResultLayer.Infrastructure.ToMessage()} because of a test", error.ToErrorMessage());
    }

    [Fact]
    public void WithLayer_WhenLayerIsCurrentlyUnknown_Should_UpdateLayer()
    {
        var error = new ResultError(FailureType.Generic, ResultLayer.Unknown);
        var newError = error.WithLayer(ResultLayer.Infrastructure);
        Assert.Equal(ResultLayer.Infrastructure, newError.ResultLayer);
    }

    [Fact]
    public void WithLayer_WhenLayerIsNotUnknown_Should_NotUpdateLayer()
    {
        var error = new ResultError(FailureType.Generic, ResultLayer.Service);
        var newError = error.WithLayer(ResultLayer.Infrastructure);
        Assert.Equal(ResultLayer.Service, newError.ResultLayer);
    }
    
    
    public static IEnumerable<object[]> AllFailureTypes =>
        Enum.GetValues<FailureType>().Where(ft => ft != FailureType.None)
            .Select(ft => new object[] { ft });
    
    public static IEnumerable<object[]> AllLayers =>
        Enum.GetValues<ResultLayer>()
            .Select(rl => new object[] { rl });
}