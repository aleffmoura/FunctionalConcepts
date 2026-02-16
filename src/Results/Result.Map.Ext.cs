namespace FunctionalConcepts.Results;

using FunctionalConcepts.Errors;

public static partial class Result
{
    public static async ValueTask<Result<TR>> MapAsync<TEntity, TR>(this ValueTask<Result<TEntity>> resultTask, Func<TEntity, Task<TR>> execute)
    {
        var result = await resultTask;
        return await result.MapAsync(execute);
    }

    public static async ValueTask<Result<TR>> MapAsync<TEntity, TR>(this Task<Result<TEntity>> resultTask, Func<TEntity, Task<TR>> execute)
    {
        var result = await resultTask;
        return await result.MapAsync(execute);
    }
}
