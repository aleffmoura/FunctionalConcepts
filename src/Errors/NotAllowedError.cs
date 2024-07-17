namespace FunctionalConcepts.Errors;

using System;
using FunctionalConcepts.Enums;

public class NotAllowedError : BaseError
{
    protected NotAllowedError(string message, Exception? exn)
        : base((int)EErrorCode.NotAllowed, message, exn)
    {
    }

    public static implicit operator NotAllowedError(string Message) => new(Message, null);
    public static implicit operator NotAllowedError((string Message, Exception Exn) tuple) => new(tuple.Message, tuple.Exn);

    public static NotAllowedError New(string msg) => (msg);
    public static NotAllowedError New(string msg, Exception exn) => (msg, exn);
}
