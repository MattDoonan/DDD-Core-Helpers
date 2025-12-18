using DDD.Core.Results.Extensions;
using DDD.Core.Results.ValueObjects;
using Xunit;

namespace Results.Tests.Extensions;

public class FailureTypeExtensionTests
{

    [Theory, MemberData(nameof(NonTypedMessagesTestCases))]
    public void ToMessage_WithNoType_ShouldReturn_MessageWithoutTypes(FailureType failureType, string expectedMessage)
    {
        Assert.Equal(expectedMessage, failureType.ToMessage());
    }
    
    [Theory, MemberData(nameof(NonTypedMessagesTestCases))]
    public void ToMessage_WithNullType_ShouldReturn_MessagesWithoutTypes(FailureType failureType, string expectedMessage)
    {
        Assert.Equal(expectedMessage, failureType.ToMessage(null));
    }
    
    [Theory, MemberData(nameof(TypedMessagesTestCases))]
    public void ToMessage_WithType_ShouldReturn_MessageWithTypes(FailureType failureType, Type objectType, string expectedMessage)
    {
        Assert.Equal(expectedMessage, failureType.ToMessage(objectType));
    }


    public static IEnumerable<object[]> NonTypedMessagesTestCases =>
    [
        [FailureType.None, "Result was successful"],
        [FailureType.Generic, "Result was a failure"],
        [FailureType.InvariantViolation, "An unexpected failure occured"],
        [FailureType.OperationCancelled, "The operation was cancelled"],
        [FailureType.OperationTimeout, "The operation timed out"],
        [FailureType.ConcurrencyViolation, "A Concurrency violation occured"],
        [FailureType.InvalidRequest, "Invalid request"],
        [FailureType.InvalidInput, "Invalid input"],
        [FailureType.DomainViolation, "The application's business logic was violated"],
        [FailureType.NotAllowed, "Operation not permitted"],
        [FailureType.NotFound, "Resource not found"],
        [FailureType.AlreadyExists, "Resource already exists"],
        [(FailureType)1000, "Unknown failure"]
    ];
    
    public static IEnumerable<object[]> TypedMessagesTestCases =>
    [
        [FailureType.None, typeof(string), $"Result retrieving {nameof(String)} was successful"],
        [FailureType.Generic, typeof(int), $"Failed to get {nameof(Int32)}"],
        [FailureType.InvariantViolation, typeof(bool), $"An unexpected failure occured when retrieving {nameof(Boolean)}"],
        [FailureType.OperationCancelled, typeof(string), $"The operation for {nameof(String)} was cancelled"],
        [FailureType.OperationTimeout, typeof(byte), $"The operation for {nameof(Byte)} timed out"],
        [FailureType.ConcurrencyViolation, typeof(short), $"A Concurrency violation occured when retrieving {nameof(Int16)}"],
        [FailureType.InvalidRequest, typeof(int), $"Invalid request to retrieve {nameof(Int32)}"],
        [FailureType.InvalidInput, typeof(object), $"Invalid input trying to retrieve {nameof(Object)}"],
        [FailureType.DomainViolation, typeof(char), $"The application's business logic was violated trying to retrieve {nameof(Char)}"],
        [FailureType.NotAllowed, typeof(string), $"Operation to get {nameof(String)} is not permitted"],
        [FailureType.NotFound, typeof(nint), $"{nameof(IntPtr)} not found"],
        [FailureType.AlreadyExists, typeof(decimal), $"{nameof(Decimal)} resource already exists"],
        [(FailureType)1000, typeof(float), $"Unknown failure to retrieve {nameof(Single)}"]
    ];
}