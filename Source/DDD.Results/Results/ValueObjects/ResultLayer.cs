namespace DDD.Core.Results.ValueObjects;

/// <summary>
/// The layer in which a result operation occurred.
/// </summary>
public enum ResultLayer
{
    Unknown,
    Infrastructure,
    Service,
    UseCase,
    Web
}