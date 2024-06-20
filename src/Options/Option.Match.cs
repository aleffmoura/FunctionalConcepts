namespace FunctionalConcepts.Options;
using System;
using FunctionalConcepts.Interfaces;
public readonly partial struct Option<T>
{
    public TR Match<TR>(Func<T, TR> some, Func<TR> none)
            => IMonad<NoneType, T>
                .Match(IsNone, default, _value, _ => none(), some);

    public Task<TR> MatchAsync<TR>(Func<T, Task<TR>> some, Func<Task<TR>> none)
            => IMonad<NoneType, T>
                .Match(IsNone, default, _value, _ => none(), some);

    public ValueTask<TR> MatchAsync<TR>(Func<T, Task<TR>> some, Func<TR> none)
            => IMonad<NoneType, T>
                .Match(IsNone, default, _value, _ => none(), some);

    public ValueTask<TR> MatchAsync<TR>(Func<T, TR> some, Func<Task<TR>> none)
            => IMonad<NoneType, T>
                .Match(IsNone, default, _value, _ => none(), some);
}
