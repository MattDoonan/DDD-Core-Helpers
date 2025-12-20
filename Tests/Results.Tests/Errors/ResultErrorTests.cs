using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.ValueObjects;
using Results.Tests.Helpers;
using Xunit;

namespace Results.Tests.Errors;

public class ResultErrorTests
{
 
    [Fact]
    public void Create_WithValidFailureType_Should_CreateSuccessfully()
    {
        var expectedFailureType = OperationStatus.Failure();
        var expectedLayer = ResultLayer.Infrastructure;
        var resultLayer = new ResultError(expectedFailureType, expectedLayer);
        Assert.Equal(expectedFailureType, resultLayer.Failure);
        Assert.Equal(expectedLayer, resultLayer.ResultLayer);
    }
    
    [Fact]
    public void IsOfType_ReturnsTrue_WhenItContainsType()
    {
        var error = new ResultError(OperationStatus.AlreadyExists<string>(), ResultLayer.Infrastructure);
        Assert.True(error.IsOfType<string>());
        Assert.True(error.IsOfType(typeof(string)));
    }

    [Fact]
    public void IsOfType_ReturnsFalse_WhenItDoesNotContainType()
    {
        var error = new ResultError(OperationStatus.Failure<string>(), ResultLayer.Infrastructure, "of a test");
        Assert.False(error.IsOfType<int>());
        Assert.False(error.IsOfType(typeof(int)));
    }
    
    [Theory, MemberData(nameof(AllFailureTypes))]
    public void IsFailureType_ReturnsTrue_WhenItIsInputtedType(FailedOperationStatus failedOperationStatus)
    {
        var error = new ResultError(failedOperationStatus, ResultLayer.Infrastructure);
        Assert.True(error.IsFailureType(failedOperationStatus));
    }

    [Fact]
    public void IsFailureType_ReturnsFalse_WhenItIsNotTheInputtedType()
    {
        var error = new ResultError(OperationStatus.Failure(), ResultLayer.Infrastructure);
        Assert.False(error.IsFailureType(OperationStatus.DomainViolation()));
        Assert.False(error.IsFailureType(StatusType.DomainViolation));

    }
    
    [Theory, MemberData(nameof(AllLayers))]
    public void IsLayer_ReturnsTrue_WhenItIsTheInputtedLayer(ResultLayer layer)
    {
        var error = new ResultError(OperationStatus.Failure(), layer);
        Assert.True(error.IsLayer(layer));
    }
    
    [Fact]
    public void IsLayer_ReturnsFalse_WhenItIsNotTheInputtedLayer()
    {
        var error = new ResultError(OperationStatus.Failure(), ResultLayer.Unknown);
        Assert.False(error.IsLayer(ResultLayer.Infrastructure));
    }

    [Fact]
    public void ToErrorMessage_WithNoBecause_CreatesFormattedErrorMessage()
    {
        var error = new ResultError(OperationStatus.Failure(), ResultLayer.Infrastructure);
        Assert.Equal($"{OperationStatus.Failure().Message} on the {ResultLayer.Infrastructure.ToMessage()}", error.ToErrorMessage());
    }
    
    [Fact]
    public void ToErrorMessage_WithType_AndWithNoBecause_CreatesFormattedErrorMessage()
    {
        var error = new ResultError(OperationStatus.Failure<string>(), ResultLayer.Infrastructure);
        Assert.Equal($"{OperationStatus.Failure<string>().Message} on the {ResultLayer.Infrastructure.ToMessage()}", error.ToErrorMessage());
    }
    
    [Fact]
    public void ToErrorMessage_AndWithBecause_CreatesFormattedErrorMessage()
    {
        var error = new ResultError(OperationStatus.Failure(), ResultLayer.Infrastructure, "of a test");
        Assert.Equal($"{OperationStatus.Failure().Message} on the {ResultLayer.Infrastructure.ToMessage()} because of a test", error.ToErrorMessage());
    }
    
    [Fact]
    public void ToErrorMessage_WithType_AndWithBecause_CreatesFormattedErrorMessage()
    {
        var error = new ResultError(OperationStatus.Failure<int>(), ResultLayer.Infrastructure, "of a test");
        Assert.Equal($"{OperationStatus.Failure<int>().Message} on the {ResultLayer.Infrastructure.ToMessage()} because of a test", error.ToErrorMessage());
    }

    [Fact]
    public void WithLayer_WhenLayerIsCurrentlyUnknown_Should_UpdateLayer()
    {
        var error = new ResultError(OperationStatus.Failure(), ResultLayer.Unknown);
        var newError = error.WithLayer(ResultLayer.Infrastructure);
        Assert.Equal(ResultLayer.Infrastructure, newError.ResultLayer);
    }

    [Fact]
    public void WithLayer_WhenLayerIsNotUnknown_Should_NotUpdateLayer()
    {
        var error = new ResultError(OperationStatus.Failure(), ResultLayer.Service);
        var newError = error.WithLayer(ResultLayer.Infrastructure);
        Assert.Equal(ResultLayer.Service, newError.ResultLayer);
    }
    
    
    public static IEnumerable<object[]> AllFailureTypes =>
        OperationStatuses.GetAllFailures().Select(ft => new object[] { ft });
    
    public static IEnumerable<object[]> AllLayers =>
        Enum.GetValues<ResultLayer>()
            .Select(rl => new object[] { rl });
}