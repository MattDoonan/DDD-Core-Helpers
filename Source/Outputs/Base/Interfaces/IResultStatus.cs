namespace Outputs.Base.Interfaces;

public interface IResultFailure
{
    public bool IsFailure { get; }

    public string ErrorMessage { get; }
    public string ErrorReason { get; }

}


public interface IResultStatus : IResultFailure
{
    public bool IsSuccessful => !IsFailure;
    public string SuccessLog { get; }
}