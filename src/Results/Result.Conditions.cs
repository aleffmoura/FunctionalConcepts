namespace FunctionalConcepts.Results;

using FunctionalConcepts.Errors;

public readonly partial struct Result<TEntity>
{
    public readonly Result<TEntity> Then(Action<TEntity> execute)
        => IsSuccess ? Result.Try<TEntity, TEntity>(_value!, value =>
        {
            execute(value);
            return value;
        }) : this;

    public readonly Result<TEntity> Else(Action<BaseError> execute)
        => IsFail ? Result.Try<BaseError, TEntity>(_error!, err =>
        {
            execute(err);
            return err;
        }) : this;

    public readonly async ValueTask<Result<TEntity>> ThenAsync(Func<TEntity, Task> execute)
        => IsSuccess ? await Result.TryCatch<TEntity, TEntity>(_value!, async value =>
        {
            await execute(value);
            return value;
        }) : this;

    public readonly async ValueTask<Result<TEntity>> ElseAsync(Func<BaseError, Task> execute)
        => IsFail ? await Result.TryCatch<BaseError, TEntity>(_error!, async err =>
        {
            await execute(err);
            return err;
        }) : this;
}
