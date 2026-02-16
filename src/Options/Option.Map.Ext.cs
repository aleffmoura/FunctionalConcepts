namespace FunctionalConcepts.Options;

using System;
using System.Threading.Tasks;

public static partial class Option
{
    public static async ValueTask<Option<TR>> MapAsync<TEntity, TR>(this Task<Option<TEntity>> optionTask, Func<TEntity, Task<TR>> execute)
    {
        var result = await optionTask;
        return await result.MapAsync(execute);
    }

    public static async ValueTask<Option<TR>> MapAsync<TEntity, TR>(this ValueTask<Option<TEntity>> optionTask, Func<TEntity, Task<TR>> execute)
    {
        var result = await optionTask;
        return await result.MapAsync(execute);
    }
}
