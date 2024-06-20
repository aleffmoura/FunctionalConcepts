namespace FunctionalConcepts.Options;
public static class Option
{
    public static Option<T> Of<T>(T value) => value;
}
