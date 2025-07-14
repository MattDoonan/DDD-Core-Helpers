namespace Outputs.Base.Interfaces;

public interface IResultFailure
{
    public bool Failed { get; }

    public string ErrorMessage { get; }
    public string ErrorReason { get; }

}


public interface IResultStatus : IResultFailure
{
    public bool Successful => !Failed;
    public string SuccessLog { get; }
}