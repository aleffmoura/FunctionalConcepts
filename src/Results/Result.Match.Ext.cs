namespace FunctionalConcepts.Results;

using FunctionalConcepts.Errors;

public static partial class Result
{
    public static async ValueTask<Result<TR>> MatchAsync<TEntity, TR>(this ValueTask<Result<TEntity>> resultTask, Func<TEntity, Task<TR>> onSome, Func<BaseError, Task<TR>> onError)
    {
        var result = await resultTask;
        return await result.MatchAsync(onSome, onError);
    }

    public static async ValueTask<Result<TR>> MatchAsync<TEntity, TR>(this ValueTask<Result<TEntity>> resultTask, Func<TEntity, Task<TR>> onSome, Func<BaseError, TR> onError)
    {
        var result = await resultTask;
        return await result.MatchAsync(onSome, onError);
    }

    public static async ValueTask<Result<TR>> MatchAsync<TEntity, TR>(this ValueTask<Result<TEntity>> resultTask, Func<TEntity, TR> onSome, Func<BaseError, Task<TR>> onError)
    {
        var result = await resultTask;
        return await result.MatchAsync(onSome, onError);
    }

    public static async ValueTask<Result<TR>> MatchAsync<TEntity, TR>(this Task<Result<TEntity>> resultTask, Func<TEntity, Task<TR>> onSome, Func<BaseError, Task<TR>> onError)
    {
        var result = await resultTask;
        return await result.MatchAsync(onSome, onError);
    }

    public static async ValueTask<Result<TR>> MatchAsync<TEntity, TR>(this Task<Result<TEntity>> resultTask, Func<TEntity, Task<TR>> onSome, Func<BaseError, TR> onError)
    {
        var result = await resultTask;
        return await result.MatchAsync(onSome, onError);
    }

    public static async ValueTask<Result<TR>> MatchAsync<TEntity, TR>(this Task<Result<TEntity>> resultTask, Func<TEntity, TR> onSome, Func<BaseError, Task<TR>> onError)
    {
        var result = await resultTask;
        return await result.MatchAsync(onSome, onError);
    }
}
