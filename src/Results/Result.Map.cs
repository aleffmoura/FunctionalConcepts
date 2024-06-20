namespace FunctionalConcepts.Results;

using FunctionalConcepts.Errors;

public readonly partial struct Result<TEntity>
{
    public readonly Result<TR> Map<TR>(Func<TEntity, TR> execute)
    {
        try
        {
            return IsSuccess ? Result.Run(_value!, execute) : _error!;
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
            return IsSuccess ? await Result.Run(_value!, execute) : _error!;
        }
        catch (Exception exn)
        {
            return (UnhandledError)("Error while Map", exn);
        }
    }
}
