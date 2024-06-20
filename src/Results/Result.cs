namespace FunctionalConcepts.Results;

using FunctionalConcepts.Constants;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Interfaces;
using FunctionalConcepts.Options;

public readonly partial struct Result<TEntity>
    : IMonad<BaseError, TEntity>
{
    private readonly TEntity? _value;
    private readonly BaseError? _error;

    public Result()
        : this(ErrorConstant.BOTTOM)
    {
    }

    private Result(TEntity value)
    {
        IsSuccess = false;
        _value = default;
        _error = default;

        if(value is null)
        {
            _error = ErrorConstant.BOTTOM;
            IsSuccess = false;
            return;
        }

        _value = value;
        IsSuccess = true;
    }

    private Result(BaseError error)
    {
        IsSuccess = false;
        _value = default;
        _error = error;
        IsSuccess = false;
    }

    public readonly bool IsSuccess { get; }
    public readonly bool IsFail { get => !IsSuccess; }

    public readonly Option<BaseError> AsOptionFail
        => IsFail ? Option.Of(_error!) : NoneType.Value;

    public readonly Option<TEntity> AsOption
        => IsSuccess ? Option.Of(_value!) : NoneType.Value;

    public static implicit operator Result<TEntity>(TEntity success) => new(success);
    public static implicit operator Result<TEntity>(BaseError error) => new(error);
}
