namespace BookApi.ApplicationService.CommandsHandler;

using BookApi.ApplicationService.Commands;
using BookApi.Domain.Interfaces.Books;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;

using MediatR;

public class BookUpdateCommandHandler : IRequestHandler<BookUpdateCommand, Result<Success>>
{
    private readonly IBookRepository _bookRepository;

    public BookUpdateCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Result<Success>> Handle(
        BookUpdateCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var maybeBook = await _bookRepository.GetById(request.Id);

            return await maybeBook.MatchAsync(
                async book =>
                {
                    book.Author = request.Author;
                    book.ReleaseDate = request.Released;
                    book.Title = request.Title;

                    return Result.Of(await _bookRepository.Update(book, cancellationToken));
                },
                () => (NotFoundError)$"Book with id: {request.Id} not found");
        }
        catch (Exception ex)
        {
            return (ServiceUnavailableError)(ex.Message, ex);
        }
    }
}
