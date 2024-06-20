namespace FunctionalConcepts.Tests.Results;

using FluentAssertions;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;

[TestFixture]
public class ResultMatchTests
{
    [Test]
    public async Task ResultMatchTests_MatchBothAsync_WhenSome_ShouldBeOk()
    {
        // arrange
        var msg = "suc";
        var callback = async (string msg) => await Task.FromResult(msg);
        Result<Success> result = Result.Success;

        // action
        var response = await result.MatchAsync(async _ => await callback(msg), async _ => await callback("fail"));

        // asserts
        result.IsFail.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        response.Should().Be(msg);
    }

    [Test]
    public async Task ResultMatchTests_MatchBothAsync_WhenFail_ShouldBeOk()
    {
        // arrange
        var msg = "fail";
        var callback = async (string msg) => await Task.FromResult(msg);
        ConflictError error = "ConflictError";
        Result<Success> result = error;

        // action
        var response = await result.MatchAsync(async _ => await callback("succ"), async _ => await callback(msg));

        // asserts
        result.IsFail.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        response.Should().Be(msg);
    }

    [Test]
    public async Task ResultMatchTests_MatchSomeAsync_WhenSome_ShouldBeOk()
    {
        // arrange
        var msg = "suc";
        var callback = async (string msg) => await Task.FromResult(msg);
        Result<Success> result = Result.Success;

        // action
        var response = await result.MatchAsync(async _ => await callback(msg), _ => "fail");

        // asserts
        result.IsFail.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        response.Should().Be(msg);
    }

    [Test]
    public async Task ResultMatchTests_MatchFailAsync_WhenFail_ShouldBeOk()
    {
        // arrange
        var msg = "fail";
        var callback = async (string msg) => await Task.FromResult(msg);
        ConflictError error = "ConflictError";
        Result<Success> result = error;

        // action
        var response = await result.MatchAsync(_ => "succ", async _ => await callback(msg));

        // asserts
        result.IsFail.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        response.Should().Be(msg);
    }

    [Test]
    public async Task ResultMatchTests_MatchFailAsync_WhenSome_ShouldBeOk()
    {
        // arrange
        var msg = "suc";
        var callback = async (string msg) => await Task.FromResult(msg);
        Result<Success> result = Result.Success;

        // action
        var response = await result.MatchAsync(_ => msg, async _ => await callback(msg));

        // asserts
        result.IsFail.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        response.Should().Be(msg);
    }

    [Test]
    public async Task ResultMatchTests_MatchSomeAsync_WhenFail_ShouldBeOk()
    {
        // arrange
        var msg = "fail";
        var callback = async (string msg) => await Task.FromResult(msg);
        ConflictError error = "ConflictError";
        Result<Success> result = error;

        // action
        var response = await result.MatchAsync(async _ => await callback(msg), _ => msg);

        // asserts
        result.IsFail.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        response.Should().Be(msg);
    }
}
