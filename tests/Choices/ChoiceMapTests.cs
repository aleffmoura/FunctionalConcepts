using FluentAssertions;
using FunctionalConcepts.Choices;
using FunctionalConcepts.Tests.Common;

namespace FunctionalConcepts.Tests.Choices
{
    [TestFixture]
    public class ChoiceMapTests
    {
        [Test]
        public void ChoiceTests_ChoiceLeft_Map_StringClass_To_IntClass_FunctionTrowsErr_ShoulBeBottom()
        {
            // arrange
            string name = "invalidNumber";
            Choice<string, ExampleTest> choice = name;

            // action
            Choice<int, ExampleTest> returned = choice.MapLeft(int.Parse);

            // asserts
            returned.IsLeft.Should().BeFalse();
            returned.IsRight.Should().BeFalse();
            returned.IsBottom.Should().BeTrue();
        }

        [Test]
        public void ChoiceTests_ChoiceLeft_Map_StringClass_To_IntClass_ShouldBeSuccess()
        {
            // arrange
            string name = "1";
            Choice<string, ExampleTest> choice = name;

            // action
            Choice<int, ExampleTest> returned = choice.MapLeft(int.Parse);

            // asserts
            returned.IsLeft.Should().BeTrue();
            returned.IsRight.Should().BeFalse();
            returned.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceLeft_Map_StringClass_To_IntClass_WhenRightShouldNotExecuteMapFunctionButReturnNewType()
        {
            // arrange
            var test = new ExampleTest();
            Choice<string, ExampleTest> choice = test;

            // action
            Choice<int, ExampleTest> returned = choice.MapLeft(int.Parse);

            // asserts
            returned.IsLeft.Should().BeFalse();
            returned.IsRight.Should().BeTrue();
            returned.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceLeft_Map_StringClass_To_IntClass_ShouldBeBottom()
        {
            // arrange
            string? name = null;
            Choice<string?, ExampleTest> choice = name;

            // action
            Choice<int, ExampleTest> returned = choice.MapLeft(v => int.Parse(v!));

            // asserts
            returned.IsLeft.Should().BeFalse();
            returned.IsRight.Should().BeFalse();
            returned.IsBottom.Should().BeTrue();
        }

        [Test]
        public async Task ChoiceTests_ChoiceLeft_MapAsync_StringClass_To_IntClass_ShouldBeSuccess()
        {
            // arrange
            static async Task<int> Map(string number) => await Task.FromResult(int.Parse(number));
            string name = "1";
            Choice<string, ExampleTest> choice = name;

            // action
            Choice<int, ExampleTest> returned = await choice.MapLeftAsync(Map);

            // asserts
            returned.IsLeft.Should().BeTrue();
            returned.IsRight.Should().BeFalse();
            returned.IsBottom.Should().BeFalse();
        }

        [Test]
        public async Task ChoiceTests_ChoiceLeft_MapAsync_StringClass_To_IntClass_FunctionThrowsError_ShoulBeBottom()
        {
            // arrange
            static async Task<int> Map(string number) => await Task.FromResult(int.Parse(number));
            string name = "invalidNumber";
            Choice<string, ExampleTest> choice = name;

            // action
            Choice<int, ExampleTest> returned = await choice.MapLeftAsync(Map);

            // asserts
            returned.IsLeft.Should().BeFalse();
            returned.IsRight.Should().BeFalse();
            returned.IsBottom.Should().BeTrue();
        }

        [Test]
        public async Task ChoiceTests_ChoiceLeft_MapAsync_StringClass_To_IntClass_ShouldMapButNotExecuteFunction()
        {
            // arrange
            static async Task<int> Map(string number) => await Task.FromResult(int.Parse(number));
            var test = new ExampleTest();
            Choice<string, ExampleTest> choice = test;

            // action
            Choice<int, ExampleTest> returned = await choice.MapLeftAsync(Map);

            // asserts
            returned.IsLeft.Should().BeFalse();
            returned.IsRight.Should().BeTrue();
            returned.IsBottom.Should().BeFalse();
        }

        [Test]
        public async Task ChoiceTests_ChoiceLeft_MapAsync_StringClass_To_IntClass_ShouldBeBottom()
        {
            // arrange
            static async Task<int> Map(string? number) => await Task.FromResult(int.Parse(number!));
            string? name = null;
            Choice<string?, ExampleTest> choice = name;

            // action
            Choice<int, ExampleTest> returned = await choice.MapLeftAsync(Map);

            // asserts
            returned.IsLeft.Should().BeFalse();
            returned.IsRight.Should().BeFalse();
            returned.IsBottom.Should().BeTrue();
        }

        [Test]
        public void ChoiceTests_ChoiceRight_Map_ClassString_To_ClassInt_FunctionThrowsError_ShoulBeBottom()
        {
            // arrange
            string name = "invalidNumber";
            Choice<ExampleTest, string> choice = name;

            // action
            Choice<ExampleTest, int> returned = choice.MapRight(int.Parse);

            // asserts
            returned.IsLeft.Should().BeFalse();
            returned.IsRight.Should().BeFalse();
            returned.IsBottom.Should().BeTrue();
        }

        [Test]
        public void ChoiceTests_ChoiceRight_Map_ClassString_To_ClassInt_ShouldBeSuccess()
        {
            // arrange
            string name = "1";
            Choice<ExampleTest, string> choice = name;

            // action
            Choice<ExampleTest, int> returned = choice.MapRight(int.Parse);

            // asserts
            returned.IsLeft.Should().BeFalse();
            returned.IsRight.Should().BeTrue();
            returned.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceRight_Map_ClassString_To_ClassInt_ShouldBeSuccess_WhenLeftShouldNotExecuteMapFunctionButReturnNewType()
        {
            // arrange
            var test = new ExampleTest();
            Choice<ExampleTest, string> choice = test;

            // action
            Choice<ExampleTest, int> returned = choice.MapRight(int.Parse);

            // asserts
            returned.IsLeft.Should().BeTrue();
            returned.IsRight.Should().BeFalse();
            returned.IsBottom.Should().BeFalse();
        }

        [Test]
        public async Task ChoiceTests_ChoiceRight_MapAsync_ClassString_To_ClassInt_ShouldBeSuccess()
        {
            // arrange
            static async Task<int> Map(string number) => await Task.FromResult(int.Parse(number));
            string name = "1";
            Choice<ExampleTest, string> choice = name;

            // action
            Choice<ExampleTest, int> returned = await choice.MapRightAsync(Map);

            // asserts
            returned.IsLeft.Should().BeFalse();
            returned.IsRight.Should().BeTrue();
            returned.IsBottom.Should().BeFalse();
        }

        [Test]
        public async Task ChoiceTests_ChoiceRight_MapAsync_ClassString_To_ClassInt_FunctionThrowsErr_ShouldBeBottom()
        {
            // arrange
            static async Task<int> Map(string number) => await Task.FromResult(int.Parse(number));
            string name = "invalidNumber";
            Choice<ExampleTest, string> choice = name;

            // action
            Choice<ExampleTest, int> returned = await choice.MapRightAsync(Map);

            // asserts
            returned.IsLeft.Should().BeFalse();
            returned.IsRight.Should().BeFalse();
            returned.IsBottom.Should().BeTrue();
        }

        [Test]
        public async Task ChoiceTests_ChoiceRight_MapAsync_ClassString_To_ClassInt_ShouldBeSuccess_WhenLeftShouldNotExecuteMapFunctionButReturnNewType()
        {
            // arrange
            static async Task<int> Map(string number) => await Task.FromResult(int.Parse(number));
            var test = new ExampleTest();
            Choice<ExampleTest, string> choice = test;

            // action
            Choice<ExampleTest, int> returned = await choice.MapRightAsync(Map);

            // asserts
            returned.IsLeft.Should().BeTrue();
            returned.IsRight.Should().BeFalse();
            returned.IsBottom.Should().BeFalse();
        }

        [Test]
        public void ChoiceTests_ChoiceRight_Map_ClassString_To_ClassInt_ShouldBeBottom()
        {
            // arrange
            string? name = null;
            Choice<ExampleTest, string?> choice = name;

            // action
            Choice<ExampleTest, int> returned = choice.MapRight(v => int.Parse(v!));

            // asserts
            returned.IsLeft.Should().BeFalse();
            returned.IsRight.Should().BeFalse();
            returned.IsBottom.Should().BeTrue();
        }

        [Test]
        public async Task ChoiceTests_ChoiceRight_MapAsync_ClassString_To_ClassInt_ShouldBeBottom()
        {
            // arrange
            static async Task<int> Map(string? number)
                => await Task.FromResult(int.Parse(number!));
            string? name = null;
            Choice<ExampleTest, string?> choice = name;

            // action
            Choice<ExampleTest, int> returned = await choice.MapRightAsync(Map);

            // asserts
            returned.IsLeft.Should().BeFalse();
            returned.IsRight.Should().BeFalse();
            returned.IsBottom.Should().BeTrue();
        }
    }
}
