namespace FunctionalConcepts.Choices;

using FunctionalConcepts.Errors;

public readonly partial struct Choice<TLeft, TRight>
{
    public readonly Choice<TL, TRight> BindLeft<TL>(
        Func<TLeft, Choice<TL, TRight>> execute)
    {
        try
        {
            return IsBottom
                 ? new Choice<TL, TRight>()
                 : this.IsLeft ? execute(_left!) : _right!;
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }

    public readonly async ValueTask<Choice<TL, TRight>> BindLeftAsync<TL>(
        Func<TLeft, Task<Choice<TL, TRight>>> execute)
    {
        try
        {
            return IsBottom
                 ? new Choice<TL, TRight>()
                 : this.IsLeft ? await execute(_left!) : _right!;
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }

    public readonly Choice<TLeft, TR> BindRight<TR>(
        Func<TRight, Choice<TLeft, TR>> execute)
    {
        try
        {
            return IsBottom
                 ? new Choice<TLeft, TR>()
                 : this.IsLeft ? _left! : execute(_right!);
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }

    public readonly async ValueTask<Choice<TLeft, TR>> BindRightAsync<TR>(
        Func<TRight, Task<Choice<TLeft, TR>>> execute)
    {
        try
        {
            return IsBottom
                 ? new Choice<TLeft, TR>()
                 : this.IsLeft ? _left! : await execute(_right!);
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }
}
