using FluentAssertions;
using FunctionalConcepts.Choices;
using FunctionalConcepts.Constants;
using FunctionalConcepts.Tests.Common;

namespace FunctionalConcepts.Tests.Choices
{
    [TestFixture]
    public class ChoiceTests
    {
        [Test]
        public void ChoiceTests_ChoiceLeft_StringInt_CreateWithOfShouldBeSuccess()
        {
            // arrange
            IQueryable<string> names = new List<string>
            {
                "FunctionalConcepts",
            }.AsQueryable();

            // action
            var choice = Choice.Of<IQueryable<string>, IQueryable<int>>(names);

            // asserts
            choice.IsLeft.Should().BeTrue();
            choice.IsRight.Should().BeFalse();
            choice.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceLeft_StringInt_ShouldBeSuccess()
        {
            // arrange
            string name = "FunctionalConcepts";

            // action
            Choice<string, int> choice = name;

            // asserts
            choice.IsLeft.Should().BeTrue();
            choice.IsRight.Should().BeFalse();
            choice.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceRight_StringInt_ShouldBeSuccess()
        {
            // arrange
            int name = 1;

            // action
            Choice<string, int> choice = name;

            // asserts
            choice.IsLeft.Should().BeFalse();
            choice.IsRight.Should().BeTrue();
            choice.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceRight_StringInt_CreateWithOfShouldBeSuccess()
        {
            // arrange
            IQueryable<int> ages = new List<int>
            {
                18,
            }.AsQueryable();

            // action
            var choice = Choice.Of<IQueryable<string>, IQueryable<int>>(ages);

            // asserts
            choice.IsLeft.Should().BeFalse();
            choice.IsRight.Should().BeTrue();
            choice.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceBottom_StringInt_CreateWithOfShouldBeSuccess()
        {
            // arrange
            var bottom = ErrorConstant.BOTTOM;

            // action
            var choice = Choice.Of<IQueryable<string>, IQueryable<int>>(bottom);

            // asserts
            choice.IsLeft.Should().BeFalse();
            choice.IsRight.Should().BeFalse();
            choice.IsBottom.Should().BeTrue();
        }

        [Test]
        public void ChoiceTests_ChoiceLeft_StringDateTime_ShouldBeSuccess()
        {
            // arrange
            string name = "FunctionalConcepts";

            // action
            Choice<string, DateTime> choice = name;

            // asserts
            choice.IsLeft.Should().BeTrue();
            choice.IsRight.Should().BeFalse();
            choice.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceRight_DateTimeString_ShouldBeSuccess()
        {
            // arrange
            string name = "FunctionalConcepts";

            // action
            Choice<DateTime, string> choice = name;

            // asserts
            choice.IsLeft.Should().BeFalse();
            choice.IsRight.Should().BeTrue();
            choice.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceLeft_ClassString_ShouldBeSuccess()
        {
            // arrange
            ExampleTest example = new ExampleTest();

            // action
            Choice<ExampleTest, string> choice = example;

            // asserts
            choice.IsLeft.Should().BeTrue();
            choice.IsRight.Should().BeFalse();
            choice.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceRight_StringClass_ShouldBeSuccess()
        {
            // arrange
            ExampleTest example = new ExampleTest();

            // action
            Choice<string, ExampleTest> choice = example;

            // asserts
            choice.IsLeft.Should().BeFalse();
            choice.IsRight.Should().BeTrue();
            choice.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceRight_DateTimeClass_ShouldBeSuccess()
        {
            // arrange
            ExampleTest example = new ExampleTest();

            // action
            Choice<DateTime, ExampleTest> choice = example;

            // asserts
            choice.IsLeft.Should().BeFalse();
            choice.IsRight.Should().BeTrue();
            choice.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceLeft_ClassDateTime_ShouldBeSuccess()
        {
            // arrange
            ExampleTest example = new ExampleTest();

            // action
            Choice<ExampleTest, DateTime> choice = example;

            // asserts
            choice.IsLeft.Should().BeTrue();
            choice.IsRight.Should().BeFalse();
            choice.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_Bottom_ClassDateTime_ShouldBeBottom()
        {
            // arrange
            ExampleTest? example = null;

            // action
            Choice<ExampleTest?, DateTime> choice = example;

            // asserts
            choice.IsLeft.Should().BeFalse();
            choice.IsRight.Should().BeFalse();
            choice.IsBottom.Should().BeTrue();
        }

        [Test]
        public void ChoiceTests_Bottom_ClassString_ShouldBeBottom()
        {
            // arrange
            ExampleTest? example = null;

            // action
            Choice<ExampleTest?, string> choice = example;

            // asserts
            choice.IsLeft.Should().BeFalse();
            choice.IsRight.Should().BeFalse();
            choice.IsBottom.Should().BeTrue();
        }

        [Test]
        public void ChoiceTests_Bottom_ClassInt_ShouldBeBottom()
        {
            // arrange
            ExampleTest? example = null;

            // action
            Choice<ExampleTest?, int> choice = example;

            // asserts
            choice.IsLeft.Should().BeFalse();
            choice.IsRight.Should().BeFalse();
            choice.IsBottom.Should().BeTrue();
        }
    }
}
