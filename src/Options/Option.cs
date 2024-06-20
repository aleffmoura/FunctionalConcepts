namespace FunctionalConcepts.Options;
public readonly partial struct Option<T>
{
    private static readonly Option<T> _none = new();
    private readonly T? _value;

    public Option()
    {
        IsSome = false;
        _value = default;
    }

    private Option(T value)
    {
        _value = value;
        IsSome = value is not null;
    }

    public readonly bool IsSome { get; }
    public readonly bool IsNone => !IsSome;

    public static implicit operator Option<T>(NoneType _) => _none;
    public static implicit operator Option<T>(T value) => new(value);
}
