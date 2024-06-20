namespace FunctionalConcepts.Errors;
using System;

public class BaseError
{
    protected BaseError(int code, string message, Exception? ex)
    {
        Code = code;
        Message = message;
        Exception = ex;
    }

    public int Code { get; private set; }
    public string Message { get; private set; }
    public Exception? Exception { get; private set; }

    public static implicit operator BaseError((int Code, string Message) tuple) => New(tuple.Code, tuple.Message);
    public static implicit operator BaseError((int Code, string Message, Exception? Exn) tuple) => New(tuple.Code, tuple.Message, tuple.Exn);

    public static BaseError New(int code, string msg) => new(code, msg, null);
    public static BaseError New(int code, string msg, Exception? ex) => new(code, msg, ex);
}
