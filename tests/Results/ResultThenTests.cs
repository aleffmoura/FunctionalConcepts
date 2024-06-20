namespace FunctionalConcepts.Tests.Results;

using FluentAssertions;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using FunctionalConcepts.Tests.Common;

[TestFixture]
public class ResultThenTests
{
    [Test]
    public async Task ResultThenTests_ResultIfSuccAsync_ShouldBeOk()
    {
        // arrange
        var msg = "test message";
        var exampleTest = new ExampleTest(msg);
        Result<ExampleTest> exampleTestResult = exampleTest;
        ExampleTest? exampleCallbackIf = null;

        async Task CallbackUpdateAsync(ExampleTest example)
        {
            var exp = await Task.FromResult(example);
            exampleCallbackIf = exp;
        }

        // action
        Result<ExampleTest> response = await exampleTestResult.ThenAsync(CallbackUpdateAsync);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.IsFail.Should().BeFalse();
        exampleCallbackIf.Should().NotBeNull();
    }

    [Test]
    public async Task ResultThenTests_ResultIfFailAsync_ShouldBeOk()
    {
        // arrange
        var code = 404;
        var msg = "not found";
        (int Code, string Msg) tuple = (code, msg);
        BaseError err = tuple;
        Result<ExampleTest> result = err;
        BaseError? error = null;

        // action
        Result<ExampleTest> response = await result.ElseAsync(async e =>
        {
            error = await Task.FromResult(e);
        });

        // assert
        response.IsSuccess.Should().BeFalse();
        response.IsFail.Should().BeTrue();
        error.Should().NotBeNull();
    }

    [Test]
    public void ResultThenTests_ResultIfSucc_ShouldBeOk()
    {
        // arrange
        var msg = "test message";
        var exampleTest = new ExampleTest(msg);
        Result<ExampleTest> exampleTestResult = exampleTest;
        ExampleTest? exampleCallbackIf = null;

        // action
        Result<ExampleTest> response = exampleTestResult.Then(example =>
        {
            exampleCallbackIf = example;
        });

        // assert
        response.IsSuccess.Should().BeTrue();
        response.IsFail.Should().BeFalse();
        exampleCallbackIf.Should().NotBeNull();
    }

    [Test]
    public void ResultThenTests_ResultIfFail_ShouldBeOk()
    {
        // arrange
        var code = 404;
        var msg = "not found";
        (int Code, string Msg) tuple = (code, msg);
        BaseError err = tuple;
        Result<ExampleTest> result = err;
        BaseError? error = null;

        // action
        Result<ExampleTest> response = result.Else(e =>
        {
            error = e;
        });

        // assert
        response.IsSuccess.Should().BeFalse();
        response.IsFail.Should().BeTrue();
        error.Should().NotBeNull();
    }

    [Test]
    public void ResultThenTests_ResultIfSucc_WhenFailNotExecuteCallback_ShouldBeOk()
    {
        // arrange
        BaseError err = (404, "not found");
        Result<ExampleTest> exampleTestResult = err;
        ExampleTest? exampleCallbackIf = null;

        // action
        Result<ExampleTest> response = exampleTestResult.Then(value =>
        {
            exampleCallbackIf = value;
        });

        // assert
        response.IsSuccess.Should().BeFalse();
        response.IsFail.Should().BeTrue();
        exampleCallbackIf.Should().BeNull();
    }

    [Test]
    public void ResultThenTests_ResultIfFail_WhenSuccNotExecuteCallback_ShouldBeOk()
    {
        // arrange
        var msg = "test message";
        Result<ExampleTest> exampleTestResult = new ExampleTest(msg);
        BaseError? exampleErr = null;

        // action
        Result<ExampleTest> response = exampleTestResult.Else(err =>
        {
            exampleErr = err;
        });

        // assert
        response.IsSuccess.Should().BeTrue();
        response.IsFail.Should().BeFalse();
        exampleErr.Should().BeNull();
    }

    [Test]
    public async Task ResultThenTests_ResultIfSuccAsync_WhenFailNotExecuteCallback_ShouldBeOk()
    {
        // arrange
        BaseError err = (404, "not found");
        Result<ExampleTest> exampleTestResult = err;
        ExampleTest? exampleCallbackIf = null;

        // action
        Result<ExampleTest> response = await exampleTestResult.ThenAsync(async value =>
        {
            exampleCallbackIf = await Task.FromResult(value);
        });

        // assert
        response.IsSuccess.Should().BeFalse();
        response.IsFail.Should().BeTrue();
        exampleCallbackIf.Should().BeNull();
    }

    [Test]
    public async Task ResultThenTests_ResultIfSuccAsync_ThrowExceptionOnFuncion_ShouldBeError()
    {
        // arrange
        Result<int> exampleTestResult = 1;

        // action
        Result<int> response = await exampleTestResult.ThenAsync(async _ =>
        {
            await Task.Delay(1);
            throw new Exception();
        });

        // assert
        response.IsFail.Should().BeTrue();
        response.IsSuccess.Should().BeFalse();
    }

    [Test]
    public void ResultThenTests_ResultIfSucc_ThrowExceptionOnFuncion_ShouldBeError()
    {
        // arrange
        Result<int> exampleTestResult = 1;

        static void Action(int v) => throw new Exception();

        // action
        Result<int> response = exampleTestResult.Then(Action);

        // assert
        response.IsFail.Should().BeTrue();
        response.IsSuccess.Should().BeFalse();
    }

    [Test]
    public async Task ResultThenTests_ResultIfFailAsync_WhenSuccNotExecuteCallback_ShouldBeOk()
    {
        // arrange
        var msg = "test message";
        Result<ExampleTest> exampleTestResult = new ExampleTest(msg);
        BaseError? exampleErr = null;

        // action
        Result<ExampleTest> response = await exampleTestResult.ElseAsync(async err =>
        {
            exampleErr = await Task.FromResult(err);
        });

        // assert
        response.IsSuccess.Should().BeTrue();
        response.IsFail.Should().BeFalse();
        exampleErr.Should().BeNull();
    }
}
