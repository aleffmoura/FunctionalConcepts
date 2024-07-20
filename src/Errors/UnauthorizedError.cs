namespace FunctionalConcepts.Errors;

using System;
using FunctionalConcepts.Enums;

public class UnauthorizedError : BaseError
{
    protected UnauthorizedError(string message, Exception? exn)
        : base((int)EErrorCode.Unauthorized, message, exn)
    {
    }

    public static implicit operator UnauthorizedError(string Message) => new(Message, null);
    public static implicit operator UnauthorizedError((string Message, Exception Exn) tuple) => new(tuple.Message, tuple.Exn);

    public static UnauthorizedError New(string msg) => (msg);
    public static UnauthorizedError New(string msg, Exception exn) => (msg, exn);
}
