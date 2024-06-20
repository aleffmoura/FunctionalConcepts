namespace FunctionalConcepts.Tests.Results;

using FluentAssertions;
using FunctionalConcepts;
using FunctionalConcepts.Constants;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;

[TestFixture]
public class ResultTests
{
    [Test]
    public void ResultTests_Result_IsBottom_ShouldBeFail()
    {
        // arrange && action
        Result<Success> result = new();
        static bool VerifyFail(BaseError err)
        {
            err.Code.Should().Be(500);
            err.Message.Should().Be(ErrorConstant.RESULT_IS_BOTTOM);
            return true;
        }

        // asserts
        result.IsSuccess.Should().BeFalse();
        result.IsFail.Should().BeTrue();
        var isFail = result.Match(some => false, VerifyFail);
        isFail.Should().BeTrue();
    }

    [Test]
    public void ResultTests_Result_Null_ShouldBeFail()
    {
        // arrange && action
        string? msg = null;
        Result<string?> result = msg;
        static bool VerifyFail(BaseError err)
        {
            err.Code.Should().Be(500);
            err.Message.Should().Be(ErrorConstant.RESULT_IS_BOTTOM);
            return true;
        }

        // asserts
        result.IsSuccess.Should().BeFalse();
        result.IsFail.Should().BeTrue();
        var isFail = result.Match(some => false, VerifyFail);
        isFail.Should().BeTrue();
    }

    [Test]
    public void ResultTests_Implicit_Operator_Success_ShouldBeOk()
    {
        // arrange
        Success unit = Result.Success;

        // action
        Result<Success> result = unit;

        // asserts
        result.IsSuccess.Should().BeTrue();
        result.IsFail.Should().BeFalse();
    }

    [Test]
    public void ResultTests_Implicit_Operator_Failure_ShouldBeOk()
    {
        // arrange
        var code = 404;
        var msg = "not found";
        BaseError resultError = (code, msg);

        // action
        Result<Success> result = resultError;

        // asserts
        result.IsSuccess.Should().BeFalse();
        result.IsFail.Should().BeTrue();
    }

    [Test]
    public void ResultTests_Match_Success_ShouldBeOk()
    {
        // arrange
        Success unit = Result.Success;
        static bool VerifySome(Success unit)
        {
            unit.Should().Be(unit);
            return true;
        }

        // action
        Result<Success> result = Result.Success;

        // asserts
        result.IsSuccess.Should().BeTrue();
        result.IsFail.Should().BeFalse();
        var isSucc = result.Match(VerifySome, error => false);
        isSucc.Should().BeTrue();
    }

    [Test]
    public void ResultTests_Match_Failure_ShouldBeOk()
    {
        // arrange
        var code = 404;
        var msg = "not found";
        BaseError resultError = (code, msg);
        bool VerifyFail(BaseError err)
        {
            resultError.Should().Be(resultError);
            resultError.Code.Should().Be(code);
            resultError.Message.Should().Be(msg);
            return true;
        }

        // action
        Result<Success> result = resultError;

        // asserts
        result.IsSuccess.Should().BeFalse();
        result.IsFail.Should().BeTrue();
        var isFail = result.Match(some => false, VerifyFail);
        isFail.Should().BeTrue();
    }

    [Test]
    public void ResultTests_AsOptionalSuccess_ShouldBeOk()
    {
        // arrange
        Success unit = Result.Success;
        Result<Success> result = unit;

        // action
        var success = result.AsOption;
        var failure = result.AsOptionFail;

        // asserts
        success.IsSome.Should().BeTrue();
        success.IsNone.Should().BeFalse();

        failure.IsSome.Should().BeFalse();
        failure.IsNone.Should().BeTrue();
    }

    [Test]
    public void ResultTests_AsOptionalFailure_ShouldBeOk()
    {
        // arrange
        BaseError resultError = (404, "not found");
        Result<Success> result = resultError;

        // action
        var failure = result.AsOptionFail;
        var success = result.AsOption;

        // asserts
        failure.IsSome.Should().BeTrue();
        failure.IsNone.Should().BeFalse();

        success.IsSome.Should().BeFalse();
        success.IsNone.Should().BeTrue();
    }
}
