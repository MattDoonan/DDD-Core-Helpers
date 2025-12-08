using DDD.Core.Results.Exceptions;

namespace DDD.Core.Results.ValueObjects;

public record ResultError
{
    public readonly FailureType FailureType;
    public readonly ResultLayer ResultLayer;
    public readonly string? Because;
    public readonly Type? OutputType;

    public ResultError(FailureType failureType, ResultLayer resultLayer, string? because)
        : this(failureType, resultLayer, because, null)
    {
    }
    
    public ResultError(FailureType failureType, ResultLayer resultLayer, Type? outputType) 
        : this(failureType, resultLayer, null, outputType)
    {
    }
    
    public ResultError(FailureType failureType, ResultLayer resultLayer, string? because = null, Type? outputType = null)
    {
        if (failureType is FailureType.None)
        {
            throw new ArgumentException("An error message cannot contain none as a failure type.");
        }
        FailureType = failureType;
        ResultLayer = resultLayer;
        Because = because;
        OutputType = outputType;
    }
    
    public bool IsOfType<T>()
    {
        return IsOfType(typeof(T));
    }
    
    public bool IsOfType(Type type)
    {
        return OutputType == type;
    }

    public bool IsFailureType(FailureType failureType)
    {
        return FailureType == failureType;
    }

    public bool IsLayer(ResultLayer layer)
    {
        return ResultLayer == layer;
    }

    public override string ToString()
    {
        var sentenceStarter = $"{FailureType.ToMessage(OutputType)} on the {ResultLayer.ToMessage()}";
        return string.IsNullOrWhiteSpace(Because) 
            ? sentenceStarter 
            : $"{sentenceStarter} because {Because}";
    }

    public ResultError AddLayer(ResultLayer layer)
    {
        return ResultLayer is ResultLayer.Unknown 
            ? new ResultError(FailureType, layer, Because, OutputType) 
            : this;
    }

}
