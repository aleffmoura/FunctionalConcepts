namespace FunctionalConcepts.Options;

using FunctionalConcepts.Results;

public readonly partial struct Option<T>
{
    public readonly Option<TB> Bind<TB>(Func<T, Option<TB>> execute)
    {
        if (IsNone)
        {
            return Option.None;
        }

        Option<TB> bind = Result.Run(_value!, execute);

        return bind;
    }

    public readonly async ValueTask<Option<TB>> BindAsync<TB>(Func<T, Task<Option<TB>>> execute)
    {
        if (IsNone)
        {
            return Option.None;
        }

        Option<TB> bind = await Result.Run(_value!, execute);

        return bind;
    }
}
