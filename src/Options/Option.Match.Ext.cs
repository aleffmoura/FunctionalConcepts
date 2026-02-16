namespace FunctionalConcepts.Options;

using System;
using System.Threading.Tasks;

public static partial class Option
{
    public static async ValueTask<Option<TR>> MatachAsync<TEntity, TR>(this Task<Option<TEntity>> optionTask, Func<TEntity, Task<TR>> onSome, Func<Task<TR>> onNone)
    {
        var opt = await optionTask;
        return await opt.Match(onSome, onNone);
    }

    public static async ValueTask<Option<TR>> MatachAsync<TEntity, TR>(this ValueTask<Option<TEntity>> optionTask, Func<TEntity, Task<TR>> onSome, Func<Task<TR>> onNone)
    {
        var opt = await optionTask;
        return await opt.Match(onSome, onNone);
    }

    public static async ValueTask<Option<TR>> MatchAsync<TEntity, TR>(this ValueTask<Option<TEntity>> optTask, Func<TEntity, TR> onSome, Func<Task<TR>> onNone)
    {
        var opt = await optTask;
        return await opt.MatchAsync(onSome, onNone);
    }

    public static async ValueTask<Option<TR>> MatchAsync<TEntity, TR>(this Task<Option<TEntity>> optTask, Func<TEntity, Task<TR>> onSome, Func<Task<TR>> onNone)
    {
        var opt = await optTask;
        return await opt.MatchAsync(onSome, onNone);
    }

    public static async ValueTask<Option<TR>> MatchAsync<TEntity, TR>(this Task<Option<TEntity>> optTask, Func<TEntity, Task<TR>> onSome, Func<TR> onNone)
    {
        var opt = await optTask;
        return await opt.MatchAsync(onSome, onNone);
    }

    public static async ValueTask<Option<TR>> MatchAsync<TEntity, TR>(this Task<Option<TEntity>> optTask, Func<TEntity, TR> onSome, Func<Task<TR>> onNone)
    {
        var opt = await optTask;
        return await opt.MatchAsync(onSome, onNone);
    }
}
