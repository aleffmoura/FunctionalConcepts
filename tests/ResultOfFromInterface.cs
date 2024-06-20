namespace FunctionalConcepts.Tests;

using System.Linq;
using FluentAssertions;
using FunctionalConcepts.Enums;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;

[TestFixture]
public class ResultOfFromInterface
{
    [Test]
    public void ResultOfFromInterface_QueryableTests_AsResult_FromQueryable_ShouldBeOk()
    {
        // arrange
        var getInRepository = () => new List<string>().AsQueryable();
        var queryable = getInRepository();

        // action
        var result = Result.Of(queryable);

        // asserts
        result.IsSuccess.Should().BeTrue();
        result.Then(value => value.Should().BeEquivalentTo(queryable));
    }

    [Test]
    public void ResultOfFromInterface_QueryableTests_ResultQueryable_FromError_AsResult_ShouldBeOk()
    {
        // arrange
        var msg = "UnhandledError";
        UnhandledError error = msg;

        // action
        var result = Result.Of<IQueryable<string>>(error);

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
