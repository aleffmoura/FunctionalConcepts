namespace FunctionalConcepts.Tests.Options;

using FluentAssertions;
using FunctionalConcepts.Errors;

using FunctionalConcepts.Options;

[TestFixture]
public class OptionIfTests
{
    [Test]
    public void OptionIfTests_IfSome_WhenSome_ShouldBeOk()
    {
        // arrange
        var msg = "some value";
        Option<string> option = msg;
        string? response = null;

        // action
        option.Then(value =>
        {
            response = value;
        });

        // asserts
        option.IsSome.Should().BeTrue();
        option.IsNone.Should().BeFalse();
        response.Should().NotBeNull();
        response.Should().Be(msg);
    }

    [Test]
    public void OptionIfTests_IfSome_WhenNone_ShouldBeOk()
    {
        // arrange
        Option<string> option = NoneType.Value;
        string? response = null;

        // action
        option.Then(value =>
        {
            response = value;
        });

        // asserts
        option.IsSome.Should().BeFalse();
        option.IsNone.Should().BeTrue();
        response.Should().BeNull();
    }

    [Test]
    public void OptionIfTests_IfNone_WhenNone_ShouldBeOk()
    {
        // arrange
        var none = NoneType.Value;
        Option<BaseError> option = none;
        var msg = "is none value";
        string? response = null;

        // action
        option.Else(() =>
        {
            response = msg;
        });

        // asserts
        option.IsSome.Should().BeFalse();
        option.IsNone.Should().BeTrue();
        response.Should().NotBeNull();
    }

    [Test]
    public void OptionIfTests_IfNone_WhenSome_ShouldBeOk()
    {
        // arrange
        Option<string> option = "some value";
        var msg = "value";
        string? response = null;

        // action
        option.Else(() =>
        {
            response = msg;
        });

        // asserts
        option.IsSome.Should().BeTrue();
        option.IsNone.Should().BeFalse();
        response.Should().BeNull();
    }

    [Test]
    public async Task OptionIfTests_IfSomeAsync_WhenSome_ShouldBeOk()
    {
        // arrange
        var msg = "some value";
        Option<string> option = msg;
        string? response = null;

        // action
        _ = await option.ThenAsync(async value =>
        {
            response = await Task.FromResult(value);
        });

        // asserts
        option.IsSome.Should().BeTrue();
        option.IsNone.Should().BeFalse();
        response.Should().NotBeNull();
        response.Should().Be(msg);
    }

    [Test]
    public async Task OptionIfTests_IfSomeAsync_WhenNone_ShouldBeOk()
    {
        // arrange
        Option<string> option = NoneType.Value;
        string? response = null;

        // action
        _ = await option.ThenAsync(async value =>
        {
            response = await Task.FromResult(value);
        });

        // asserts
        option.IsSome.Should().BeFalse();
        option.IsNone.Should().BeTrue();
        response.Should().BeNull();
    }

    [Test]
    public async Task OptionIfTests_IfNoneAsync_WhenNone_ShouldBeOk()
    {
        // arrange
        var none = NoneType.Value;
        Option<BaseError> option = none;
        var msg = "is none value";
        string? response = null;

        // action
        _ = await option.ElseAsync(async () =>
        {
            response = await Task.FromResult(msg);
        });

        // asserts
        option.IsSome.Should().BeFalse();
        option.IsNone.Should().BeTrue();
        response.Should().NotBeNull();
    }

    [Test]
    public async Task OptionIfTests_IfNoneAsync_WhenSome_ShouldBeOk()
    {
        // arrange
        Option<string> option = "some value";
        var msg = "value";
        string? response = null;

        // action
        _ = await option.ElseAsync(async () =>
        {
            response = await Task.FromResult(msg);
        });

        // asserts
        option.IsSome.Should().BeTrue();
        option.IsNone.Should().BeFalse();
        response.Should().BeNull();
    }
}
