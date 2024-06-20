namespace BookApi.ApplicationService.CommandsHandler;

using BookApi.ApplicationService.Commands;
using BookApi.Domain.Features.Books;
using BookApi.Domain.Interfaces.Books;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;

using MediatR;

public class BookRemoveCommandHandler : IRequestHandler<BookRemoveCommand, Result<Success>>
{
    private readonly IBookRepository _bookRepository;

    public BookRemoveCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Result<Success>> Handle(BookRemoveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var maybeBook = await _bookRepository.GetById(request.Id);

            return await maybeBook.MatchAsync(
                async book => Result.Of(await _bookRepository.Delete(book, cancellationToken)),
                () => (NotFoundError)$"Book with id: {request.Id} not found");
        }
        catch (Exception ex)
        {
            return (ServiceUnavailableError)(ex.Message, ex);
        }
    }
}
