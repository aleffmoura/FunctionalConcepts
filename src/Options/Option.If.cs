namespace FunctionalConcepts.Options;

using System;
using FunctionalConcepts.Helpers;

public readonly partial struct Option<T>
{
    public Option<T> Then(Action<T> execute)
        => IsSome ? OptionHelper.Run(_value!, this, execute) : this;

    public async ValueTask<Option<T>> ThenAsync(Func<T, Task> execute)
        => IsSome ? await OptionHelper.Run(_value!, this, execute) : this;

    public Option<T> Else(Action execute)
        => IsNone ? OptionHelper.Run(this, execute) : this;

    public async ValueTask<Option<T>> ElseAsync(Func<Task> execute)
        => IsNone ? await OptionHelper.Run(this, execute) : this;
}
