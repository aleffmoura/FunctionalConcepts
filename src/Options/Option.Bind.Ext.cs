namespace FunctionalConcepts.Options;

using System;
using System.Threading.Tasks;

public static partial class Option
{
    public static async ValueTask<Option<TB>> BindAsync<TEntity, TB>(this Task<Option<TEntity>> optionTask, Func<TEntity, Task<Option<TB>>> execute)
    {
        var result = await optionTask;
        return await result.BindAsync(execute);
    }

    public static async ValueTask<Option<TB>> BindAsync<TEntity, TB>(this ValueTask<Option<TEntity>> optionTask, Func<TEntity, Task<Option<TB>>> execute)
    {
        var result = await optionTask;
        return await result.BindAsync(execute);
    }
}
