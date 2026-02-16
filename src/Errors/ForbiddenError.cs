namespace FunctionalConcepts.Errors;

using System;
using FunctionalConcepts.Enums;

public class ForbiddenError : BaseError
{
    protected ForbiddenError(string message, Exception? exn)
        : base((int)EErrorCode.Forbidden, message, exn)
    {
    }

    public static implicit operator ForbiddenError(string Message) => new(Message, null);
    public static implicit operator ForbiddenError((string Message, Exception Exn) tuple) => new(tuple.Message, tuple.Exn);

    public static ForbiddenError New(string msg) => msg;
    public static ForbiddenError New(string msg, Exception exn) => (msg, exn);
}
