namespace BookApi.ApplicationService.CommandsHandler;

using BookApi.ApplicationService.Commands;
using BookApi.Domain.Features.Books;
using BookApi.Domain.Interfaces.Books;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;

using MediatR;

public class BookCreateCommandHandler : IRequestHandler<BookCreateCommand, Result<Guid>>
{
    private IBookRepository _bookRepository;

    public BookCreateCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Result<Guid>> Handle(BookCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Book book = new Book
            {
                Title = request!.Title,
                Author = request!.Author,
                ReleaseDate = request!.Released,
                BookCoverUrl = request?.BookCoverUrl,
            };

            return await _bookRepository.Save(book, cancellationToken);
        }
        catch (Exception ex)
        {
            return (ServiceUnavailableError)(ex.Message, ex);
        }
    }
}
