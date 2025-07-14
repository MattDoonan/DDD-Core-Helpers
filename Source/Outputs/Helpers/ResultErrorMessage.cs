namespace Outputs.Helpers;

public static class ResultErrorMessage
{
    public static string Create(string sentenceStarter, string? because)
    {
        return string.IsNullOrWhiteSpace(because) 
            ? sentenceStarter 
            : $"{sentenceStarter} because {because}";
    }
}