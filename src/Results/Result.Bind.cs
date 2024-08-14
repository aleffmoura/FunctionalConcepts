namespace FunctionalConcepts.Results;
public readonly partial struct Result<TEntity>
{
    public readonly Result<TB> Bind<TB>(Func<TEntity, Result<TB>> execute)
    {
        if (IsFail)
        {
            return _error!;
        }

        Result<TB> bind = Result.Try(_value!, execute);

        return bind;
    }

    public readonly async ValueTask<Result<TB>> BindAsync<TB>(Func<TEntity, Task<Result<TB>>> execute)
    {
        if (IsFail)
        {
            return _error!;
        }

        Result<TB> bind = await Result.TryCatch(_value!, execute);

        return bind;
    }
}
