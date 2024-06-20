namespace FunctionalConcepts.Tests;

using FluentAssertions;
using FunctionalConcepts.Enums;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;

[TestFixture]
public class ResultOfFromList
{
    [Test]
    public void ResultOfFromList_ListTests_AsResult_FromQueryable_ShouldBeOk()
    {
        // arrange
        var getInRepository = () => new List<string>();
        var list = getInRepository();

        // action
        Result<List<string>> result = list;

        // asserts
        result.IsSuccess.Should().BeTrue();
        result.Then(value => value.Should().BeEquivalentTo(list));
    }

    [Test]
    public void ResultOfFromList_ListTests_ResultQueryable_FromError_ShouldBeOk()
    {
        // arrange
        var msg = "UnhandledError";
        UnhandledError error = msg;

        // action
        Result<List<string>> result = error;

        // asserts
        result.IsSuccess.Should().BeFalse();
        result.IsFail.Should().BeTrue();
        result.Else(value =>
        {
            value.Should().Be(error);
            value.Code.Should().Be((int)EErrorCode.Unhandled);
            value.Message.Should().Be(msg);
        });
    }
}
