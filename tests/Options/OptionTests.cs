namespace FunctionalConcepts.Tests.Options;

using FluentAssertions;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Options;
using FunctionalConcepts.Results;
using FunctionalConcepts.Tests.Common;

[TestFixture]
public class OptionTests
{
    [Test]
    public void OptionTests_Implicit_Operator_NoneType_ShouldBeSuccess()
    {
        // arrange
        var none = Option.None;

        // action
        Option<BaseError> option = none;

        // asserts
        option.IsSome.Should().BeFalse();
        option.IsNone.Should().BeTrue();
    }

    [Test]
    public void OptionTests_Implicit_Operator_Null_ShouldBeSuccess()
    {
        // arrange
        string? none = null;

        // action
        Option<string?> option = none;

        // asserts
        option.IsSome.Should().BeFalse();
        option.IsNone.Should().BeTrue();
    }

    [Test]
    public void OptionTests_Implicit_Operator_NullClass_ShouldBeSuccess()
    {
        // arrange
        ExampleTest? none = null;

        // action
        Option<ExampleTest?> option = none;

        // asserts
        option.IsSome.Should().BeFalse();
        option.IsNone.Should().BeTrue();
    }

    [Test]
    public void OptionTests_Implicit_Operator_ShouldBeSuccess()
    {
        // arrange
        var code = 404;
        var msg = "not found";
        (int Code, string Msg) tuple = (code, msg);
        BaseError resultError = tuple;

        // action
        Option<BaseError> option = resultError;

        // asserts
        option.IsSome.Should().BeTrue();
        option.IsNone.Should().BeFalse();
    }

    [Test]
    public void OptionTests_MatchWithSome_ShouldBeSuccess()
    {
        // arrange
        var code = 404;
        var msg = "not found";
        (int Code, string Msg) tuple = (code, msg);
        BaseError resultError = tuple;

        var action = (BaseError result) =>
        {
            result.Should().Be(resultError);
            return Result.Success;
        };

        // action
        Option<BaseError> option = resultError;

        // asserts
        option.IsSome.Should().BeTrue();
        option.IsNone.Should().BeFalse();
        option.Match(action, () => Result.Success);
    }

    [Test]
    public void OptionTests_MatchWitNone_ShouldBeSuccess()
    {
        // arrange
        var none = Option.None;

        // action
        Option<BaseError> option = none;

        // asserts
        option.IsSome.Should().BeFalse();
        option.IsNone.Should().BeTrue();
        var isNone = option.Match(some => false, () => true);
        isNone.Should().BeTrue();
    }
}
