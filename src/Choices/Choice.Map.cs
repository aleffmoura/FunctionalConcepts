namespace FunctionalConcepts.Choices;

using FunctionalConcepts.Errors;

public readonly partial struct Choice<TLeft, TRight>
{
    public readonly Choice<TR, TRight> MapLeft<TR>(Func<TLeft, TR> execute)
    {
        try
        {
            return IsBottom
                 ? new Choice<TR, TRight>()
                 : this.IsLeft ? execute(_left!) : _right!;
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }

    public readonly async ValueTask<Choice<TR, TRight>> MapLeftAsync<TR>(Func<TLeft, Task<TR>> execute)
    {
        try
        {
            return IsBottom
                 ? new Choice<TR, TRight>()
                 : this.IsLeft ? await execute(_left!) : _right!;
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }

    public readonly Choice<TLeft, TR> MapRight<TR>(Func<TRight, TR> execute)
    {
        try
        {
            return IsBottom
                 ? new Choice<TLeft, TR>()
                 : this.IsRight ? execute(_right!) : _left!;
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }

    public readonly async ValueTask<Choice<TLeft, TR>> MapRightAsync<TR>(Func<TRight, Task<TR>> execute)
    {
        try
        {
            return IsBottom
                 ? new Choice<TLeft, TR>()
                 : this.IsRight ? await execute(_right!) : _left!;
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }
}
