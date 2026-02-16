namespace FunctionalConcepts.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class Option
{
    public static async ValueTask<Option<TEntity>> ThenAsync<TEntity>(this Task<Option<TEntity>> optionTask, Func<TEntity, Task<Option<TEntity>>> execute)
    {
        var result = await optionTask;
        var opt = await result.ThenAsync(execute);
        return opt;
    }

    public static async ValueTask<Option<TEntity>> ThenAsync<TEntity>(this ValueTask<Option<TEntity>> optionTask, Func<TEntity, Task<Option<TEntity>>> execute)
    {
        var result = await optionTask;
        var opt = await result.ThenAsync(execute);
        return opt;
    }

    public static async ValueTask<Option<TEntity>> ElseAsync<TEntity>(this Task<Option<TEntity>> optionTask, Func<Task<Option<TEntity>>> execute)
    {
        var result = await optionTask;
        var opt = await result.ElseAsync(execute);
        return opt;
    }

    public static async ValueTask<Option<TEntity>> ElseAsync<TEntity>(this ValueTask<Option<TEntity>> optionTask, Func<Task<Option<TEntity>>> execute)
    {
        var result = await optionTask;
        var opt = await result.ElseAsync(execute);
        return opt;
    }
}
