namespace BookApi.Infra.Data.Features.Books;

using System;
using BookApi.Domain.Features.Books;
using BookApi.Domain.Interfaces.Books;
using BookApi.Infra.Data.Contexts;
using FunctionalConcepts;
using FunctionalConcepts.Results;

public class BookRepository : BookReadRepository, IBookRepository
{
    private BookstoreContext _dbContext;

    public BookRepository(BookstoreContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Success> Delete(Book book, CancellationToken cancellationToken)
    {
        _dbContext.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }

    public async Task<Guid> Save(Book book, CancellationToken cancellationToken)
    {
        var entity = _dbContext.Books.Add(book).Entity;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<Success> Update(Book book, CancellationToken cancellationToken)
    {
        _dbContext.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
