using Outputs.Results.Base.Abstract;
using Outputs.Results.Base.Enums;
using Outputs.Results.Base.Interfaces;

namespace Outputs.Results.Advanced;

public class Response : CoreResult<Response>, IResultFactory<Response>
{
    private Response(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private Response()
    {
    }
    
    private Response(FailureType failureType, string because) : base(failureType, FailedLayer.Infrastructure, because)
    {
    }
    
    public static Response Pass()
    {
        return new Response();
    }

    public static Response Fail(string because = "")
    {
        return new Response(FailureType.Generic, because);
    }
    
    public static Response NotFound(string because = "")
    {
        return new Response(FailureType.NotFound, because);
    }
    
    public static Response AlreadyExists(string because = "")
    {
        return new Response(FailureType.AlreadyExists, because);
    }
    
    public static Response InvalidRequest(string because = "")
    {
        return new Response(FailureType.InvalidRequest, because);
    }
    
    public static Response OperationTimout(string because = "")
    {
        return new Response(FailureType.OperationTimeout, because);
    }
    
    public static Response<T> Pass<T>(T value)
    {
        return Response<T>.Pass(value);
    }
    
    public static Response<T> Fail<T>(string because = "")
    {
        return Response<T>.Fail(FailureType.Generic, because);
    }
    
    public static Response<T> NotFound<T>(string because = "")
    {
        return Response<T>.Fail(FailureType.NotFound, because);
    }
    
    public static Response<T> AlreadyExists<T>(string because = "")
    {
        return Response<T>.Fail(FailureType.AlreadyExists, because);
    }
    
    public static Response<T> InvalidRequest<T>(string because = "")
    {
        return Response<T>.Fail(FailureType.InvalidRequest, because);
    }
    
    public static Response<T> OperationTimout<T>(string because = "")
    {
        return Response<T>.Fail(FailureType.OperationTimeout, because);
    }
    
    public static Response Copy(Response result)
    {
        return Create(result);
    }
    
    public static Response<T> Copy<T>(Response<T> result)
    {
        return Response<T>.Create(result);
    }
    
    public static implicit operator ServiceResult(Response result)
    {
        return ServiceResult.Create(result);
    }
    
    public ServiceResult ToServiceResult()
    {
        return this;
    }
    
    public static implicit operator UseCaseResult(Response result)
    {
        return UseCaseResult.Create(result);
    }
    
    public UseCaseResult ToUseCaseResult()
    {
        return this;
    }

    internal static Response Create(IResultStatus result)
    {
        return new Response(result);
    }
}

public class Response<T>  : CoreResult<T, Response>
{
    private Response(T value) : base(value)
    {
    }
    
    private Response(FailureType failureType, string because) : base(failureType, FailedLayer.Infrastructure, because)
    {
    }
    
    private Response(ITypedResult<T> result) : base( result)
    {
    }
    
    internal static Response<T> Pass(T value)
    {
        return new Response<T>(value);
    }

    internal static Response<T> Fail(FailureType failureType, string because = "")
    {
        return new Response<T>(failureType, because);
    }
    
    internal static Response<T> Create(ITypedResult<T> result)
    {
        if (result.FailedLayer == FailedLayer.None)
        {
            return new Response<T>(result)
            {
                FailedLayer = FailedLayer.Infrastructure
            };   
        }
        return new Response<T>(result);
    }
    
    public static implicit operator Response<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator ServiceResult<T>(Response<T> result)
    {
        return ServiceResult<T>.Create(result);
    }
    
    public static implicit operator ServiceResult(Response<T> result)
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
    
    public static implicit operator UseCaseResult<T>(Response<T> result)
    {
        return UseCaseResult<T>.Create(result);
    }
    
    public static implicit operator UseCaseResult(Response<T> result)
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