using FluentAssertions;
using FunctionalConcepts.Choices;

namespace FunctionalConcepts.Tests.Choices
{
    [TestFixture]
    public class ChoiceIfAsyncTests
    {
        [Test]
        public async Task ChoiceIfAsyncTests_ChoiceLeft_IfLeft_ShouldExecutePassedFunction()
        {
            // arrange
            bool isLeft = false;
            string name = "FunctionalConcepts";
            Choice<string, int> choice = name;

            // action
            _ = await choice.ThenLeftAsync(async val =>
            {
                isLeft = true;
                await Task.Delay(1);
            });

            // asserts
            isLeft.Should().BeTrue();
        }

        [Test]
        public async Task ChoiceIfAsyncTests_ChoiceLeft_IfRight_Should_NotExecutePassedFunction()
        {
            // arrange
            bool isLeft = false;
            string name = "FunctionalConcepts";
            Choice<string, int> choice = name;

            // action
            _ = await choice.ThenRightAsync(async val =>
            {
                isLeft = true;
                await Task.Delay(1);
            });

            // asserts
            isLeft.Should().BeFalse();
        }

        [Test]
        public async Task ChoiceIfAsyncTests_ChoiceRight_IfRight_Should_ExecutePassedFunction()
        {
            // arrange
            bool isRight = false;
            int age = 18;
            Choice<string, int> choice = age;

            // action
            _ = await choice.ThenRightAsync(async val =>
            {
                isRight = true;
                await Task.Delay(1);
            });

            // asserts
            isRight.Should().BeTrue();
        }

        [Test]
        public async Task ChoiceIfAsyncTests_ChoiceRight_IfLeft_Should_NotExecutePassedFunction()
        {
            // arrange
            bool isRight = false;
            int age = 18;
            Choice<string, int> choice = age;

            // action
            _ = await choice.ThenLeftAsync(async val =>
            {
                isRight = true;
                await Task.Delay(1);
            });

            // asserts
            isRight.Should().BeFalse();
        }

        [Test]
        public async Task ChoiceIfAsyncTests_ChoiceRight_IfBottom_Should_ExecutePassedFunction()
        {
            // arrange
            bool isBottom = false;
            int? age = null;
            Choice<string, int?> choice = age;

            // action
            _ = await choice.ElseAsync(async val =>
            {
                isBottom = true;
                await Task.Delay(1);
            });

            // asserts
            isBottom.Should().BeTrue();
        }

        [Test]
        public async Task ChoiceIfAsyncTests_ChoiceRight_IfBottom_Should_NotExecutePassedFunction()
        {
            // arrange
            bool isBottom = false;
            int age = 18;
            Choice<string, int> choice = age;

            // action
            _ = await choice.ElseAsync(async val =>
            {
                isBottom = true;
                await Task.Delay(1);
            });

            // asserts
            isBottom.Should().BeFalse();
        }

        [Test]
        public async Task ChoiceIfAsyncTests_ChoiceLeft_IfBottom_Should_NotExecutePassedFunction()
        {
            // arrange
            bool isBottom = false;
            string age = "18";
            Choice<string, int> choice = age;

            // action
            _ = await choice.ElseAsync(async val =>
            {
                isBottom = true;
                await Task.Delay(1);
            });

            // asserts
            isBottom.Should().BeFalse();
        }
    }
}
