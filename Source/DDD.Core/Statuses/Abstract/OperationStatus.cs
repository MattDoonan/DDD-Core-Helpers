using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses.Abstract;

/// <summary>
/// Represents the status of an operation.
/// </summary>
public abstract record OperationStatus
{
    public readonly StatusType Type;
    public string Message;

    protected OperationStatus(StatusType statusType, string message)
    {
        Type = statusType;
        Message = message;
    }

    /// <summary>
    /// Creates an <see cref="AlreadyExists"/> operation status.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="AlreadyExists"/>.
    /// </returns>
    public static AlreadyExists AlreadyExists()
    {
        return new AlreadyExists();
    }
    
    /// <summary>
    /// Creates a <see cref="Cancelled"/> operation status.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="Cancelled"/>.
    /// </returns>
    public static Cancelled Cancelled()
    {
        return new Cancelled();
    }

    /// <summary>
    /// Creates a <see cref="ConcurrencyViolation"/> operation status.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="ConcurrencyViolation"/>.
    /// </returns>
    public static ConcurrencyViolation ConcurrencyViolation()
    {
        return new ConcurrencyViolation();
    }

    /// <summary>
    /// Creates a <see cref="DomainViolation"/> operation status.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="DomainViolation"/>.
    /// </returns>
    public static DomainViolation DomainViolation()
    { 
        return new DomainViolation();
    }

    /// <summary>
    /// Creates a <see cref="Failure"/> operation status.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="Failure"/>.
    /// </returns>
    public static Failure Failure()
    {
        return new Failure();
    }

    /// <summary>
    /// Creates a <see cref="InvalidInput"/> operation status.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="InvalidInput"/>.
    /// </returns>
    public static InvalidInput InvalidInput()
    {
        return new InvalidInput();
    }

    /// <summary>
    /// Creates a <see cref="InvalidRequest"/> operation status.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="InvalidRequest"/>.
    /// </returns>
    public static InvalidRequest InvalidRequest()
    {
        return new InvalidRequest();
    }

    /// <summary>
    /// Creates a <see cref="InvariantViolation"/> operation status.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="InvariantViolation"/>.
    /// </returns>
    public static InvariantViolation InvariantViolation()
    { 
        return new InvariantViolation();
    }
    
    /// <summary>
    /// Creates a <see cref="NotAllowed"/> operation status.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="NotAllowed"/>.
    /// </returns>
    public static NotAllowed NotAllowed()
    {
        return new NotAllowed();
    }

    /// <summary>
    /// Creates a <see cref="NotFound"/> operation status.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="NotFound"/>.
    /// </returns>
    public static NotFound NotFound()
    {
        return new NotFound();
    }
    
    /// <summary>
    /// Creates a <see cref="Success"/> operation status.
    /// </summary>
    /// <returns></returns>
    public static Success Success()
    {
        return new Success();
    }
    
    /// <summary>
    /// Creates a <see cref="TimedOut"/> operation status.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="TimedOut"/>.
    /// </returns>
    public static TimedOut TimedOut()
    {
        return new TimedOut();
    }
    
    /// <summary>
    /// Creates an <see cref="AlreadyExists"/> operation status for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type related to the operation.
    /// </typeparam>
    /// <returns>
    /// An instance of <see cref="AlreadyExists"/>.
    /// </returns>
    public static AlreadyExists AlreadyExists<T>()
    {
        return new AlreadyExists<T>();
    }
    
    /// <summary>
    /// Creates a <see cref="Cancelled"/> operation status for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type related to the operation.
    /// </typeparam>
    /// <returns>
    /// An instance of <see cref="Cancelled"/>.
    /// </returns>
    public static Cancelled Cancelled<T>()
    {
        return new Cancelled<T>();
    }

    /// <summary>
    /// Creates a <see cref="ConcurrencyViolation"/> operation status for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type related to the operation.
    /// </typeparam>
    /// <returns>
    /// An instance of <see cref="ConcurrencyViolation"/>.
    /// </returns>
    public static ConcurrencyViolation ConcurrencyViolation<T>()
    {
        return new ConcurrencyViolation<T>();
    }

    /// <summary>
    /// Creates a <see cref="DomainViolation"/> operation status for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type related to the operation.
    /// </typeparam>
    /// <returns>
    /// An instance of <see cref="DomainViolation"/>.
    /// </returns>
    public static DomainViolation DomainViolation<T>()
    { 
        return new DomainViolation<T>();
    }

    /// <summary>
    /// Creates a <see cref="Failure"/> operation status for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type related to the operation.
    /// </typeparam>
    /// <returns>
    /// An instance of <see cref="Failure"/>.
    /// </returns>
    public static Failure Failure<T>()
    {
        return new Failure<T>();
    }

    /// <summary>
    /// Creates a <see cref="InvalidInput"/> operation status for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type related to the operation.
    /// </typeparam>
    /// <returns>
    /// An instance of <see cref="InvalidInput"/>.
    /// </returns>
    public static InvalidInput InvalidInput<T>()
    {
        return new InvalidInput<T>();
    }

    /// <summary>
    /// Creates a <see cref="InvalidRequest"/> operation status for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type related to the operation.
    /// </typeparam>
    /// <returns>
    /// An instance of <see cref="InvalidRequest"/>.
    /// </returns>
    public static InvalidRequest InvalidRequest<T>()
    {
        return new InvalidRequest<T>();
    }

    /// <summary>
    /// Creates a <see cref="InvariantViolation"/> operation status for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type related to the operation.
    /// </typeparam>
    /// <returns>
    /// An instance of <see cref="InvariantViolation"/>.
    /// </returns>
    public static InvariantViolation InvariantViolation<T>()
    { 
        return new InvariantViolation<T>();
    }
    
    /// <summary>
    /// Creates a <see cref="NotAllowed"/> operation status for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type related to the operation.
    /// </typeparam>
    /// <returns>
    /// An instance of <see cref="NotAllowed"/>.
    /// </returns>
    public static NotAllowed NotAllowed<T>()
    {
        return new NotAllowed<T>();
    }
    
    /// <summary>
    /// Creates a <see cref="NotFound"/> operation status for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type related to the operation.
    /// </typeparam>
    /// <returns>
    /// An instance of <see cref="NotFound"/>.
    /// </returns>
    public static NotFound NotFound<T>()
    {
        return new NotFound<T>();
    }
    
    /// <summary>
    /// Creates a <see cref="Success"/> operation status for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type related to the operation.
    /// </typeparam>
    /// <returns>
    /// An instance of <see cref="Success"/>.
    /// </returns>
    public static Success Success<T>()
    {
        return new Success<T>();
    }
    
    /// <summary>
    /// Creates a <see cref="TimedOut"/> operation status for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type related to the operation.
    /// </typeparam>
    /// <returns>
    /// An instance of <see cref="TimedOut"/>.
    /// </returns>
    public static TimedOut TimedOut<T>()
    {
        return new TimedOut<T>();
    }
}