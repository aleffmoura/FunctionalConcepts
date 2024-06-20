namespace FunctionalConcepts.Results;
public readonly partial struct Result<TEntity>
{
    public readonly Result<TEntity> Bind(Func<TEntity, Result<TEntity>> execute)
        => IsSuccess ? Result.Try(_value!, execute) : _error!;

    public readonly async ValueTask<Result<TEntity>> BindAsync(Func<TEntity, Task<Result<TEntity>>> execute)
        => IsSuccess ? await Result.TryCatch(_value, execute!) : _error!;
}
