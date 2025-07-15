namespace Outputs.Base.Interfaces;

public interface IAdvancedResult : IResultStatus
{
    public FailureType FailureType { get; }
    public string MainError => FailureType.ToMessage();
}