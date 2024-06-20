using FluentAssertions;
using FunctionalConcepts.Choices;
using FunctionalConcepts.Tests.Common;

namespace FunctionalConcepts.Tests.Choices
{
    [TestFixture]
    public class ChoiceMatchTests
    {
        [Test]
        public void ChoiceMatchTests_ChoiceLeftStringInt_Match_ShouldBeExecuteLeft()
        {
            // arrange
            string name = "FunctionalConcepts";
            string valueRetornedShouldBe = $"left value: {name}";
            Choice<string, int> choice = name;

            // action
            var returned = choice.Match(
                                left => $"left value: {left}",
                                right => $"right value as string: {right}");

            // asserts
            choice.IsLeft.Should().BeTrue();
            returned.Should().Be(valueRetornedShouldBe);
        }

        [Test]
        public void ChoiceMatchTests_ChoiceRight_StringInt_Match_ShouldBeExecuteRight()
        {
            // arrange
            int value = 123;
            string valueRetornedShouldBe = $"right value as string: {value}";
            Choice<string, int> choice = value;

            // action
            var returned = choice.Match(
                                left => $"left value: {left}",
                                right => $"right value as string: {right}");

            // asserts
            choice.IsRight.Should().BeTrue();
            returned.Should().Be(valueRetornedShouldBe);
        }

        [Test]
        public void ChoiceMatchTests_ChoiceBottom_StringInt_Match_ShouldBeReturnDefaultValue()
        {
            // arrange
            int? value = null;
            Choice<string, int?> choice = value;

            // action
            var returned = choice.Match(
                                left => $"left value: {left}",
                                right => $"right value as string: {right}");

            // asserts
            choice.IsBottom.Should().BeTrue();
            returned.Should().BeNull();
        }

        [Test]
        public void ChoiceMatchTests_ChoiceBottom_StringClass_Match_ShouldBeReturnDefaultValue()
        {
            // arrange
            ExampleTest? value = null;
            Choice<string, ExampleTest?> choice = value;

            // action
            var returned = choice.Match(left => $"left value", right => $"right value");

            // asserts
            choice.IsBottom.Should().BeTrue();
            returned.Should().BeNull();
        }
    }
}
