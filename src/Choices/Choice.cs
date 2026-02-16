namespace FunctionalConcepts.Choices;

using FunctionalConcepts.Constants;
using FunctionalConcepts.Errors;

public readonly partial struct Choice<TLeft, TRight>
{
    private readonly BaseError? _bottom;
    private readonly TLeft? _left;
    private readonly TRight? _right;

    public Choice()
        : this((500, "Choice was initialize with bottom"))
    {
    }

    private Choice(BaseError baseError)
    {
        IsRight = false;
        IsLeft = false;
        _left = default;
        _right = default;
        IsBottom = true;
        _bottom = baseError;
    }

    private Choice(TLeft left)
    {
        IsRight = false;
        IsBottom = false;
        IsLeft = false;
        _left = default;
        _right = default;
        _bottom = default;

        if (left is not null)
        {
            _left = left;
            IsLeft = true;
            return;
        }

        IsBottom = true;
        _bottom = ErrorConstant.BOTTOM;
    }

    private Choice(TRight right)
    {
        IsRight = false;
        IsBottom = false;
        IsLeft = false;
        _left = default;
        _right = default;
        _bottom = default;

        if (right is not null)
        {
            _right = right;
            IsRight = true;
            return;
        }

        IsBottom = true;
        _bottom = ErrorConstant.BOTTOM;
    }

    public readonly bool IsLeft { get; }
    public readonly bool IsRight { get; }
    public readonly bool IsBottom { get; }

    public static implicit operator Choice<TLeft, TRight>(TLeft left) => new(left);
    public static implicit operator Choice<TLeft, TRight>(TRight right) => new(right);
    public static implicit operator Choice<TLeft, TRight>(BaseError value) => new(value);

    public static implicit operator TLeft?(Choice<TLeft, TRight> choice) => choice._left;
    public static implicit operator TRight?(Choice<TLeft, TRight> choice) => choice._right;
    public static implicit operator BaseError?(Choice<TLeft, TRight> choice) => choice._bottom;
}
