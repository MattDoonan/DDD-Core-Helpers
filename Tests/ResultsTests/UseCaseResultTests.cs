using DDD.Core.Results;
using DDD.Core.Results.ValueObjects;
using OutputTests.Extensions;
using OutputTests.TestStructures;
using Xunit;

namespace OutputTests;

public class UseCaseResultTests : BasicResultTests
{
    public override void WhenIPassTheResult_Then_TheResultIsSuccessful()
    {
        var result = UseCaseResult.Pass();
        result.AssertSuccessful(ResultLayer.UseCase);
    }

    public override void WhenIFailTheResult_Then_TheResultIsAFailure()
    {
        var result = UseCaseResult.Fail();
        result.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }

    public override void WhenIFailTheResult_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = UseCaseResult.Fail(errorMessage);
        result.AssertFailure(FailureType.Generic, ResultLayer.UseCase,1);
    }

    public override void GivenIHaveASuccessfulResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        var useCaseResult = UseCaseResult.Pass();
        var result = useCaseResult.ToResult();
        result.AssertSuccessful(ResultLayer.UseCase);
    }

    public override void GivenIHaveAFailureResult_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var useCaseResult = UseCaseResult.Fail(errorMessage);
        var result = useCaseResult.ToResult();
        result.AssertFailure(FailureType.Generic, ResultLayer.UseCase,1);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIRemoveTheValue_Then_TheResultIsConvertedSuccessfully()
    {
        const string value = "Test";    
        var useCaseResult = UseCaseResult.Pass(value);
        var result = useCaseResult.RemoveType();
        Assert.IsType<UseCaseResult>(result);
        result.AssertSuccessful(ResultLayer.UseCase);
    }

    public override void GivenIHaveAFailureResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = UseCaseResult.Fail(errorMessage);
        var copiedResult = UseCaseResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        var result = UseCaseResult.Pass();    
        var copiedResult = UseCaseResult.Copy(result);
        result.AssertEquivalent(copiedResult);
    }

    public override void GivenIHaveAFailureResult_WithoutAValue_ThenICanConvertItIntoATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = UseCaseResult.Fail(errorMessage);
        UseCaseResult<int> convertedResult = result;
        result.AssertEquivalent(convertedResult);
        Assert.ThrowsAny<Exception>(() => convertedResult.Output);
    }

    public override void GivenIHaveASuccessfulResult_WithoutAValue_WhenIConvertItIntoATypedResult_Then_AnErrorIsThrown()
    {
        var result = UseCaseResult.Pass();
        Assert.ThrowsAny<Exception>(() =>
        {
            UseCaseResult<int> _ = result;
        });
    }

    public override void GivenIHaveAManySuccessfulResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully()
    {
        var r1 = UseCaseResult.Pass();
        var r2 = UseCaseResult.Pass();
        var r3 = UseCaseResult.Pass(5);
        var mergedResult = UseCaseResult.Merge(r1, r2, r3);
        mergedResult.AssertSuccessful(ResultLayer.UseCase);
    }

    public override void GivenIHaveASomeSuccessfulAndSomeFailureResults_WhenIMergeThem_Then_TheResultIsMergedSuccessfully_AsAFailureResult()
    {
        var r1 = UseCaseResult.Pass();
        var r2 = UseCaseResult.Fail();
        var r3 = UseCaseResult.Pass(1);
        var r4 = UseCaseResult.Fail<string>("Error");
        var mergedResult = UseCaseResult.Merge(r1, r2, r3, r4);
        Assert.True(mergedResult.IsFailure);
        Assert.False(mergedResult.IsSuccessful);
        Assert.Equal(FailureType.Generic, mergedResult.PrimaryFailureType);
        Assert.Equal(ResultLayer.UseCase, mergedResult.CurrentLayer);
        Assert.Equal(3, mergedResult.ErrorMessages.Count());
    }

    public override void WhenIPassTheResult_WithAValue_Then_TheResultIsSuccessful_AndHasTheValue()
    {
        const string value = "Test";
        var result = UseCaseResult.Pass(value);
        result.AssertSuccessful(value, ResultLayer.UseCase);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = UseCaseResult.Fail<string>();
        result.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);    
    }

    public override void WhenIFailTheResult_ThatIsMeantToHaveAValue_WithAErrorMessage_Then_TheResultIsAFailure_WithTheFullErrorMessage()
    {
        const string errorMessage = "I want it to fail";
        var result = UseCaseResult.Fail<string>(errorMessage);
        result.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }

    public override void GivenIHaveAValue_WhenIImplyTheResult_Then_TheResultIsImportedSuccessfully()
    {
        const string value = "Test";
        UseCaseResult<string> convertedResult = value;
        convertedResult.AssertSuccessful(value, ResultLayer.UseCase);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string value = "Test";
        var useCaseResult = UseCaseResult.Pass(value);
        var result = useCaseResult.ToTypedResult();
        Assert.IsType<Result<string>>(result);
        result.AssertSuccessful(value, ResultLayer.UseCase);
    }

    public override void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_WhenIConvertItIntoAResult_Then_TheResultIsConvertedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var useCaseResult = UseCaseResult.Fail<string>(errorMessage);
        var result = useCaseResult.ToTypedResult();
        Assert.IsType<Result<string>>(result);
        result.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }

    public override void GivenIHaveAFailureResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const string errorMessage = "I want it to fail";
        var result = UseCaseResult.Fail<string>(errorMessage);
        var copiedResult = UseCaseResult.Copy(result);
        result.AssertEquivalent( copiedResult);
    }

    public override void GivenIHaveASuccessfulResult_WithAValue_WhenICopyIt_Then_TheResultIsCopiedSuccessfully()
    {
        const byte value = 20;
        var result = UseCaseResult.Pass(value);    
        var copiedResult = UseCaseResult.Copy(result);
        result.AssertEquivalent( copiedResult);
    }

    public override void WhenIHaveAValue_Then_ItCanBeImplicitlyConvertedIntoAResult()
    {
        const byte value = 10;
        UseCaseResult<byte> result = value;
        result.AssertSuccessful(value, ResultLayer.UseCase);
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvariantViolation_Then_TheResultIsAFailure()
    {
        var result = UseCaseResult.InvariantViolation();
        result.AssertFailure(FailureType.InvariantViolation, ResultLayer.UseCase, 1);    
    }
    
    [Fact]
    public void WhenIFailTheResult_BecauseOfAnInvariantViolation_ThatIsMeantToHaveAValue_Then_TheResultIsAFailure()
    {
        var result = UseCaseResult.InvariantViolation<int>();
        result.AssertFailure(FailureType.InvariantViolation, ResultLayer.UseCase, 1);    
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_Then_ItCanBeConvertedToATypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = UseCaseResult.Fail(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase,1);
    }
    
    [Fact]
    public void GivenIHaveAFailureResult_ThatIsMeantToHaveAValue_Then_ItCanBeConvertedToADifferentTypedResult()
    {
        const string errorMessage = "I want it to fail";
        var result = UseCaseResult.Fail<int>(errorMessage);    
        var convertedResult = result.ToTypedUseCaseResult<string>();
        convertedResult.AssertFailure(FailureType.Generic, ResultLayer.UseCase, 1);
    }
}