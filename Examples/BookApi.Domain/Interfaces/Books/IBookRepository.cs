namespace BookApi.Domain.Interfaces.Books;

using BookApi.Domain.Bases;
using BookApi.Domain.Features.Books;
using FunctionalConcepts;

public interface IBookRepository : IReadRepository<Book>
{
    Task<Guid> Save(Book book, CancellationToken cancellationToken);
    Task<Success> Delete(Book book, CancellationToken cancellationToken);
    Task<Success> Update(Book book, CancellationToken cancellationToken);
}
