namespace BookApi.ApplicationService.QueriesHandler;

using System.Threading;
using System.Threading.Tasks;
using BookApi.ApplicationService.Queries;
using BookApi.ApplicationService.ViewModels.Books;
using BookApi.Domain.Bases;
using BookApi.Domain.Features.Books;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;

public class BookByIdQueryHandler : IRequestHandler<BookByIdQuery, Result<BookDetailViewModel>>
{
    private IReadRepository<Book> _repository;

    public BookByIdQueryHandler(IReadRepository<Book> repository)
    {
        _repository = repository;
    }

    public async Task<Result<BookDetailViewModel>> Handle(BookByIdQuery request, CancellationToken cancellationToken)
    {
        var maybeBook = await _repository.GetById(request.Id);

        var result = maybeBook
            .Map(book => new BookDetailViewModel(book!.Id, book.Title, book.Author))
            .Match(WhenBookExists, () => WhenNotFoundBookExists(request.Id));

        return result;
    }

    private Result<BookDetailViewModel> WhenBookExists(BookDetailViewModel bookDetailViewModel) => bookDetailViewModel;

    private Result<BookDetailViewModel> WhenNotFoundBookExists(Guid id)
        => (NotFoundError)$"Não foi encontrado book com identificador: {id}";
}
