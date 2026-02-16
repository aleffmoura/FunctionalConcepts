namespace FunctionalConcepts.Results;

using FunctionalConcepts.Errors;

public static partial class Result
{
    public static async ValueTask<Result<TB>> BindAsync<TEntity, TB>(this ValueTask<Result<TEntity>> resultTask, Func<TEntity, Task<Result<TB>>> execute)
    {
        var result = await resultTask;
        return await result.BindAsync(execute);
    }

    public static async ValueTask<Result<TB>> BindAsync<TEntity, TB>(this Task<Result<TEntity>> resultTask, Func<TEntity, Task<Result<TB>>> execute)
    {
        var result = await resultTask;
        return await result.BindAsync(execute);
    }
}
