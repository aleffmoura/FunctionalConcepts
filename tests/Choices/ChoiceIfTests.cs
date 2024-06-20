using FluentAssertions;
using FunctionalConcepts.Choices;

namespace FunctionalConcepts.Tests.Choices
{
    [TestFixture]
    public class ChoiceIfTests
    {
        [Test]
        public void ChoiceIfTests_ChoiceLeft_IfLeft_ShouldExecutePassedFunction()
        {
            // arrange
            bool isLeft = false;
            string name = "FunctionalConcepts";
            Choice<string, int> choice = name;

            // action
            _ = choice.ThenLeft(val =>
            {
                isLeft = true;
            });

            // asserts
            isLeft.Should().BeTrue();
        }

        [Test]
        public void ChoiceIfTests_ChoiceLeft_IfRight_Should_NotExecutePassedFunction()
        {
            // arrange
            bool isLeft = false;
            string name = "FunctionalConcepts";
            Choice<string, int> choice = name;

            // action
            _ = choice.ThenRight(val =>
            {
                isLeft = true;
            });

            // asserts
            isLeft.Should().BeFalse();
        }

        [Test]
        public void ChoiceIfTests_ChoiceRight_IfRight_Should_ExecutePassedFunction()
        {
            // arrange
            bool isRight = false;
            int age = 18;
            Choice<string, int> choice = age;

            // action
            _ = choice.ThenRight(val =>
            {
                isRight = true;
            });

            // asserts
            isRight.Should().BeTrue();
        }

        [Test]
        public void ChoiceIfTests_ChoiceRight_IfLeft_Should_NotExecutePassedFunction()
        {
            // arrange
            bool isRight = false;
            int age = 18;
            Choice<string, int> choice = age;

            // action
            _ = choice.ThenLeft(val =>
            {
                isRight = true;
            });

            // asserts
            isRight.Should().BeFalse();
        }

        [Test]
        public void ChoiceIfTests_ChoiceRight_IfBottom_Should_ExecutePassedFunction()
        {
            // arrange
            bool isBottom = false;
            int? age = null;
            Choice<string, int?> choice = age;

            // action
            _ = choice.Else(val =>
            {
                isBottom = true;
            });

            // asserts
            isBottom.Should().BeTrue();
        }

        [Test]
        public void ChoiceIfTests_ChoiceRight_IfBottom_Should_NotExecutePassedFunction()
        {
            // arrange
            bool isBottom = false;
            int age = 18;
            Choice<string, int> choice = age;

            // action
            _ = choice.Else(val =>
            {
                isBottom = true;
            });

            // asserts
            isBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceIfTests_ChoiceLeft_IfBottom_Should_NotExecutePassedFunction()
        {
            // arrange
            bool isBottom = false;
            string age = "18";
            Choice<string, int> choice = age;

            // action
            _ = choice.Else(val =>
            {
                isBottom = true;
            });

            // asserts
            isBottom.Should().BeFalse();
        }

        [Test]
        public async Task ChoiceIfTests_ChoiceRight_IfRightAsync_FunctionThowsError_ShouldReturnBottom()
        {
            // arrange
            int age = 18;
            Choice<string, int> choice = age;

            // action
            var response = await choice.ThenRightAsync(val =>
            {
                throw new Exception("error");
            });

            // asserts
            response.IsBottom.Should().BeTrue();
            response.IsRight.Should().BeFalse();
            response.IsLeft.Should().BeFalse();
        }

        [Test]
        public void ChoiceIfTests_ChoiceRight_IfRight_FunctionThowsError_ShouldReturnBottom()
        {
            // arrange
            int age = 18;
            Choice<string, int> choice = age;
            static void Action(int v) => throw new Exception("error");

            // action
            var response = choice.ThenRight(Action);

            // asserts
            response.IsBottom.Should().BeTrue();
            response.IsRight.Should().BeFalse();
            response.IsLeft.Should().BeFalse();
        }
    }
}
