using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Results.Interfaces;

public interface IResultFailure
{
    public bool IsFailure { get; }
    public OperationStatus PrimaryStatus { get; }
    void SetPrimaryStatus(OperationStatus newStatus);
    bool IsPrimaryStatus(OperationStatus status);
    bool IsPrimaryStatus(StatusType statusType);
}