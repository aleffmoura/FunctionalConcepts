namespace BookApi.ApplicationService.Commands;

using FluentValidation;
using FunctionalConcepts;
using FunctionalConcepts.Results;

using MediatR;

public class BookUpdateCommand : IRequest<Result<Success>>
{
    public Guid Id { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime Released { get; set; }

    private class BookUpdateCommandValidator : AbstractValidator<BookUpdateCommand>
    {
        public BookUpdateCommandValidator()
        {
            RuleFor(a => a.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id is invalid");

            RuleFor(a => a.Author)
                    .NotEmpty()
                    .WithMessage("Author cant be null or empty")
                    .Must(x => x is not null && x.Length > 2)
                    .WithMessage("Author is less than 2.");

            RuleFor(a => a.Title)
                    .NotEmpty()
                    .WithMessage("Title cant be null or empty")
                    .Must(x => x is not null && x.Length > 2)
                    .WithMessage("Title is less than 2.");

            RuleFor(a => a.Released)
                    .Must(x => x > DateTime.MinValue)
                    .WithMessage("Release date is invalid");
        }
    }
}
