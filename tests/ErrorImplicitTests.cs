namespace FunctionalConcepts.Tests;

using FluentAssertions;
using FunctionalConcepts.Enums;
using FunctionalConcepts.Errors;

[TestFixture]
public class ErrorImplicitTests
{
    [Test]
    public void ErrorImplicitTests_ConflictError_ShouldBeOk()
    {
        // arrange
        var msg = "Object already exists";

        // action
        ConflictError error = msg;

        // asserts
        error.Code.Should().Be((int)EErrorCode.Conflict);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_ForbiddenError_ShouldBeOk()
    {
        // arrange
        var msg = "forbidden";

        // action
        ForbiddenError error = msg;

        // asserts
        error.Code.Should().Be((int)EErrorCode.Forbidden);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_InvalidObjectError_ShouldBeOk()
    {
        // arrange
        var msg = "invalid object";

        // action
        InvalidObjectError error = msg;

        // asserts
        error.Code.Should().Be((int)EErrorCode.InvalidObject);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_NotAllowedError_ShouldBeOk()
    {
        // arrange
        var msg = "NotAllowed";

        // action
        NotAllowedError error = msg;

        // asserts
        error.Code.Should().Be((int)EErrorCode.NotAllowed);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_NotFoundError_ShouldBeOk()
    {
        // arrange
        var msg = "NotFoundError";

        // action
        NotFoundError error = msg;

        // asserts
        error.Code.Should().Be((int)EErrorCode.NotFound);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_ServiceUnavailableError_ShouldBeOk()
    {
        // arrange
        var msg = "ServiceUnavailableError";

        // action
        ServiceUnavailableError error = msg;

        // asserts
        error.Code.Should().Be((int)EErrorCode.ServiceUnavailable);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_UnauthorizedError_ShouldBeOk()
    {
        // arrange
        var msg = "UnauthorizedError";

        // action
        UnauthorizedError error = msg;

        // asserts
        error.Code.Should().Be((int)EErrorCode.Unauthorized);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_UnhandledError_ShouldBeOk()
    {
        // arrange
        var msg = "UnhandledError";

        // action
        UnhandledError error = msg;

        // asserts
        error.Code.Should().Be((int)EErrorCode.Unhandled);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_ConflictError_WithException_ShouldBeOk()
    {
        // arrange
        var msg = "Object already exists";
        var exn = new Exception("Exception");

        // action
        ConflictError error = (msg, exn);

        // asserts
        error.Exception.Should().Be(exn);
        error.Exception!.Message.Should().Be(exn.Message);
        error.Code.Should().Be((int)EErrorCode.Conflict);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_ForbiddenError_WithException_ShouldBeOk()
    {
        // arrange
        var msg = "forbidden";
        var exn = new Exception("Exception");

        // action
        ForbiddenError error = (msg, exn);

        // asserts
        error.Exception.Should().Be(exn);
        error.Exception!.Message.Should().Be(exn.Message);
        error.Code.Should().Be((int)EErrorCode.Forbidden);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_InvalidObjectError_WithException_ShouldBeOk()
    {
        // arrange
        var msg = "invalid object";
        var exn = new Exception("Exception");

        // action
        InvalidObjectError error = (msg, exn);

        // asserts
        error.Exception.Should().Be(exn);
        error.Exception!.Message.Should().Be(exn.Message);
        error.Code.Should().Be((int)EErrorCode.InvalidObject);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_NotAllowedError_WithException_ShouldBeOk()
    {
        // arrange
        var msg = "NotAllowed";
        var exn = new Exception("Exception");

        // action
        NotAllowedError error = (msg, exn);

        // asserts
        error.Exception.Should().Be(exn);
        error.Exception!.Message.Should().Be(exn.Message);
        error.Code.Should().Be((int)EErrorCode.NotAllowed);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_NotFoundError_WithException_ShouldBeOk()
    {
        // arrange
        var msg = "NotFoundError";
        var exn = new Exception("Exception");

        // action
        NotFoundError error = (msg, exn);

        // asserts
        error.Exception.Should().Be(exn);
        error.Exception!.Message.Should().Be(exn.Message);
        error.Code.Should().Be((int)EErrorCode.NotFound);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_ServiceUnavailableError_WithException_ShouldBeOk()
    {
        // arrange
        var msg = "ServiceUnavailableError";
        var exn = new Exception("Exception");

        // action
        ServiceUnavailableError error = (msg, exn);

        // asserts
        error.Exception.Should().Be(exn);
        error.Exception!.Message.Should().Be(exn.Message);
        error.Code.Should().Be((int)EErrorCode.ServiceUnavailable);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_UnauthorizedError_WithException_ShouldBeOk()
    {
        // arrange
        var msg = "UnauthorizedError";
        var exn = new Exception("Exception");

        // action
        UnauthorizedError error = (msg, exn);

        // asserts
        error.Exception.Should().Be(exn);
        error.Exception!.Message.Should().Be(exn.Message);
        error.Code.Should().Be((int)EErrorCode.Unauthorized);
        error.Message.Should().Be(msg);
    }

    [Test]
    public void ErrorImplicitTests_UnhandledError_WithException_ShouldBeOk()
    {
        // arrange
        var msg = "UnhandledError";
        var exn = new Exception("Exception");

        // action
        UnhandledError error = (msg, exn);

        // asserts
        error.Exception.Should().Be(exn);
        error.Exception!.Message.Should().Be(exn.Message);
        error.Code.Should().Be((int)EErrorCode.Unhandled);
        error.Message.Should().Be(msg);
    }
}
