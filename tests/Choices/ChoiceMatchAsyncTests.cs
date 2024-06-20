using FluentAssertions;
using FunctionalConcepts.Choices;
using FunctionalConcepts.Tests.Common;

namespace FunctionalConcepts.Tests.Choices
{
    [TestFixture]
    public class ChoiceMatchAsyncTests
    {
        [Test]
        public async Task ChoiceMatchAsyncTests_ChoiceLeftStringInt_BothAsync_Match_ShouldBeExecuteLeft()
        {
            // arrange
            string name = "FunctionalConcepts";
            string valueRetornedShouldBe = $"left value: {name}";
            Choice<string, int> choice = name;

            // action
            var returned = await choice.MatchAsync(
                                async left => await Task.FromResult($"left value: {left}"),
                                async right => await Task.FromResult($"right value as string: {right}"));

            // asserts
            choice.IsLeft.Should().BeTrue();
            returned.Should().Be(valueRetornedShouldBe);
        }

        [Test]
        public async Task ChoiceMatchAsyncTests_ChoiceLeftStringInt_Match_ShouldBeExecuteLeft()
        {
            // arrange
            string name = "FunctionalConcepts";
            string valueRetornedShouldBe = $"left value: {name}";
            Choice<string, int> choice = name;

            // action
            var returned = await choice.MatchAsync(
                                async left => await Task.FromResult($"left value: {left}"),
                                right => $"right value as string: {right}");

            // asserts
            choice.IsLeft.Should().BeTrue();
            returned.Should().Be(valueRetornedShouldBe);
        }

        [Test]
        public async Task ChoiceMatchAsyncTests_ChoiceRight_IntString_Match_ShouldBeExecuteRight()
        {
            // arrange
            string name = "FunctionalConcepts";
            string valueRetornedShouldBe = $"right value as string: {name}";
            Choice<int, string> choice = name;

            // action
            var returned = await choice.MatchAsync(
                                async left => await Task.FromResult($"left value: {left}"),
                                right => $"right value as string: {right}");

            // asserts
            choice.IsRight.Should().BeTrue();
            returned.Should().Be(valueRetornedShouldBe);
        }

        [Test]
        public async Task ChoiceMatchAsyncTests_ChoiceBottom_IntString_Match_ShouldBeDefaultValue()
        {
            // arrange
            string? name = null;
            Choice<int, string?> choice = name;

            // action
            var returned = await choice.MatchAsync(
                                async left => await Task.FromResult($"left value: {left}"),
                                right => $"right value as string: {right}");

            // asserts
            choice.IsBottom.Should().BeTrue();
            returned.Should().BeNull();
        }

        [Test]
        public async Task ChoiceMatchAsyncTests_ChoiceBottom_StringInt_Match_ShouldBeDefaultValue()
        {
            // arrange
            string? name = null;
            Choice<string?, int> choice = name;

            // action
            var returned = await choice.MatchAsync(
                                async left => await Task.FromResult($"left value: {left}"),
                                right => $"right value as string: {right}");

            // asserts
            choice.IsBottom.Should().BeTrue();
            returned.Should().BeNull();
        }

        [Test]
        public async Task ChoiceMatchAsyncTests_ChoiceRight_StringInt_Match_ShouldBeExecuteRight()
        {
            // arrange
            int value = 123;
            string valueRetornedShouldBe = $"right value as string: {value}";
            Choice<string, int> choice = value;

            // action
            var returned = await choice.MatchAsync(
                                left => $"left value: {left}",
                                async right => await Task.FromResult($"right value as string: {right}"));

            // asserts
            choice.IsRight.Should().BeTrue();
            returned.Should().Be(valueRetornedShouldBe);
        }

        [Test]
        public async Task ChoiceMatchAsyncTests_ChoiceBottom_StringInt_Match_ShouldBeDefault()
        {
            // arrange
            int? value = null;
            Choice<string, int?> choice = value;

            // action
            var returned = await choice.MatchAsync(
                                left => $"left value: {left}",
                                async right => await Task.FromResult($"right value as string: {right}"));

            // asserts
            choice.IsBottom.Should().BeTrue();
            returned.Should().BeNull();
        }

        [Test]
        public async Task ChoiceMatchAsyncTests_ChoiceBottom_IntString_Match_ShouldBeDefault()
        {
            // arrange
            int? value = null;
            Choice<int?, string> choice = value;

            // action
            var returned = await choice.MatchAsync(
                                left => $"left value: {left}",
                                async right => await Task.FromResult($"right value as string: {right}"));

            // asserts
            choice.IsBottom.Should().BeTrue();
            returned.Should().BeNull();
        }

        [Test]
        public async Task ChoiceMatchAsyncTests_ChoiceLeft_IntString_Match_ShouldBeExecuteLeft()
        {
            // arrange
            int value = 123;
            string valueRetornedShouldBe = $"left value: {value}";
            Choice<int, string> choice = value;

            // action
            var returned = await choice.MatchAsync(
                                left => $"left value: {left}",
                                async right => await Task.FromResult($"right value as string: {right}"));

            // asserts
            choice.IsLeft.Should().BeTrue();
            returned.Should().Be(valueRetornedShouldBe);
        }

        [Test]
        public async Task ChoiceMatchAsyncTests_ChoiceBottom_StringInt_Match_ShouldBeReturnDefaultValue()
        {
            // arrange
            int? value = null;
            Choice<string, int?> choice = value;

            // action
            var returned = await choice.MatchAsync(
                                left => Task.FromResult($"left value: {left}"),
                                right => Task.FromResult($"right value as string: {right}"));

            // asserts
            choice.IsBottom.Should().BeTrue();
            returned.Should().BeNull();
        }

        [Test]
        public async Task ChoiceMatchAsyncTests_ChoiceBottom_StringClass_Match_ShouldBeReturnDefaultValue()
        {
            // arrange
            ExampleTest? value = null;
            Choice<string, ExampleTest?> choice = value;

            // action
            var returned = await choice.MatchAsync(
                async left => await Task.FromResult($"left value"),
                async right => await Task.FromResult($"right value"));

            // asserts
            choice.IsBottom.Should().BeTrue();
            returned.Should().BeNull();
        }

        [Test]
        public async Task ChoiceMatchAsyncTests_ChoiceBottom_ClassInt_Match_ShouldBeReturnDefaultValue()
        {
            // arrange
            ExampleTest? value = null;
            Choice<ExampleTest?, int> choice = value;

            // action
            var returned = await choice.MatchAsync(
                async left => await Task.FromResult($"left value"),
                async right => await Task.FromResult($"right value"));

            // asserts
            choice.IsBottom.Should().BeTrue();
            returned.Should().BeNull();
        }
    }
}
