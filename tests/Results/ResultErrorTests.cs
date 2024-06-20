namespace FunctionalConcepts.Tests.Results;

using FluentAssertions;
using FunctionalConcepts.Errors;

[TestFixture]
public class ResultErrorTests
{
    [Test]
    public void ResultErrorTests_Implicit_Parse_WithoutException_ExceptionShouldBeNull_ShouldBeOk()
    {
        // arrange
        var code = 404;
        var msg = "not found";
        (int Code, string Msg) tuple = (code, msg);

        // action
        BaseError resultError = tuple;

        // asserts
        resultError.Code.Should().Be(code);
        resultError.Message.Should().Be(msg);
        resultError.Exception.Should().BeNull();
    }

    [Test]
    public void ResultErrorTests_Implicit_Parse_WithException_ShouldBeOk()
    {
        // arrange
        var code = 404;
        var msg = "not found";
        Exception ex = new("not found");
        (int Code, string Msg, Exception Exn) tuple = (code, msg, ex);

        // action
        BaseError resultError = tuple;

        // asserts
        resultError.Code.Should().Be(code);
        resultError.Message.Should().Be(msg);
        resultError.Exception.Should().Be(ex);
        ex.Message.Should().Be(resultError.Exception?.Message);
    }
}
