using FunctionalConcepts.Errors;

namespace FunctionalConcepts.Choices;

public readonly partial struct Choice<TLeft, TRight>
{
    public readonly Choice<TLeft, TRight> ThenLeft(Action<TLeft> execute)
        => IsLeft ? Choice.Run(execute, _left, this) : this;

    public readonly async ValueTask<Choice<TLeft, TRight>> ThenLeftAsync(Func<TLeft, Task> execute)
        => IsLeft ? await Choice.Run(execute, _left, this) : this;

    public readonly Choice<TLeft, TRight> ThenRight(Action<TRight> execute)
        => IsRight ? Choice.Run(execute, _right, this) : this;

    public readonly async ValueTask<Choice<TLeft, TRight>> ThenRightAsync(Func<TRight, Task> execute)
        => IsRight ? await Choice.Run(execute, _right, this) : this;

    public readonly Choice<TLeft, TRight> Else(Action<BaseError> execute)
        => IsBottom ? Choice.Run(execute, _bottom, this) : this;

    public readonly async ValueTask<Choice<TLeft, TRight>> ElseAsync(Func<BaseError, Task> execute)
        => IsBottom ? await Choice.Run(execute, _bottom, this) : this;
}
