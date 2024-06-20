namespace BookApi.ApplicationService.Commands;

using FluentValidation;
using FunctionalConcepts;
using FunctionalConcepts.Results;

using MediatR;

public class BookRemoveCommand : IRequest<Result<Success>>
{
    public BookRemoveCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }

    private class BookUpdateCommandValidator : AbstractValidator<BookRemoveCommand>
    {
        public BookUpdateCommandValidator()
        {
        }
    }
}
