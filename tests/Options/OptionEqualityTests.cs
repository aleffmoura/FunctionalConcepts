using FluentAssertions;
using FunctionalConcepts.Options;

namespace FunctionalConcepts.Tests.Options
{
    [TestFixture]
    public class OptionEqualityTests
    {
        [Test]
        public void OptionEqualityTests_Option_HashCode_Int_ShouldBeEquals()
        {
            // arrange
            int number = 1;
            Option<int> option2 = 1;

            // action
            var result = option2.GetHashCode() == number.GetHashCode();

            // asserts
            result.Should().BeTrue();
        }

        [Test]
        public void OptionEqualityTests_Option_HashCodeNone()
        {
            // arrange
            var hashCodeNone = NoneType.Value.GetHashCode();
            Option<int> option = NoneType.Value;

            // action
            var result = option.GetHashCode();

            // asserts
            result.Should().Be(hashCodeNone);
        }

        [Test]
        public void OptionEqualityTests_Option_Equals_Int_ShouldBeEquals()
        {
            // arrange
            Option<int> option = 1;
            Option<int> option2 = 1;

            // action
            var result = option.Equals(option2);

            // asserts
            result.Should().BeTrue();
        }

        [Test]
        public void OptionEqualityTests_Option_ImplicitEquals_Int_ShouldBeEquals()
        {
            // arrange
            Option<int> option = 1;
            Option<int> option2 = 1;

            // action
            var result = option == option2;

            // asserts
            result.Should().BeTrue();
        }

        [Test]
        public void OptionEqualityTests_Option_ImplicitEquals_Int_ShouldNotBeEquals()
        {
            // arrange
            Option<int> option = 1;
            Option<int> option2 = 2;

            // action
            var result = option == option2;

            // asserts
            result.Should().BeFalse();
        }

        [Test]
        public void OptionEqualityTests_Option_ImplicitNotEquals_Int_ShouldBeTrue()
        {
            // arrange
            Option<int> option = 1;
            Option<int> option2 = 2;

            // action
            var result = option != option2;

            // asserts
            result.Should().BeTrue();
        }

        [Test]
        public void OptionEqualityTests_Option_ImplicitNotEquals_Int_ShouldBeFalse()
        {
            // arrange
            Option<int> option = 1;
            Option<int> option2 = 1;

            // action
            var result = option != option2;

            // asserts
            result.Should().BeFalse();
        }

        [Test]
        public void OptionEqualityTests_Option_ImplicitOtherType_Int_ShouldBeFalse()
        {
            // arrange
            Option<int> option = 1;
            object option2 = 2;

            // action
            var result = option.Equals(option2);

            // asserts
            result.Should().BeFalse();
        }

        [Test]
        public void OptionEqualityTests_Option_ImplicitOtherTypeButOpt_Int_ShouldBeTrue()
        {
            // arrange
            Option<int> option = 1;
            object option2 = Option.Of(1);

            // action
            var result = option.Equals(option2);

            // asserts
            result.Should().BeTrue();
        }

        [Test]
        public void OptionEqualityTests_Option_ImplicitOtherTypeButOpt_ButOtherValue_Int_ShouldBeTrue()
        {
            // arrange
            Option<int> option = 1;
            object option2 = Option.Of(2);

            // action
            var result = option.Equals(option2);

            // asserts
            result.Should().BeFalse();
        }

        [Test]
        public void OptionEqualityTests_Option_Equals_String_ShouldBeEquals()
        {
            // arrange
            Option<string> option = "testing";
            Option<string> option2 = "testing";

            // action
            var result = option.Equals(option2);

            // asserts
            result.Should().BeTrue();
        }

        [Test]
        public void OptionEqualityTests_Option_Equals_Int_ShouldNotBeEquals()
        {
            // arrange
            Option<int> option = 1;
            Option<int> option2 = 2;

            // action
            var result = option.Equals(option2);

            // asserts
            result.Should().BeFalse();
        }

        [Test]
        public void OptionEqualityTests_Option_Equals_ShouldBeNotEquals()
        {
            // arrange
            Option<string> option = "testing";
            Option<string> option2 = "testing2";

            // action
            var result = option.Equals(option2);

            // asserts
            result.Should().BeFalse();
        }

        [Test]
        public void OptionEqualityTests_Option_Equals_WithNone_ShouldBeNotEquals()
        {
            // arrange
            Option<string> option = "testing";
            Option<string> option2 = NoneType.Value;

            // action
            var result = option.Equals(option2);

            // asserts
            result.Should().BeFalse();
        }

        [Test]
        public void OptionEqualityTests_Option_Equals_FirstWithNone_ShouldBeNotEquals()
        {
            // arrange
            Option<string> option = NoneType.Value;
            Option<string> option2 = "testing";

            // action
            var result = option.Equals(option2);

            // asserts
            result.Should().BeFalse();
        }

        [Test]
        public void OptionEqualityTests_Option_Equals_NotEqualsType_ShouldBeNotEquals()
        {
            // arrange
            Option<int> option = 1;
            Option<string> option2 = "testing";

            // action
            var result = option.Equals(option2);

            // asserts
            result.Should().BeFalse();
        }
    }
}
