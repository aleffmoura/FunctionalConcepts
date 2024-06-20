namespace BookApi.ApplicationService.Queries;

using BookApi.Domain.Features.Books;
using FunctionalConcepts.Results;
using MediatR;

public class BookCollectionQuery : IRequest<Result<IQueryable<Book>>>
{
}
