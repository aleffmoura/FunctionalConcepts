namespace FunctionalConcepts.Tests.Options;

using FluentAssertions;
using FunctionalConcepts.Errors;

using FunctionalConcepts.Options;

[TestFixture]
public class OptionMatchTests
{
    [Test]
    public async Task OptionTests_OptionMatchTests_WhenSome_MatchAsync_SomeAsync_ShouldBeOk()
    {
        // arrange
        var value = "string value";

        // action
        Option<string> option = value;

        // asserts
        var isSome = await option.MatchAsync(async some => await Task.FromResult(true), () => false);
        isSome.Should().BeTrue();
    }

    [Test]
    public async Task OptionTests_OptionMatchTests_WhenSome_MatchAsync_NoneAsync_ShouldBeOk()
    {
        // arrange
        var value = "string value";

        // action
        Option<string> option = value;

        // asserts
        var isSome = await option.MatchAsync(_ => true, async () => await Task.FromResult(true));
        isSome.Should().BeTrue();
    }

    [Test]
    public async Task OptionTests_OptionMatchTests_WhenNone_MatchAsync_NoneAsync_ShouldBeOk()
    {
        // arrange
        var value = NoneType.Value;

        // action
        Option<string> option = value;

        // asserts
        var isNone = await option.MatchAsync(_ => false, async () => await Task.FromResult(true));
        isNone.Should().BeTrue();
    }

    [Test]
    public async Task OptionTests_OptionMatchTests_WhenNone_MatchAsync_SomeAsync_ShouldBeOk()
    {
        // arrange
        var value = NoneType.Value;

        // action
        Option<string> option = value;

        // asserts
        var isNone = await option.MatchAsync(async _ => await Task.FromResult(true), () => true);
        isNone.Should().BeTrue();
    }

    [Test]
    public async Task OptionTests_OptionMatchTests_WhenNone_MatchAsync_BothCaseAsync_ShouldBeOk()
    {
        // arrange
        var none = NoneType.Value;

        // action
        Option<BaseError> option = none;

        // asserts
        var isNone = await option.MatchAsync(async some => await Task.FromResult(false), async () => await Task.FromResult(true));
        isNone.Should().BeTrue();
    }

    [Test]
    public async Task OptionTests_OptionMatchTests_WhenSome_MatchAsync_BothCaseAsync_ShouldBeOk()
    {
        // arrange
        var value = "some value";

        // action
        Option<string> option = value;

        // asserts
        var isSome = await option.MatchAsync(async some => await Task.FromResult(true), async () => await Task.FromResult(false));
        isSome.Should().BeTrue();
    }
}
