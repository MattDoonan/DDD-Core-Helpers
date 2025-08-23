using Outputs.Results.Base.Enums;

namespace Outputs.Results.Base.Interfaces;

public interface IResultFailure
{
    public bool IsFailure { get; }
    public List<string> ErrorMessages { get; }
}

public interface IResultStatus : IResultFailure
{
    public bool IsSuccessful => !IsFailure;
    public FailureType FailureType { get; }
    public FailedLayer FailedLayer { get; }
    public string MainError => FailureType.ToMessage();
}