using Base.Results.Base.Abstract;
using Base.Results.Base.Enums;

namespace Base.Helpers;

public static class ResultErrorMessage
{
    public static bool TryCreate(string sentenceStarter, string? because, out string errorLog)
    {
        errorLog = string.Empty;
        if (string.IsNullOrWhiteSpace(sentenceStarter))
        {
            return false;
        }
        errorLog = string.IsNullOrWhiteSpace(because) 
            ? sentenceStarter 
            : $"{sentenceStarter} because {because}";
        return true;
    }

    public static string MainErrorMessage(this ResultStatus resultStatus)
    {
        var failureMessage = resultStatus.FailureType.ToMessage();
        var layerMessage = resultStatus.FailedLayer.ToMessage();
        if (resultStatus.FailureType == FailureType.None || resultStatus.FailedLayer == FailedLayer.None)
        {
            return failureMessage;
        }
        return resultStatus.FailedLayer switch
        {
            FailedLayer.Unknown => $"{failureMessage} at an {layerMessage}",
            _ => $"{failureMessage} at the {layerMessage}"
        };
    }
}