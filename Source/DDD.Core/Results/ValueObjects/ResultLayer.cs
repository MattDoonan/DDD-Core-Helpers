namespace DDD.Core.Results.ValueObjects;

public enum ResultLayer
{
    Unknown,
    Infrastructure,
    Service,
    UseCase,
    Web
}

public static class ResultLayerExtensions
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