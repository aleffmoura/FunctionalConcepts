namespace FunctionalConcepts.Tests.Options;

using FluentAssertions;
using FunctionalConcepts.Options;

[TestFixture]
public class OptionMapTests
{
    [Test]
    public void OptionMapTests_Map_StringToInt_ShouldBeSuccess()
    {
        // arrange
        Option<string> maybe = "18";

        // action
        Option<int> option = maybe.Map(int.Parse);

        // asserts
        option.IsSome.Should().BeTrue();
        option.Then(val => val.Should().Be(18));
    }

    [Test]
    public async Task OptionMapTests_MapAsync_StringToInt_ShouldBeSuccess()
    {
        // arrange
        Option<string> maybe = "18";

        // action
        Option<int> option = await maybe.MapAsync(async val => await Task.FromResult(int.Parse(val)));

        // asserts
        option.IsSome.Should().BeTrue();
        option.Then(val => val.Should().Be(18));
    }

    [Test]
    public void OptionMapTests_Map_StringToInt_WhenNone_ShouldBeSuccess()
    {
        // arrange
        Option<string> maybe = NoneType.Value;

        // action
        Option<int> option = maybe.Map(int.Parse);

        // asserts
        option.IsNone.Should().BeTrue();
    }

    [Test]
    public async Task OptionMapTests_MapAsync_StringToInt_WhenNone_ShouldBeSuccess()
    {
        // arrange
        Option<string> maybe = NoneType.Value;

        // action
        Option<int> option = await maybe.MapAsync(async val => await Task.FromResult(int.Parse(val)));

        // asserts
        option.IsNone.Should().BeTrue();
    }
}
