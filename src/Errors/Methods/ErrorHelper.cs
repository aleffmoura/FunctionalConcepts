namespace FunctionalConcepts.Errors.Methods;

using FunctionalConcepts.Errors;

public static class ErrorHelper
{
    public static BaseError CustomError(int statusCode, string msg) => (statusCode, msg);
    public static BaseError CustomError(int statusCode, string msg, Exception exn) => (statusCode, msg, exn);

    public static ConflictError Conflict(string msg) => msg;
    public static ConflictError Conflict(string msg, Exception exn) => (msg, exn);

    public static ForbiddenError Forbidden(string msg) => msg;
    public static ForbiddenError Forbidden(string msg, Exception exn) => (msg, exn);

    public static InvalidObjectError InvalidObject(string msg) => msg;
    public static InvalidObjectError InvalidObject(string msg, Exception exn) => (msg, exn);

    public static NotAllowedError NotAllowed(string msg) => msg;
    public static NotAllowedError NotAllowed(string msg, Exception exn) => (msg, exn);

    public static NotFoundError NotFound(string msg) => msg;
    public static NotFoundError NotFound(string msg, Exception exn) => (msg, exn);

    public static ServiceUnavailableError ServiceUnavailable(string msg) => msg;
    public static ServiceUnavailableError ServiceUnavailable(string msg, Exception exn) => (msg, exn);

    public static UnauthorizedError Unauthorized(string msg) => msg;
    public static UnauthorizedError Unauthorized(string msg, Exception exn) => (msg, exn);

    public static UnhandledError Unhandled(string msg) => msg;
    public static UnhandledError Unhandled(string msg, Exception exn) => (msg, exn);
}
