namespace FunctionalConcepts.Results;

using FunctionalConcepts.Errors;
using FunctionalConcepts.Interfaces;
public readonly partial struct Result<TEntity>
    : IMonad<BaseError, TEntity>
{
    public TR? Match<TR>(
        Func<TEntity, TR> onSome,
        Func<BaseError, TR> onError)
            => IMonad<BaseError, TEntity>
                .Match(IsFail, _error, _value, onError, onSome);

    public async Task<TR> MatchAsync<TR>(
       Func<TEntity, Task<TR>> onSome,
       Func<BaseError, Task<TR>> onError)
            => await IMonad<BaseError, TEntity>
                .Match(IsFail, _error, _value, onError, onSome);

    public async Task<TR> MatchAsync<TR>(
        Func<TEntity, Task<TR>> onSome,
        Func<BaseError, TR> onError)
            => await IMonad<BaseError, TEntity>
                .Match(IsFail, _error, _value, onError, onSome);

    public ValueTask<TR> MatchAsync<TR>(
        Func<TEntity, TR> onSome,
        Func<BaseError, Task<TR>> onError)
            => IMonad<BaseError, TEntity>
                .Match(IsFail, _error, _value, onError, onSome);
}
