using DDD.Core.Results.Extensions;
using DDD.Core.Results.ValueObjects;
using Xunit;

namespace Results.Tests.Extensions;

public class ResultLayerExtensionTests
{

    [Theory, MemberData(nameof(ResultLayerTestCases))]
    public void ToMessage_Should_ReturnCorrectMessage(ResultLayer resultLayer, string expectedMessage)
    {
        Assert.Equal(expectedMessage, resultLayer.ToMessage());
    }
    
    public static IEnumerable<object[]> ResultLayerTestCases =>
    [
        [ResultLayer.Unknown, "Unknown layer"],
        [ResultLayer.Infrastructure, "Infrastructure layer"],
        [ResultLayer.Service, "Service layer"],
        [ResultLayer.UseCase, "Use case layer"],
        [ResultLayer.Web, "Web layer"],
    ];
}