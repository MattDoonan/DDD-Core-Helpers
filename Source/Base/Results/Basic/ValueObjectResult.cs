using Outputs.ObjectTypes;
using Outputs.Results.Advanced;
using Outputs.Results.Base.Abstract;
using Outputs.Results.Base.Enums;
using Outputs.Results.Base.Interfaces;

namespace Outputs.Results.Basic;

public static class ValueObjectResult
{
    public static ValueObjectResult<T> Pass<T>(T value)
        where T : class, IValueObject
    {
        return ValueObjectResult<T>.Pass(value);
    }
    
    public static ValueObjectResult<T> Fail<T>(string because = "")
        where T : class, IValueObject
    {
        return ValueObjectResult<T>.Fail(because);
    }
    
    public static ValueObjectResult<T> Copy<T>(ValueObjectResult<T> result)
        where T : class, IValueObject
    {
        return ValueObjectResult<T>.Create(result);
    }
}


public class ValueObjectResult<T> : TypedResult<T>
  where T : IValueObject
{
    private ValueObjectResult(T value) : base(value)
    {
    }
    
    private ValueObjectResult(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    private ValueObjectResult(ITypedResult<T> result) : base(result)
    {
    }

    internal static ValueObjectResult<T> Pass(T value)
    {
        return new ValueObjectResult<T>(value);
    }

    internal static ValueObjectResult<T> Fail(string because = "")
    {
        return new ValueObjectResult<T>(FailureType.ValueObject, because);
    }
    
    internal static ValueObjectResult<T> Create(ITypedResult<T> result)
    {
        return new ValueObjectResult<T>(result);
    }
    
    public static implicit operator ValueObjectResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator MapperResult<T>(ValueObjectResult<T> result)
    {
        return MapperResult<T>.Create(result);
    }
    
    public static implicit operator MapperResult(ValueObjectResult<T> result)
    {
        return MapperResult.Create(result);
    }
    
    public MapperResult<T> ToTypedMapperResult()
    {
        return this;
    }
    
    public MapperResult ToMapperResult()
    {
        return this;
    }
    
    public static implicit operator Response<T>(ValueObjectResult<T> result)
    {
        return Response<T>.Create(result);
    }
    
    public static implicit operator Response(ValueObjectResult<T> result)
    {
        return Response.Create(result);
    }
    
    public Response<T> ToTypedResponse()
    {
        return this;
    }
    
    public Response ToResponse()
    {
        return this;
    }
    
    public static implicit operator ServiceResult<T>(ValueObjectResult<T> result)
    {
        return ServiceResult<T>.Create(result);
    }
    
    public static implicit operator ServiceResult(ValueObjectResult<T> result)
    {
        return ServiceResult.Create(result);
    }
    
    public ServiceResult<T> ToTypedServiceResult()
    {
        return this;
    }
    
    public ServiceResult ToServiceResult()
    {
        return this;
    }
    
    public static implicit operator UseCaseResult<T>(ValueObjectResult<T> result)
    {
        return UseCaseResult<T>.Create(result);
    }
    
    public static implicit operator UseCaseResult(ValueObjectResult<T> result)
    {
        return UseCaseResult.Create(result);
    }
    
    public UseCaseResult<T> ToTypedUseCaseResult()
    {
        return this;
    }
    
    public UseCaseResult ToUseCaseResult()
    {
        return this;
    }
}

public static class ValueObjectResultExtensions
{
    public static ValueObjectResult<T> AsTypedValueObjectResult<T>(this T value)
        where T : IValueObject
    {
        return value;
    }
}