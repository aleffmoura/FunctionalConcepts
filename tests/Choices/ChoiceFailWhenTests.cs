using FluentAssertions;
using FunctionalConcepts.Choices;
using FunctionalConcepts.Errors;

namespace FunctionalConcepts.Tests.Choices
{
    [TestFixture]
    public class ChoiceFailWhenTests
    {
        [Test]
        public void ChoiceFailWhenTests_ChoiceLeft_StringInt_ShouldBeNotFail()
        {
            // arrange
            string name = "FunctionalConcepts";
            Choice<string, int> choice = name;
            var notFound = (NotFoundError)"User not found";

            // action
            var response = choice.FailWhen(v => v == "unknown", notFound);

            // asserts
            response.IsLeft.Should().BeTrue();
            response.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceFailWhenTests_ChoiceLeft_StringInt_AlreadBottom_ShouldBeReplicateBottom()
        {
            // arrange
            string name = "unknown";
            Choice<string?, int> choice = name;
            var notfoundError = (NotFoundError)"User not found";
            var failChoice = choice.FailWhen(v => v == "unknown", notfoundError);
            var invalidObj = (InvalidObjectError)"some error";

            // action
            var response = failChoice.FailWhen(v => v == "notverified", invalidObj);

            // asserts
            response.IsBottom.Should().BeTrue();
            response.Else(err =>
            {
                err.Message.Should().Be(notfoundError.Message);
            });
        }

        [Test]
        public void ChoiceFailWhenTests_ChoiceLeft_StringInt_ShouldBeFailWithNotFound()
        {
            // arrange
            string name = "unknown";
            Choice<string, int> choice = name;
            var notFound = (NotFoundError)"User not found";

            // action
            var response = choice.FailWhen(v => v == "unknown", notFound);

            // asserts
            response.IsBottom.Should().BeTrue();
        }

        [Test]
        public void ChoiceFailWhenTests_ChoiceRight_StringInt_ShouldBeNotFail()
        {
            // arrange
            int age = 18;
            Choice<string, int> choice = age;
            var notAllowed = (NotAllowedError)"too young";

            // action
            var response = choice.FailWhen(v => v < 18, notAllowed);

            // asserts
            response.IsRight.Should().BeTrue();
            response.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceFailWhenTests_ChoiceRight_StringInt_AlreadBottom_ShouldBeReplicateBottom()
        {
            // arrange
            int age = 17;
            Choice<string, int> choice = age;
            var notAllowed = (NotAllowedError)"too young";
            var failChoice = choice.FailWhen(v => v < 18, notAllowed);

            // action
            var response = failChoice.FailWhen(v => v == "notverified", (InvalidObjectError)"some error");

            // asserts
            response.IsBottom.Should().BeTrue();
            response.Else(err =>
            {
                err.Message.Should().Be(notAllowed.Message);
            });
        }

        [Test]
        public void ChoiceFailWhenTests_ChoiceRight_StringInt_ShouldBeFailNotAllowedError()
        {
            // arrange
            int age = 17;
            Choice<string, int> choice = age;
            var notAllowed = (NotAllowedError)"too young";

            // action
            var response = choice.FailWhen(v => v < 18, notAllowed);

            // asserts
            response.IsRight.Should().BeFalse();
            response.IsBottom.Should().BeTrue();
            response.Else(err =>
            {
                err.Message.Should().Be(notAllowed.Message);
            });
        }

        [Test]
        public void ChoiceFailWhenTests_ChoiceRight_WhenBottom()
        {
            // arrange
            var notAllowed = (NotAllowedError)"too young";
            Choice<string, int> choice = notAllowed;

            // action
            var response = choice.FailWhen(v => v < 18, (NotAllowedError)"err");

            // asserts
            response.IsBottom.Should().BeTrue();
            response.Else(err =>
            {
                err.Message.Should().Be(notAllowed.Message);
            });
        }

        [Test]
        public void ChoiceFailWhenTests_ChoiceLeft_WhenBottom()
        {
            // arrange
            var notAllowed = (NotAllowedError)"too young";
            Choice<string, int> choice = notAllowed;

            // action
            var response = choice.FailWhen(v => v == "18", (NotAllowedError)"err");

            // asserts
            response.IsBottom.Should().BeTrue();
            response.Else(err =>
            {
                err.Message.Should().Be(notAllowed.Message);
            });
        }
    }
}
