namespace FunctionalConcepts.Results;

using FunctionalConcepts.Errors;

public readonly partial struct Result<TEntity>
{
    public readonly Result<TEntity> FailWhen(Func<TEntity, bool> expression, BaseError baseError)
        => IsSuccess && expression(_value!) ? baseError : this;
}
