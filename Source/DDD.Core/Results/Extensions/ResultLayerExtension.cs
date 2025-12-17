using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Extensions;

public static class ResultLayerExtension
{
    public static string ToMessage(this ResultLayer failedLayer)
    {
        return failedLayer switch
        {
            ResultLayer.Infrastructure => "Infrastructure layer",
            ResultLayer.Service => "Service layer",
            ResultLayer.UseCase => "Use case layer",
            ResultLayer.Web => "Web layer",
            _ => "Unknown layer"
        };
    }
}