namespace FunctionalConcepts.Options;

using FunctionalConcepts.Results;

public readonly partial struct Option<T>
{
    public readonly Option<TR> Map<TR>(Func<T, TR> execute)
        => IsSome ? Result.Run(_value!, execute) : Option.None;

    public readonly async ValueTask<Option<TR>> MapAsync<TR>(Func<T, Task<TR>> execute)
        => IsSome ? await Result.Run(_value!, execute) : Option.None;
}
