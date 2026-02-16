namespace FunctionalConcepts.Tests.Options;

using FluentAssertions;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Options;

[TestFixture]
public class OptionFailWhenTests
{
    [Test]
    public void OptionFailWhenTests_FailWhenNone_ShouldNotFoundError_WithDefaultMessage()
    {
        // arrange
        var error = "Error message";
        Option<string> option = Option.None;

        // action
        var response = option.FailWhen(value => value.Length < 5, (InvalidObjectError)error);

        // asserts
        option.IsSome.Should().BeFalse();
        option.IsNone.Should().BeTrue();
        response.IsSuccess.Should().BeFalse();
        response.IsFail.Should().BeTrue();
        response.Else(fail =>
        {
            var err = fail.Should().BeOfType<NotFoundError>().Subject;
            err.Message.Should().Be("object not found");
        });
    }

    [Test]
    public void OptionFailWhenTests_FailWhenNone_ShouldNotFoundError_WithMessage()
    {
        // arrange
        var messageWhenNone = "registry not found";
        var error = "Error message";
        Option<string> option = Option.None;

        // action
        var response = option.FailWhen(value => value.Length < 5, (InvalidObjectError)error, messageWhenNone);

        // asserts
        option.IsSome.Should().BeFalse();
        option.IsNone.Should().BeTrue();
        response.IsSuccess.Should().BeFalse();
        response.IsFail.Should().BeTrue();
        response.Else(fail =>
        {
            var err = fail.Should().BeOfType<NotFoundError>().Subject;
            err.Message.Should().Be(messageWhenNone);
        });
    }

    [Test]
    public void OptionFailWhenTests_FailWhenSome_ShouldBeOk()
    {
        // arrange
        var error = "Error message";
        var msg = "somevalue";
        Option<string> option = msg;

        // action
        var response = option.FailWhen(value => value.Length < 5, (InvalidObjectError)error);

        // asserts
        option.IsSome.Should().BeTrue();
        option.IsNone.Should().BeFalse();
        response.IsSuccess.Should().BeTrue();
        response.Then(value =>
        {
            value.Should().Be(msg);
        });
    }

    [Test]
    public void OptionFailWhenTests_FailWhenSome_ButFailInLength_ShouldReturnInvalidObjectError()
    {
        // arrange
        var error = "Error message";
        Option<string> option = "some";
        var invalidError = (InvalidObjectError)error;

        // action
        var response = option.FailWhen(value => value.Length < 5, invalidError);

        // asserts
        option.IsSome.Should().BeTrue();
        option.IsNone.Should().BeFalse();
        response.IsFail.Should().BeTrue();
        response.Else(value =>
        {
            value.Should().Be(invalidError);
            value.Message.Should().Be(error);
        });
    }
}
