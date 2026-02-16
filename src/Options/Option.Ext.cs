namespace FunctionalConcepts.Options;

public static partial class Option
{
    public static readonly NoneType None = default;
    public static Option<T> Of<T>(T value) => value;
    public static Option<T> AsOption<T>(this T value) => value;
}
