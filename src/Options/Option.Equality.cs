namespace FunctionalConcepts.Options;
public readonly partial struct Option<T>
    : IEquatable<Option<T>>
{
    public static bool operator ==(Option<T> left, Option<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Option<T> left, Option<T> right)
    {
        return !(left == right);
    }

    public readonly bool Equals(Option<T> other)
    {
        return IsNone
                ? other.IsNone
                : other.Map(Compare)
                       .Match(some => some, () => false);
    }

    public override readonly bool Equals(object? obj)
        => obj is Option<T> option && Equals(option);

    public readonly override int GetHashCode()
        => IsSome ? _value!.GetHashCode()
                  : NoneType.Value.GetHashCode();

    private readonly bool Compare(T other) => _value!.Equals(other);
}
