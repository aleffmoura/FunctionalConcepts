namespace FunctionalConcepts.Results;

using FunctionalConcepts.Errors;

public static partial class Result
{
    public static Success Success => default;

    public static Result<TSuccess> Of<TSuccess>(TSuccess value) => value;
    public static Result<TSuccess> Of<TSuccess>(BaseError error) => error;

    internal static TR Run<TIn, TR>(TIn value, Func<TIn, TR> execute)
    {
        return execute(value);
    }

    internal static Task<TR> Run<TIn, TR>(TIn value, Func<TIn, Task<TR>> execute)
    {
        return execute(value);
    }

    internal static Result<TR> Try<TIn, TR>(TIn value, Func<TIn, Result<TR>> execute)
    {
        try
        {
            return execute(value);
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }

    internal static async Task<Result<TR>> TryCatch<TIn, TR>(TIn value, Func<TIn, Task<Result<TR>>> execute)
    {
        try
        {
            return await execute(value);
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }
}
