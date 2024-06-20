namespace FunctionalConcepts.Results;

using System.Linq.Expressions;
using FunctionalConcepts.Errors;

public readonly partial struct Result<TEntity>
{
    public readonly Result<TEntity> FailWhen(Expression<Func<TEntity, bool>> expression, BaseError baseError)
        => IsSuccess && expression.Compile().Invoke(_value!) ? baseError : this;
}
