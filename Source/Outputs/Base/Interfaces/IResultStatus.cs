namespace Outputs.Base.Interfaces;

public interface IResultFailure
{
    public bool IsFailure { get; }
    public List<string> ErrorMessages { get; }

}


public interface IResultStatus : IResultFailure
{
    public bool IsSuccessful => !IsFailure;
    public List<string> SuccessLogs { get; }
}