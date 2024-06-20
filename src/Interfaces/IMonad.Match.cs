using FunctionalConcepts.Results;

namespace FunctionalConcepts.Interfaces
{
    internal partial interface IMonad<TLeft, TRight>
    {
        public static TR Match<TR>(
            bool isLeft,
            TLeft? left,
            TRight? right,
            Func<TLeft, TR> onLeft,
            Func<TRight, TR> onRight)
        {
            return isLeft ? Result.Run(left!, onLeft)
                          : Result.Run(right!, onRight);
        }

        public static async Task<TR> Match<TR>(
            bool isLeft,
            TLeft? left,
            TRight? right,
            Func<TLeft, Task<TR>> onLeft,
            Func<TRight, Task<TR>> onRight)
        {
            return isLeft ? await Result.Run(left!, onLeft)
                          : await Result.Run(right!, onRight);
        }

        public static async ValueTask<TR> Match<TR>(
            bool isLeft,
            TLeft? left,
            TRight? right,
            Func<TLeft, Task<TR>> onLeft,
            Func<TRight, TR> onRight)
        {
            return isLeft ? await Result.Run(left!, onLeft)
                          : Result.Run(right!, onRight);
        }

        public static async ValueTask<TR> Match<TR>(
            bool isLeft,
            TLeft? left,
            TRight? right,
            Func<TLeft, TR> onLeft,
            Func<TRight, Task<TR>> onRight)
        {
            return isLeft ? Result.Run(left!, onLeft)
                          : await Result.Run(right!, onRight);
        }
    }
}
