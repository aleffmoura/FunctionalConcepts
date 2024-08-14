using FluentAssertions;
using FunctionalConcepts.Choices;

namespace FunctionalConcepts.Tests.Choices
{
    [TestFixture]
    public class ChoiceBindTests
    {
        [Test]
        public void ChoiceBindTests_ChoiceLeft_StringInt_BindLeft_WhenLeft_FuncThrowError_ShouldBeBottom()
        {
            // arrange
            string name = "FunctionalConcepts";
            Choice<string, int> choice = name;

            // action
            var binded = choice.BindLeft(Action);

            // asserts
            binded.IsLeft.Should().BeFalse();
            binded.IsRight.Should().BeFalse();
            binded.IsBottom.Should().BeTrue();

            static Choice<string, int> Action(string _) => throw new Exception("error");
        }

        [Test]
        public async Task ChoiceBindTests_ChoiceLeft_StringInt_BindAsyncLeft_WhenLeft_FuncThrowError_ShouldBeBottom()
        {
            // arrange
            string name = "FunctionalConcepts";
            Choice<string, int> choice = name;

            // action
            var binded = await choice.BindLeftAsync(Action);

            // asserts
            binded.IsLeft.Should().BeFalse();
            binded.IsRight.Should().BeFalse();
            binded.IsBottom.Should().BeTrue();

            static async Task<Choice<string, int>> Action(string _)
            {
                await Task.Delay(1);
                throw new Exception("error");
            }
        }

        [Test]
        public async Task ChoiceBindTests_ChoiceRight_StringInt_BindAsyncRight_WhenRight_FuncThrowError_ShouldBeBottom()
        {
            // arrange
            int age = 12;
            Choice<string, int> choice = age;

            // action
            var binded = await choice.BindRightAsync(Action);

            // asserts
            binded.IsLeft.Should().BeFalse();
            binded.IsRight.Should().BeFalse();
            binded.IsBottom.Should().BeTrue();

            static async Task<Choice<string, int>> Action(int _)
            {
                await Task.Delay(1);
                throw new Exception("error");
            }
        }

        [Test]
        public void ChoiceBindTests_ChoiceRight_StringInt_BindRight_WhenRight_FuncThrowError_ShouldBeBottom()
        {
            // arrange
            int name = 12;
            Choice<string, int> choice = name;

            // action
            var binded = choice.BindRight(Action);

            // asserts
            binded.IsLeft.Should().BeFalse();
            binded.IsRight.Should().BeFalse();
            binded.IsBottom.Should().BeTrue();

            static Choice<string, int> Action(int _) => throw new Exception("error");
        }

        [Test]
        public void ChoiceBindTests_ChoiceLeft_StringInt_BindLeft_WhenLeft_ShouldBeSuccess()
        {
            // arrange
            string name = "FunctionalConcepts";
            var toCompare = $"bind left {name}";
            Choice<string, int> choice = name;

            // action
            var binded = choice.BindLeft(value =>
            {
                return Choice.Of<string, int>($"bind left {value}");
            });

            // asserts
            binded.IsLeft.Should().BeTrue();
            binded.IsRight.Should().BeFalse();
            binded.IsBottom.Should().BeFalse();
            binded.ThenLeft(val => val.Should().Be(toCompare));
        }

        [Test]
        public async Task ChoiceBindTests_ChoiceLeft_StringInt_BindLeft_WhenLeftAsync_ShouldBeSuccess()
        {
            // arrange
            string name = "FunctionalConcepts";
            var toCompare = $"bind left {name}";
            Choice<string, int> choice = name;

            // action
            var binded = await choice.BindLeftAsync(async value =>
            {
                return await Task.FromResult(Choice.Of<string, int>($"bind left {value}"));
            });

            // asserts
            binded.IsLeft.Should().BeTrue();
            binded.IsRight.Should().BeFalse();
            binded.IsBottom.Should().BeFalse();
            binded.ThenLeft(val => val.Should().Be(toCompare));
        }

        [Test]
        public void ChoiceBindTests_ChoiceLeft_StringInt_BindRight_WhenLeft_ShouldNotExecuteFunction()
        {
            // arrange
            string name = "FunctionalConcepts";
            Choice<string, int> choice = name;

            // action
            var binded = choice.BindRight(value =>
            {
                return Choice.Of<string, int>(value + 2);
            });

            // asserts
            binded.IsLeft.Should().BeTrue();
            binded.IsRight.Should().BeFalse();
            binded.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceBindTests_ChoiceRight_StringInt_BindRight_WhenRight_ShouldBeSuccess()
        {
            // arrange
            int age = 18;
            var toCompare = age + 2;
            Choice<string, int> choice = age;

            // action
            var binded = choice.BindRight(value =>
            {
                return Choice.Of<string, decimal>(value + 2);
            });

            // asserts
            binded.IsLeft.Should().BeFalse();
            binded.IsRight.Should().BeTrue();
            binded.IsBottom.Should().BeFalse();
            binded.ThenRight(val => val.Should().Be(toCompare));
        }

        [Test]
        public void ChoiceBindTests_ChoiceRight_StringInt_BindLeft_WhenRight_ShouldBeSuccess()
        {
            // arrange
            int age = 18;
            Choice<string, int> choice = age;

            // action
            var binded = choice.BindLeft(value => Choice.Of<string, int>("test"));

            // asserts
            binded.IsLeft.Should().BeFalse();
            binded.IsRight.Should().BeTrue();
            binded.IsBottom.Should().BeFalse();
            binded.ThenRight(val => val.Should().Be(age));
        }

        [Test]
        public async Task ChoiceBindTests_ChoiceRight_StringInt_BindRightAsyn_WhenRight_ShouldBeSuccess()
        {
            // arrange
            int age = 18;
            decimal toCompare = age + 2;
            Choice<string, int> choice = age;

            // action
            var binded = await choice.BindRightAsync(async value =>
            {
                return await Task.FromResult(Choice.Of<string, decimal>(value + 2));
            });

            // asserts
            binded.IsLeft.Should().BeFalse();
            binded.IsRight.Should().BeTrue();
            binded.IsBottom.Should().BeFalse();
            binded.ThenRight(val => val.Should().Be(toCompare));
        }

        [Test]
        public void ChoiceBindTests_ChoiceLeft_StringInt_BindLeft_WhenLeft_ExecuteFunctionThrowsException_ButChoiceIsBottom()
        {
            // arrange
            string name = "FunctionalConcepts";
            var toCompare = $"bind left {name}";
            Choice<string, int> choice = name;

            static Choice<string, int> Action(string value) => throw new Exception("error");

            // action
            var binded = choice.BindLeft(Action);

            // asserts
            binded.IsLeft.Should().BeFalse();
            binded.IsRight.Should().BeFalse();
            binded.IsBottom.Should().BeTrue();
            binded.Else(err => err.Message.Should().Be("error"));
        }

        [Test]
        public void ChoiceBindTests_ChoiceRight_StringInt_BindRight_WhenRight_ExecuteFunctionThrowsException_ButChoiceIsBottom()
        {
            // arrange
            int age = 18;
            var toCompare = age + 2;
            Choice<string, int> choice = age;
            static Choice<string, int> Action(int value) => throw new Exception("error");

            // action
            var binded = choice.BindRight(Action);

            // asserts
            binded.IsLeft.Should().BeFalse();
            binded.IsRight.Should().BeFalse();
            binded.IsBottom.Should().BeTrue();
            binded.Else(err => err.Message.Should().Be("error"));
        }

        [Test]
        public async Task ChoiceBindTests_ChoiceLeft_StringInt_BindLeftAsync_WhenLeft_ExecuteFunctionThrowsException_ButChoiceIsBottom()
        {
            // arrange
            string name = "FunctionalConcepts";
            var toCompare = $"bind left {name}";
            Choice<string, int> choice = name;

            static async Task<Choice<string, int>> Action(string value)
            {
                await Task.Delay(1);
                throw new Exception("error");
            }

            // action
            var binded = await choice.BindLeftAsync(Action);

            // asserts
            binded.IsLeft.Should().BeFalse();
            binded.IsRight.Should().BeFalse();
            binded.IsBottom.Should().BeTrue();
            binded.Else(err => err.Message.Should().Be("error"));
        }

        [Test]
        public async Task ChoiceBindTests_ChoiceRight_StringInt_BindRightAsync_WhenRight_ExecuteFunctionThrowsException_ButChoiceIsBottom()
        {
            // arrange
            int age = 18;
            var toCompare = age + 2;
            Choice<string, int> choice = age;

            // action
            var binded = await choice.BindRightAsync(Action);

            // asserts
            binded.IsLeft.Should().BeFalse();
            binded.IsRight.Should().BeFalse();
            binded.IsBottom.Should().BeTrue();
            binded.Else(err => err.Message.Should().Be("error"));

            static async Task<Choice<string, int>> Action(int value)
            {
                await Task.Delay(1);
                throw new Exception("error");
            }
        }
    }
}
