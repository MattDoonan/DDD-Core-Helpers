using Outputs.Results.Abstract;

namespace Outputs.Results.Interfaces;

public interface IResultFailure
{
    public bool IsFailure { get; }
    public List<string> ErrorMessages { get; }
}

public interface IResultStatus : IResultFailure
{
    public bool IsSuccessful => !IsFailure;
    public FailureType FailureType { get; }
    public string MainError => FailureType.ToMessage();
}