namespace FunctionalConcepts.Tests.Results;

using FluentAssertions;
using FunctionalConcepts;
using FunctionalConcepts.Constants;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using FunctionalConcepts.Tests.Common;
using Newtonsoft.Json.Serialization;

[TestFixture]
public class ResultMapTests
{
    [Test]
    public void ResultMapTests_ResultMap_ShouldBeOk()
    {
        // arrange
        var msg = "test message";
        Result<Success> result = Result.Success;
        var exampleTest = new ExampleTest(msg);

        // action
        Result<ExampleTest> response = result.Map(_ => exampleTest);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.IsFail.Should().BeFalse();
        var example = response.Match(some => some, fail => new ExampleTest("Should not return this example"));
        example.Should().Be(exampleTest);
        example!.Msg.Should().Be(example.Msg);
    }

    [Test]
    public void ResultMapTests_ResultMap_WhenError_MapToNewTypeResultButWithError_ReturnError_ShouldBeOk()
    {
        // arrange
        var code = 500;
        var msg = "test message";
        BaseError error = (code, msg);
        Result<Success> result = error;
        var exampleTest = new ExampleTest(msg);

        // action
        Result<ExampleTest> response = result.Map(_ => exampleTest);

        // assert
        response.IsFail.Should().BeTrue();
        response.IsSuccess.Should().BeFalse();
        var responseError = response.Match(_ => (100, "Should not return this example"), fail => fail);
        responseError.Should().Be(error);
        responseError!.Code.Should().Be(code);
        responseError!.Message.Should().Be(msg);
    }

    [Test]
    public async Task ResultMapTests_ResultAsyncMap_ShouldBeOk()
    {
        // arrange
        var msg = "test message";
        Result<Success> result = Result.Success;
        var exampleTest = new ExampleTest(msg);

        async Task<ExampleTest> ActionMap(Success _)
        {
            return await Task.FromResult(exampleTest);
        }

        // action
        Result<ExampleTest> response = await result.MapAsync(ActionMap);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.IsFail.Should().BeFalse();
        var example = response.Match(some => some, fail => new ExampleTest("Should not return this example"));
        example.Should().Be(exampleTest);
        example!.Msg.Should().Be(example.Msg);
    }

    [Test]
    public async Task ResultMapTests_ResultAsyncMap_WhenError_MapToNewTypeResultButWithError_ReturnError_ShouldBeOk()
    {
        // arrange
        var code = 500;
        var msg = "test message";
        BaseError error = (code, msg);
        Result<Success> result = error;
        var exampleTest = new ExampleTest(msg);

        // action
        Result<ExampleTest> response = await result.MapAsync(async _ => await Task.FromResult(exampleTest));

        // assert
        response.IsFail.Should().BeTrue();
        response.IsSuccess.Should().BeFalse();
        var responseError = response.Match(_ => (100, "Should not return this example"), fail => fail);
        responseError.Should().Be(error);
        responseError!.Code.Should().Be(code);
        responseError!.Message.Should().Be(msg);
    }

    [Test]
    public void ResultMapTests_ResultMap_ActionThrowsError_ShouldBeBottom()
    {
        // arrange
        Result<Success> result = Result.Success;
        static ExampleTest Action(Success s) => throw new Exception();

        // action
        Result<ExampleTest> response = result.Map(Action);

        // assert
        response.IsSuccess.Should().BeFalse();
        response.IsFail.Should().BeTrue();
        var msg = response.Match(some => "not return this", fail => fail.Message);
        msg.Should().Be("Error while Map");
    }

    [Test]
    public async Task ResultMapTests_ResultMap_AsyncActionThrowsError_ShouldBeBottom()
    {
        // arrange
        Result<Success> result = Result.Success;
        static async Task<ExampleTest> Action(Success s)
        {
            await Task.Delay(1);
            throw new Exception();
        }

        // action
        var response = await result.MapAsync(Action);

        // assert
        response.IsSuccess.Should().BeFalse();
        response.IsFail.Should().BeTrue();
        var msg = response.Match(some => "not return this", fail => fail.Message);
        msg.Should().Be("Error while Map");
    }
}
