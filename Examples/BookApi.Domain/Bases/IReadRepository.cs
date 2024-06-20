namespace BookApi.Domain.Bases;

using BookApi.Domain.Features.Books;
using FunctionalConcepts.Options;

public interface IReadRepository<T>
    where T : Entity<T>
{
    Task<Option<Book>> GetById(Guid id);
    Task<IQueryable<T>> GetAll();
}
