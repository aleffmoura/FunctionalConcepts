namespace FunctionalConcepts.Errors;

using System;
using FunctionalConcepts.Enums;

public class InvalidObjectError : BaseError
{
    protected InvalidObjectError(string message, Exception? exn)
        : base((int)EErrorCode.InvalidObject, message, exn)
    {
    }

    public static implicit operator InvalidObjectError(string Message) => new(Message, null);
    public static implicit operator InvalidObjectError((string Message, Exception Exn) tuple) => new(tuple.Message, tuple.Exn);

    public static InvalidObjectError New(string msg) => (msg);
    public static InvalidObjectError New(string msg, Exception exn) => (msg, exn);
}
