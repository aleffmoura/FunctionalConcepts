namespace FunctionalConcepts.Results;

using FunctionalConcepts.Errors;

public readonly partial struct Result<TEntity>
{
    public readonly Result<TR> Map<TR>(Func<TEntity, TR> execute)
    {
        try
        {
            return IsFail ? _error! : Result.Run(_value!, execute);
        }
        catch (Exception exn)
        {
            return (UnhandledError)("Error while Map", exn);
        }
    }

    public readonly async ValueTask<Result<TR>> MapAsync<TR>(Func<TEntity, Task<TR>> execute)
    {
        try
        {
            return IsFail ? _error! : await Result.Run(_value!, execute);
        }
        catch (Exception exn)
        {
            return (UnhandledError)("Error while Map", exn);
        }
    }
}
