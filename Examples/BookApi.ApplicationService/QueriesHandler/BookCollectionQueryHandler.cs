namespace BookApi.ApplicationService.QueriesHandler;

using System.Threading;
using System.Threading.Tasks;
using BookApi.ApplicationService.Queries;
using BookApi.Domain.Bases;
using BookApi.Domain.Features.Books;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;

public class BookCollectionQueryHandler : IRequestHandler<BookCollectionQuery, Result<IQueryable<Book>>>
{
    private IReadRepository<Book> _repository;

    public BookCollectionQueryHandler(IReadRepository<Book> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IQueryable<Book>>> Handle(BookCollectionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return Result.Of(await _repository.GetAll());
        }
        catch (Exception ex)
        {
            return (ServiceUnavailableError)(ex.Message, ex);
        }
    }
}
