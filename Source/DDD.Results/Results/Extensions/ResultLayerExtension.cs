using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="ResultLayer"/> enum.
/// </summary>
public static class ResultLayerExtension
{
    /// <summary>
    /// Converts a <see cref="ResultLayer"/> enum value to a human-readable message.
    /// </summary>
    /// <param name="failedLayer">
    /// The <see cref="ResultLayer"/> enum value to convert.
    /// </param>
    /// <returns>
    /// A string representing the human-readable message for the specified <see cref="ResultLayer"/> value.
    /// </returns>
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