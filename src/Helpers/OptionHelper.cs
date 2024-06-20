namespace FunctionalConcepts.Helpers;

using System;
using System.Threading.Tasks;
using FunctionalConcepts.Options;

internal class OptionHelper
{
    internal static Option<T> Run<T>(T value, Option<T> option, Action<T> action)
    {
        action(value);
        return option;
    }

    internal static async Task<Option<T>> Run<T>(T value, Option<T> option, Func<T, Task> action)
    {
        await action(value);
        return option;
    }

    internal static Option<T> Run<T>(Option<T> option, Action action)
    {
        action();
        return option;
    }

    internal static async Task<Option<T>> Run<T>(Option<T> option, Func<Task> action)
    {
        await action();
        return option;
    }
}
