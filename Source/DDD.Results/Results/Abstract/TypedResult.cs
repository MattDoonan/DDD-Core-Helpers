using System.Diagnostics.CodeAnalysis;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Exceptions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Abstract;

/// <summary>
/// Abstract base class for typed results.
/// Provides functionality to handle results with an associated output of type T.
/// Inherits from ResultStatus.
/// </summary>
/// <typeparam name="T">
/// The type of the output associated with the result.
/// </typeparam>
public abstract class TypedResult<T> : ResultStatus, ITypedResult<T>
{
    /// <summary>
    /// The output associated with the result.
    /// </summary>
    /// <exception cref="ResultOutputAccessException">
    /// Thrown when attempting to access the output of a failed result.
    /// </exception>
    public T Output
    {
        get
        {
            if (_output is not null && HasOutput)
            {
                return _output;
            }
            return !HasOutput 
                ? throw new ResultOutputAccessException(this, $"Cannot access the output of type {typeof(T).Name} when the result is a failure.") 
                : default!;
        }
    }
    public bool HasOutput { get; }

    private readonly T? _output;

    /// <summary>
    /// Initializes a new successful instance of the <see cref="TypedResult{T}"/> class
    /// with the specified output value and result layer.
    /// </summary>
    /// <param name="output">
    /// The output value of type T.
    /// </param>
    /// <param name="layer">
    /// The result layer indicating where the result was generated.
    /// </param>
    protected TypedResult(T output, ResultLayer layer) 
        : base(OperationStatus.Success<T>(), layer)
    {
        _output = output;
        HasOutput  = true;
    }
    
    /// <summary>
    /// Initializes a new failed instance of the <see cref="TypedResult{T}"/> class
    /// by converting a non-typed failed result.
    /// </summary>
    /// <param name="result">
    /// The non-typed result to convert.
    /// </param>
    /// <param name="newResultLayer">
    /// The new result layer to set for the converted result.
    /// </param>
    /// <exception cref="ResultConversionException">
    /// Thrown when attempting to convert a successful non-typed result to a typed result.
    /// </exception>
    protected TypedResult(IResultStatus result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
        if (result.IsSuccessful)
        {
            throw new ResultConversionException(this, "Cannot convert a successful non-typed Result to a typed Result.");
        }
        HasOutput = false;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="TypedResult{T}"/> class
    /// by converting another typed result.
    /// </summary>
    /// <param name="typedResult">
    /// The typed result to convert.
    /// </param>
    /// <param name="newResultLayer">
    /// The new result layer to set for the converted result.
    /// </param>
    protected TypedResult(ITypedResult<T> typedResult, ResultLayer? newResultLayer = null) 
        : base(typedResult, newResultLayer)
    {
        if (typedResult.IsSuccessful)
        {
            _output = typedResult.Output;
            HasOutput = true;
        }
        else
        {
            HasOutput = false;
        }
    }
    
    
    /// <summary>
    /// Initializes a new failed instance of the <see cref="TypedResult{T}"/> class
    /// with the specified error.
    /// </summary>
    /// <param name="error">
    /// The <see cref="ResultError"/> representing the failure details.
    /// </param>
    protected TypedResult(ResultError error) 
        : base(error)
    {
        HasOutput = false;
    }

    /// <summary>
    /// Attempts to get the output of the result.
    /// If the result is successful, assigns the output to the out parameter and returns true.
    /// If the result is a failure, assigns default to the out parameter and returns false.
    /// </summary>
    /// <param name="output">
    /// The output value of type T if the result is successful; otherwise, default.
    /// </param>
    /// <returns>
    /// True if the result is successful and the output is assigned; otherwise, false.
    /// </returns>
    public bool TryGetOutput([NotNullWhen(true)] out T? output)
    {
        output = _output;
        return HasOutput;
    }

    /// <summary>
    /// Unwraps the output of the result.
    /// Returns the output if present; otherwise, returns null.
    /// </summary>
    /// <returns>
    /// The output of type T if present; otherwise, null.
    /// </returns>
    public T? Unwrap() => _output;
    
    /// <summary>
    /// Gets the output of the result or returns the specified default value if the result is a failure.
    /// </summary>
    /// <param name="defaultValue">
    /// The default value to return if the result is a failure.
    /// </param>
    /// <returns>
    /// The output of type T if the result is successful; otherwise, the specified default value.
    /// </returns>
    public T GetOrDefault(T defaultValue) =>  HasOutput ?  Output : defaultValue;
    
    /// <summary>
    /// Gets the type of the output associated with the result.
    /// </summary>
    /// <returns>
    /// The <see cref="Type"/> of the output associated with the result.
    /// </returns>
    public Type GetOutputType() => typeof(T);

    
    /// <summary>
    /// Gets the name of the output type associated with the result.
    /// </summary>
    /// <returns>
    /// The name of the output type associated with the result.
    /// </returns>
    public string GetOutputTypeName() => typeof(T).Name;
    
    /// <summary>
    /// Gets all errors that are of the output type T.
    /// </summary>
    /// <returns>
    /// An enumerable of <see cref="ResultError"/> objects that are of type T.
    /// </returns>
    public IEnumerable<ResultError> GetErrorsOfType() => GetErrorsBy(e => e.IsOfType<T>());
    
    /// <summary>
    /// Implicitly converts a <see cref="TypedResult{T}"/> to its output of type T.
    /// </summary>
    /// <param name="result">
    /// The <see cref="TypedResult{T}"/> to convert.
    /// </param>
    /// <returns>
    /// The output of type T associated with the result.
    /// </returns>
    public static implicit operator T(TypedResult<T> result) => result.Output;
    
}