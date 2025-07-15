namespace Outputs.Helpers;

public static class ResultErrorMessage
{
    public static bool Create(string sentenceStarter, string? because, out string errorLog)
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
}