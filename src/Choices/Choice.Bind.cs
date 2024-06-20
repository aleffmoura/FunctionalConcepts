namespace FunctionalConcepts.Choices;

using FunctionalConcepts.Errors;

public readonly partial struct Choice<TLeft, TRight>
{
    public readonly Choice<TLeft, TRight> BindLeft(
        Func<TLeft, Choice<TLeft, TRight>> execute)
    {
        try
        {
            return IsBottom
                 ? new Choice<TLeft, TRight>()
                 : this.IsLeft ? execute(_left!) : _right!;
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }

    public readonly async ValueTask<Choice<TLeft, TRight>> BindLeftAsync(
        Func<TLeft, Task<Choice<TLeft, TRight>>> execute)
    {
        try
        {
            return IsBottom
                 ? new Choice<TLeft, TRight>()
                 : this.IsLeft ? await execute(_left!) : _right!;
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }

    public readonly Choice<TLeft, TRight> BindRight(
        Func<TRight, Choice<TLeft, TRight>> execute)
    {
        try
        {
            return IsBottom
                 ? new Choice<TLeft, TRight>()
                 : this.IsLeft ? _left! : execute(_right!);
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }

    public readonly async ValueTask<Choice<TLeft, TRight>> BindRightAsync(
        Func<TRight, Task<Choice<TLeft, TRight>>> execute)
    {
        try
        {
            return IsBottom
                 ? new Choice<TLeft, TRight>()
                 : this.IsLeft ? _left! : await execute(_right!);
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }
}
