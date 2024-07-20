namespace FunctionalConcepts.Errors;

using System;
using FunctionalConcepts.Enums;

public class UnhandledError : BaseError
{
    protected UnhandledError(string message, Exception? exn)
        : base((int)EErrorCode.Unhandled, message, exn)
    {
    }

    public static implicit operator UnhandledError(string Message) => new(Message, null);
    public static implicit operator UnhandledError((string Message, Exception Exn) tuple) => new(tuple.Message, tuple.Exn);

    public static UnhandledError New(string msg) => (msg);
    public static UnhandledError New(string msg, Exception exn) => (msg, exn);
}
