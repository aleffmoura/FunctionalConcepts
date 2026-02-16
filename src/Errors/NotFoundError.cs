namespace FunctionalConcepts.Errors;

using System;
using FunctionalConcepts.Enums;

public class NotFoundError : BaseError
{
    protected NotFoundError(string message, Exception? exn)
        : base((int)EErrorCode.NotFound, message, exn)
    {
    }

    public static implicit operator NotFoundError(string Message) => new(Message, null);
    public static implicit operator NotFoundError((string Message, Exception Exn) tuple) => new(tuple.Message, tuple.Exn);

    public static NotFoundError New(string msg) => msg;
    public static NotFoundError New(string msg, Exception exn) => (msg, exn);
}
