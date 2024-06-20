namespace FunctionalConcepts.Choices;
using System;
using System.Threading.Tasks;
using FunctionalConcepts.Interfaces;

public readonly partial struct Choice<TLeft, TRight>
{
    public readonly TR? Match<TR>(
    Func<TLeft, TR> onLeft,
    Func<TRight, TR> onRight)
        => IsBottom
            ? default
            : IMonad<TLeft, TRight>.Match(IsLeft, _left, _right, onLeft, onRight);

    public readonly async ValueTask<TR?> MatchAsync<TR>(
        Func<TLeft, Task<TR>> onLeft,
        Func<TRight, TR> onRight)
        => IsBottom
            ? default
            : await IMonad<TLeft, TRight>.Match(IsLeft, _left, _right, onLeft, onRight);

    public readonly async ValueTask<TR?> MatchAsync<TR>(
        Func<TLeft, TR> onLeft,
        Func<TRight, Task<TR>> onRight)
        => IsBottom
            ? default
            : await IMonad<TLeft, TRight>.Match(IsLeft, _left, _right, onLeft, onRight);

    public readonly async Task<TR?> MatchAsync<TR>(
        Func<TLeft, Task<TR>> onLeft,
        Func<TRight, Task<TR>> onRight)
        => IsBottom
            ? default
            : await IMonad<TLeft, TRight>.Match(IsLeft, _left, _right, onLeft, onRight);
}
