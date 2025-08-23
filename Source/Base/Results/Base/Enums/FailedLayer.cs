namespace Outputs.Results.Base.Enums;

public enum FailedLayer
{
    None,
    Unknown,
    Infrastructure,
    Service,
    UseCase,
    Web
}

public static class FailedLayerExtensions
{
    
    public static string ToMessage(this FailedLayer failedLayer)
    {
        return failedLayer switch
        {
            FailedLayer.None => "All layers have been successful",
            FailedLayer.Infrastructure => "Infrastructure layer",
            FailedLayer.Service => "Service layer",
            FailedLayer.UseCase => "Use case layer",
            FailedLayer.Web => "Web layer",
            _ => "Unknown layer"
        };
    }
}