namespace FunctionalConcepts.Choices;

using System.Linq.Expressions;
using FunctionalConcepts.Errors;

public readonly partial struct Choice<TLeft, TRight>
{
    public readonly Choice<TLeft, TRight> FailWhen(
        Expression<Func<TLeft, bool>> expression,
        BaseError baseError)
            => IsBottom || IsRight
                ? this
                : expression.Compile().Invoke(_left!) ? baseError : this;

    public readonly Choice<TLeft, TRight> FailWhen(
        Expression<Func<TRight, bool>> expression,
        BaseError baseError)
            => IsBottom || IsLeft
                ? this
                : expression.Compile().Invoke(_right!) ? baseError : this;
}
