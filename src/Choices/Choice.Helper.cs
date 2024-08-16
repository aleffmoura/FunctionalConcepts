using FunctionalConcepts.Errors;

namespace FunctionalConcepts.Choices;

public static class Choice
{
    public static Choice<TLeft, TRight> Of<TLeft, TRight>(TLeft value) => value;
    public static Choice<TLeft, TRight> Of<TLeft, TRight>(TRight value) => value;
    public static Choice<TLeft, TRight> Of<TLeft, TRight>(BaseError value) => value;

    internal static Choice<TLeft, TRight> Run<T, TLeft, TRight>(
        Action<T> action,
        T? value,
        Choice<TLeft, TRight> instance)
    {
        try
        {
            action(value!);
            return instance;
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }

    internal static async Task<Choice<TLeft, TRight>> Run<T, TLeft, TRight>(
        Func<T, Task> action,
        T? value,
        Choice<TLeft, TRight> instance)
    {
        try
        {
            await action(value!);
            return instance;
        }
        catch (Exception exn)
        {
            return (UnhandledError)(exn.Message, exn);
        }
    }
}
