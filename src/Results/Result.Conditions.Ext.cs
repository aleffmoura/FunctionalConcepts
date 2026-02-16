namespace FunctionalConcepts.Results;

using FunctionalConcepts.Errors;

public static partial class Result
{
    public static async ValueTask<Result<TEntity>> ThenAsync<TEntity>(this ValueTask<Result<TEntity>> resultTask, Func<TEntity, Task<Result<TEntity>>> execute)
    {
        var result = await resultTask;
        var opt = await result.ThenAsync(execute);
        return opt;
    }

    public static async ValueTask<Result<TEntity>> ThenAsync<TEntity>(this Task<Result<TEntity>> resultTask, Func<TEntity, Task<Result<TEntity>>> execute)
    {
        var result = await resultTask;
        var opt = await result.ThenAsync(execute);
        return opt;
    }

    public static async ValueTask<Result<TEntity>> ElseAsync<TEntity>(this ValueTask<Result<TEntity>> resultTask, Func<BaseError, Task<Result<TEntity>>> execute)
    {
        var result = await resultTask;
        var opt = await result.ElseAsync(execute);
        return opt;
    }

    public static async ValueTask<Result<TEntity>> ElseAsync<TEntity>(this Task<Result<TEntity>> resultTask, Func<BaseError, Task<Result<TEntity>>> execute)
    {
        var result = await resultTask;
        var opt = await result.ElseAsync(execute);
        return opt;
    }
}
