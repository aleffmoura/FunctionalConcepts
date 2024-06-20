namespace BookApi.ApplicationService.Queries;

using BookApi.ApplicationService.ViewModels.Books;
using FluentValidation;
using FunctionalConcepts.Results;
using MediatR;

public class BookByIdQuery : IRequest<Result<BookDetailViewModel>>
{
    public Guid Id { get; set; }

    private class BookByIdQueryValidator : AbstractValidator<BookByIdQuery>
    {
        public BookByIdQueryValidator()
        {
            RuleFor(a => a.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id cant be empty");
        }
    }
}
