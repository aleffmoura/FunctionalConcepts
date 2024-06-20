namespace FunctionalConcepts.Options;

using System;
using System.Linq.Expressions;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;

public readonly partial struct Option<T>
{
    public Result<T> FailWhen(Expression<Func<T, bool>> expression, BaseError baseError, string defaulMessageIfNone = "object not found")
        => IsNone ? (NotFoundError)defaulMessageIfNone
                  : expression.Compile().Invoke(_value!)
                        ? baseError : _value!;
}
