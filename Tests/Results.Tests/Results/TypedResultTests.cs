using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results;
using DDD.Core.Results.Abstract;
using DDD.Core.Results.Exceptions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;
using Results.Tests.Results.Extensions;
using Xunit;

namespace Results.Tests.Results;

public class TypedResultTests
{
    public sealed class TestResult<T> : TypedResult<T>
    {
        public TestResult(T value, ResultLayer layer) : base(value, layer)
        {
        }

        public TestResult(IResultStatus result, ResultLayer? newResultLayer = null) : base(result, newResultLayer)
        {
        }

        public TestResult(ITypedResult<T> valueResult, ResultLayer? newResultLayer = null) : base(valueResult,
            newResultLayer)
        {
        }

        public TestResult(ResultError error) : base(error)
        {
        }
    }

    [Fact]
    public void SuccessfulIntTypedResult_WhenAccessingOutput_Should_ReturnOutput()
    {
        var result = new TestResult<int>(42, ResultLayer.UseCase);
        result.AssertSuccessful(42, ResultLayer.UseCase);
    }

    [Fact]
    public void FailureIntTypedResult_WhenAccessingOutput_Should_ThrowException()
    {
        var error = new ResultError(OperationStatus.Failure<int>(), ResultLayer.UseCase, "Failure occurred");
        var result = new TestResult<int>(error);
        result.AssertFailure(OperationStatus.Failure<int>(), ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void SuccessfulStringTypedResult_WhenAccessingOutput_Should_ReturnOutput()
    {
        var result = new TestResult<string>("A result", ResultLayer.UseCase);
        result.AssertSuccessful("A result", ResultLayer.UseCase);
    }

    [Fact]
    public void FailureIntStringResult_WhenAccessingOutput_Should_ThrowException()
    {
        var error = new ResultError(OperationStatus.Failure<string>(), ResultLayer.UseCase, "Failure occurred");
        var result = new TestResult<string>(error);
        result.AssertFailure(OperationStatus.Failure<string>(), ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void SuccessfulObjectTypedResult_WhenAccessingOutput_Should_ReturnOutput()
    {
        var objectOutput = new { Name = "Test", Value = 123 };
        var result = new TestResult<object>(objectOutput, ResultLayer.UseCase);
        result.AssertSuccessful(objectOutput, ResultLayer.UseCase);
    }

    [Fact]
    public void FailureObjectStringResult_WhenAccessingOutput_Should_ThrowException()
    {
        var error = new ResultError(OperationStatus.Failure<object>(), ResultLayer.UseCase, "Failure occurred");
        var result = new TestResult<object>(error);
        result.AssertFailure(OperationStatus.Failure<object>(), ResultLayer.UseCase, 1);
    }
    
    [Fact]
    public void SuccessfulIntTypedResult_WithANullableOutputType_WhenAccessingOutput_Should_ReturnOutput()
    {
        var result = new TestResult<int?>(null, ResultLayer.UseCase);
        result.AssertSuccessful(null, ResultLayer.UseCase);
    }
    
    [Fact]
    public void FailureIntTypedResult_WithANullableOutputType_WhenAccessingOutput_Should_ThrowException()
    {
        var error = new ResultError(OperationStatus.Failure<int?>(), ResultLayer.UseCase, "Failure occurred");
        var result = new TestResult<int?>(error);
        result.AssertFailure(OperationStatus.Failure<int?>(), ResultLayer.UseCase, 1);
    }

    [Fact]
    public void SuccessfulStringTypedResult_WithANullableOutputType_WhenAccessingOutput_Should_ReturnOutput()
    {
        var result = new TestResult<string?>(null, ResultLayer.UseCase);
        result.AssertSuccessful(null, ResultLayer.UseCase);
    }
    
    [Fact]
    public void FailureStringTypedResult_WithANullableOutputType_WhenAccessingOutput_Should_ThrowException()
    {
        var error = new ResultError(OperationStatus.Failure<string?>(), ResultLayer.UseCase, "Failure occurred");
        var result = new TestResult<string?>(error);
        result.AssertFailure(OperationStatus.Failure<string?>(), ResultLayer.UseCase, 1);
    }

    [Fact]
    public void ConvertingSuccessfulNonTypedResult_ToTypedResult_Should_ThrowException()
    {
        var nonTypedResult = Result.Pass();
        Assert.Throws<ResultConversionException>(() => new TestResult<int>(nonTypedResult));
    }

    [Fact]
    public void ConvertingFailureNonTypedResult_ToTypedResult_Should_WorkCorrectly()
    {
        var nonTypedResult = Result.Fail("Failure occurred");
        var typedResult = new TestResult<int>(nonTypedResult);
        typedResult.AssertFailure(OperationStatus.Failure(), ResultLayer.Unknown, 1);
    }

    [Fact]
    public void TryGetOutput_WhenResultIsSuccessful_Should_ReturnOutput()
    {
        var result = new TestResult<int>(100, ResultLayer.UseCase);
        Assert.True(result.TryGetOutput(out var output));
        Assert.Equal(100, output);
    }

    [Fact]
    public void TryGetOutput_WhenResultIsFailure_Should_ReturnFalse()
    {
        var error = new ResultError(OperationStatus.Failure<int>(), ResultLayer.UseCase, "Failure occurred");
        var result = new TestResult<object>(error);
        Assert.False(result.TryGetOutput(out var output));
        Assert.Null(output);
    }

    [Fact]
    public void GetOrDefault_WhenResultIsSuccessful_Should_ReturnOutput()
    {
        var result = new TestResult<int>(200, ResultLayer.UseCase);
        Assert.Equal(200, result.GetOrDefault(0));
    }

    [Fact]
    public void GetOrDefault_WhenResultIsFailure_Should_ReturnDefaultValue()
    {
        var error = new ResultError(OperationStatus.Failure<int>(), ResultLayer.UseCase, "Failure occurred");
        var result = new TestResult<int>(error);
        Assert.Equal(42, result.GetOrDefault(42));
    }
    
    [Fact]
    public void Unwrap_WhenResultIsSuccessful_Should_ReturnOutput()
    {
        var result = new TestResult<string>("Hello, World!", ResultLayer.UseCase);
        Assert.Equal("Hello, World!", result.Unwrap());
    }

    [Fact]
    public void Unwrap_WhenResultIsFailure_Should_ReturnNull()
    {
        var error = new ResultError(OperationStatus.Failure<string>(), ResultLayer.UseCase, "Failure occurred");
        var result = new TestResult<string>(error);
        Assert.Null(result.Unwrap());
    }
    
    [Fact]
    public void GetOutputTypeName_Should_ReturnCorrectTypeName()
    {
        var result = new TestResult<double?>(3.14, ResultLayer.UseCase);
        Assert.Equal(typeof(double?).Name, result.GetOutputTypeName());
    }

    [Fact]
    public void GetOutputTypeName_WithNullableType_Should_ReturnCorrectTypeName()
    {
        var result = new TestResult<int?>(null, ResultLayer.UseCase);
        Assert.Equal(typeof(int?).Name, result.GetOutputTypeName());
    }
    
    [Fact]
    public void GetOutputType_Should_ReturnCorrectTypeName()
    {
        var result = new TestResult<double>(3.14, ResultLayer.UseCase);
        Assert.Equal(typeof(double), result.GetOutputType());
    }

    [Fact]
    public void GetOutputType_WithNullableType_Should_ReturnCorrectTypeName()
    {
        var result = new TestResult<int?>(null, ResultLayer.UseCase);
        Assert.Equal(typeof(int?), result.GetOutputType());
    }
    
    [Fact]
    public void GetErrorsOfType_Should_ReturnErrorsOfSpecifiedType()
    {
        var error = new ResultError(OperationStatus.AlreadyExists<int>(), ResultLayer.Infrastructure, "validation error");
        var result = new TestResult<int>(error);
        result.AddError(new ResultError(OperationStatus.Failure(), ResultLayer.Infrastructure, "error"));
        var validationErrors = result.GetErrorsOfType().ToList();
        Assert.Single(validationErrors);
        Assert.Equal(OperationStatus.AlreadyExists<int>(), validationErrors[0].Failure);
    }
    
    [Fact]
    public void ImplicitConversion_FromTypedResult_ToOutputType_Should_WorkCorrectly()
    {
        var result = new TestResult<string>("Implicit conversion", ResultLayer.UseCase);
        string output = result;
        Assert.Equal("Implicit conversion", output);
    }
}