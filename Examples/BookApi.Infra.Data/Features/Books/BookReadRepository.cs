namespace BookApi.Infra.Data.Features.Books;

using System;
using System.Linq;
using BookApi.Domain.Bases;
using BookApi.Domain.Features.Books;
using BookApi.Infra.Data.Contexts;
using FunctionalConcepts.Options;
using Microsoft.EntityFrameworkCore;

public class BookReadRepository : IReadRepository<Book>
{
    private BookstoreContext _dbContext;

    public BookReadRepository(BookstoreContext dbContext)
        => _dbContext = dbContext;

    public Task<IQueryable<Book>> GetAll()
        => Task.FromResult(_dbContext.Books.AsNoTracking());

    public async Task<Option<Book>> GetById(Guid id)
        => await _dbContext.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
}
