namespace FunctionalConcepts.Tests.Results;

using FluentAssertions;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using FunctionalConcepts.Tests.Common;

[TestFixture]
public class ResultBindTests
{
    [Test]
    public void ResultBindTests_ResultBind_ShouldBeOk()
    {
        // arrange
        var toUpdatedWithBind = "updated message";
        var msg = "test message";
        var exampleTest = new ExampleTest(msg);
        Result<ExampleTest> response = exampleTest;

        // action
        var returned = response.Bind(toBind =>
        {
            return toBind with
            {
                Msg = toUpdatedWithBind,
            };
        });

        // assert
        returned.IsSuccess.Should().BeTrue();
        returned.Then(value => value.Msg.Should().Be(toUpdatedWithBind));
    }

    [Test]
    public async Task ResultBindTests_ResultBindAsync_ShouldBeOk()
    {
        // arrange
        var toUpdatedWithBind = "updated message";
        var msg = "test message";
        var exampleTest = new ExampleTest(msg);
        Result<ExampleTest> response = exampleTest;

        // action
        var returned = await response.BindAsync(async toBind =>
        {
            return toBind with
            {
                Msg = await Task.FromResult(toUpdatedWithBind),
            };
        });

        // assert
        returned.IsSuccess.Should().BeTrue();
        returned.Then(value => value.Msg.Should().Be(toUpdatedWithBind));
    }

    [Test]
    public void ResultBindTests_ResultBind_WhenFailNotExecuteBind_ShouldBeOk()
    {
        // arrange
        var toUpdatedWithBind = "updated message";
        BaseError error = (500, "error message");
        Result<ExampleTest> response = error;
        ExampleTest? exampleTest = null;

        // action
        var returned = response.Bind(toBind =>
        {
            exampleTest = toBind with
            {
                Msg = toUpdatedWithBind,
            };

            return exampleTest;
        });

        // assert
        returned.IsFail.Should().BeTrue();
        returned.IsSuccess.Should().BeFalse();
        exampleTest.Should().BeNull();
    }

    [Test]
    public async Task ResultBindTests_ResultBindAsync_WhenFailNotExecuteBind_ShouldBeOk()
    {
        // arrange
        var toUpdatedWithBind = "updated message";
        BaseError error = (500, "error message");
        Result<ExampleTest> response = error;
        ExampleTest? exampleTest = null;

        // action
        var returned = await response.BindAsync(async toBind =>
        {
            exampleTest = toBind with
            {
                Msg = toUpdatedWithBind,
            };

            return await Task.FromResult(exampleTest);
        });

        // assert
        returned.IsFail.Should().BeTrue();
        returned.IsSuccess.Should().BeFalse();
        exampleTest.Should().BeNull();
    }
}
