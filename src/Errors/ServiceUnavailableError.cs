namespace FunctionalConcepts.Errors;

using System;
using FunctionalConcepts.Enums;

public class ServiceUnavailableError : BaseError
{
    protected ServiceUnavailableError(string message, Exception? exn)
        : base((int)EErrorCode.ServiceUnavailable, message, exn)
    {
    }

    public static implicit operator ServiceUnavailableError(string Message) => new(Message, null);
    public static implicit operator ServiceUnavailableError((string Message, Exception Exn) tuple) => new(tuple.Message, tuple.Exn);

    public static ServiceUnavailableError New(string msg) => (msg);
    public static ServiceUnavailableError New(string msg, Exception exn) => (msg, exn);
}
