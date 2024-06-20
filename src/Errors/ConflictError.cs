namespace FunctionalConcepts.Errors;

using System;
using FunctionalConcepts.Enums;

public class ConflictError : BaseError
{
    protected ConflictError(string message, Exception? exn)
        : base((int)EErrorCode.Conflict, message, exn)
    {
    }

    public static implicit operator ConflictError(string Message) => new(Message, null);
    public static implicit operator ConflictError((string Message, Exception Exn) tuple) => new(tuple.Message, tuple.Exn);
}
